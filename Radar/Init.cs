using System;
using X975.Radar.GameObjects.GatedWisps;
using X975.Radar.GameObjects.FishNodes;
using X975.Radar.GameObjects.LootChests;
using X975.Radar.GameObjects.LocalPlayer;
using X975.Radar.GameObjects.Dungeons;
using X975.Radar.GameObjects.Harvestables;
using X975.Radar.GameObjects.Mobs;
using X975.Radar.GameObjects.Players;
using X975.Radar.Packets.Handlers;
using Albion.Network;
using System.Threading;
using X975.Radar.Sniffer;
using X975.Radar.Drawing.Overlays;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using X975.Protocol.Connect.Messages.ResponseObj;
using X975.Radar.Dependencies.Harvestable;
using X975.Radar.Dependencies.Item;
using X975.Radar.Dependencies.Mob;
using X975.Tools;

namespace X975.Radar
{
    public class Init
    {
        private readonly LocalPlayerHandler localPlayerHandler;
        private readonly PlayersHandler playersHandler;
        private readonly HarvestablesHandler harvestablesHandler;
        private readonly MobsHandler mobsHandler;
        private readonly DungeonsHandler dungeonsHandler;
        private readonly FishNodesHandler fishNodesHandler;
        private readonly GatedWispsHandler gatedWispsHandler;
        private readonly LootChestsHandler lootChestsHandler;

        private readonly PacketDeviceSelector packetSniffer;
        private readonly Thread globalTimer, radarOverlay, itemsOverlay, infoOverlay;

        private IPhotonReceiver photonReceiver;

        public static PacketIndexes PacketIndexes;
        public static PacketOffsets PacketOffsets;

        public Init()
        {
            if (!File.Exists(Path.Combine(Pathfinder.mainFolder, "ITEMS\\T1_TRASH.png")))
            {
                Diagnostics.ReportFatal("Can't find ITEMS\\T1_TRASH.png!" +
                                        "\n" +
                                        "Make sure that ITEMS folder is in the same directory as the executable and contains images of items.");
            }
            
            PacketIndexes = Diagnostics.DoVital(() => ReadJson<PacketIndexes>("jsons/indexes.json"),
                "Can't load json/indexes.json");
            PacketOffsets = Diagnostics.DoVital(() => ReadJson<PacketOffsets>("jsons/offsets.json"),
                "Can't load json/offsets.json");

            #region HANDLERS

            localPlayerHandler = Diagnostics.DoVital(() =>
                    new LocalPlayerHandler(ReadJson<Dictionary<string, Cluster>>("jsons/clusters.json")),
                "Can't load json/clusters.json"
            );

            playersHandler = Diagnostics.DoVital(() =>
                    new PlayersHandler(ItemData.Load("ao-bin-dumps/items.xml")),
                "Can't load ao-bin-dumps/items.xml"
            );

            harvestablesHandler = Diagnostics.DoVital(() =>
                    new HarvestablesHandler(HarvestableData.Load("ao-bin-dumps/harvestables.xml"),
                        localPlayerHandler),
                "Can't load ao-bin-dumps/harvestables.xml"
            );

            mobsHandler = Diagnostics.DoVital(() =>
                    new MobsHandler(MobData.Load("ao-bin-dumps/mobs.xml")),
                "Can't load ao-bin-dumps/mobs.xml"
            );

            dungeonsHandler = new DungeonsHandler();
            fishNodesHandler = new FishNodesHandler();
            gatedWispsHandler = new GatedWispsHandler();
            lootChestsHandler = new LootChestsHandler();

            #endregion

            #region PHOTON

            ReceiverBuilder builder = ReceiverBuilder.Create();

            // builder.AddHandler(new DebugHandler());
            builder.AddEventHandler(new LeaveEventHandler(playersHandler, mobsHandler, dungeonsHandler,
                fishNodesHandler, gatedWispsHandler, lootChestsHandler));
            builder.AddResponseHandler(new ChangeClusterEventHandler(localPlayerHandler, playersHandler,
                harvestablesHandler, mobsHandler, dungeonsHandler, fishNodesHandler, gatedWispsHandler,
                lootChestsHandler));
            builder.AddResponseHandler(new JoinResponseOperationHandler(localPlayerHandler, playersHandler,
                harvestablesHandler, mobsHandler, dungeonsHandler, fishNodesHandler, gatedWispsHandler,
                lootChestsHandler));
            builder.AddRequestHandler(new MoveRequestOperationHandler(localPlayerHandler, harvestablesHandler));
            builder.AddEventHandler(new MistsPlayerJoinedInfoEventHandler(localPlayerHandler));
            builder.AddEventHandler(new LoadClusterObjectsEventHandler(localPlayerHandler));
            builder.AddEventHandler(new NewCharacterEventHandler(playersHandler, localPlayerHandler));
            builder.AddEventHandler(new MountedEventHandler(playersHandler));
            builder.AddEventHandler(new ChangeFlaggingFinishedEventHandler(localPlayerHandler, playersHandler));
            builder.AddEventHandler(new CharacterEquipmentChangedEventHandler(playersHandler));
            builder.AddEventHandler(new MoveEventHandler(playersHandler, mobsHandler));
            builder.AddEventHandler(new HealthUpdateEventHandler(playersHandler, mobsHandler));
            builder.AddEventHandler(new RegenerationChangedEventHandler(playersHandler)); //Разобраться с этим дерьмом
            builder.AddEventHandler(new NewHarvestableEventHandler(harvestablesHandler));
            builder.AddEventHandler(new NewHarvestablesListEventHandler(harvestablesHandler));
            builder.AddEventHandler(new HarvestableChangeStateEventHandler(harvestablesHandler));
            builder.AddEventHandler(new MobChangeStateEventHandler(mobsHandler));
            builder.AddEventHandler(new NewMobEventHandler(mobsHandler));
            builder.AddEventHandler(new NewFishingZoneEventHandler(fishNodesHandler));
            builder.AddEventHandler(new NewDungeonEventHandler(dungeonsHandler));
            builder.AddEventHandler(new NewGatedWispEventHandler(gatedWispsHandler));
            builder.AddEventHandler(new WispGateOpenedEventHandler(gatedWispsHandler));
            builder.AddEventHandler(new NewLootChestEventHandler(lootChestsHandler));
            builder.AddEventHandler(new KeySyncEventHandler(playersHandler));

            photonReceiver = builder.Build();

            #endregion

            #region THREADS

            packetSniffer = new PacketDeviceSelector(photonReceiver);

            globalTimer = new Thread(() => new GlobalTimer(localPlayerHandler, playersHandler, mobsHandler).Start());

            radarOverlay = new Thread(() =>
            {
                GameOverlay.TimerService.EnableHighPrecisionTimers();

                using (RadarOverlay overlay = new RadarOverlay(localPlayerHandler, playersHandler, harvestablesHandler,
                           mobsHandler, dungeonsHandler, fishNodesHandler, gatedWispsHandler, lootChestsHandler))
                {
                    overlay.Create();
                    overlay.Join();
                }
            });

            itemsOverlay = new Thread(() =>
            {
                GameOverlay.TimerService.EnableHighPrecisionTimers();

                using (ItemsOverlay overlay = new ItemsOverlay(localPlayerHandler, playersHandler))
                {
                    overlay.Create();
                    overlay.Join();
                }
            });

            infoOverlay = new Thread(() =>
            {
                GameOverlay.TimerService.EnableHighPrecisionTimers();

                using (InfoOverlay overlay = new InfoOverlay(localPlayerHandler))
                {
                    overlay.Create();
                    overlay.Join();
                }
            });

            #endregion
        }

        public void Start()
        {
            packetSniffer.Start();
            globalTimer.Start();
            radarOverlay.Start();
            itemsOverlay.Start();
            infoOverlay.Start();
        }

        private T ReadJson<T>(string file)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(file));
        }
    }
}