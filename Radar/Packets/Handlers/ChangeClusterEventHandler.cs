using System;
using X975.Radar.GameObjects.LocalPlayer;
using X975.Radar.GameObjects.Players;
using X975.Radar.GameObjects.Harvestables;
using X975.Radar.GameObjects.Mobs;
using X975.Radar.GameObjects.Dungeons;
using X975.Radar.GameObjects.FishNodes;
using X975.Radar.GameObjects.GatedWisps;
using X975.Radar.GameObjects.LootChests;
using Albion.Network;
using System.Threading.Tasks;

namespace X975.Radar.Packets.Handlers
{
    class ChangeClusterEventHandler : ResponsePacketHandler<ChangeClusterEvent>
    {
        private readonly LocalPlayerHandler localPlayerHandler;
        private readonly PlayersHandler playersHandler;
        private readonly HarvestablesHandler harvestablesHandler;
        private readonly MobsHandler mobsHandler;
        private readonly DungeonsHandler dungeonsHandler;
        private readonly FishNodesHandler fishNodesHandler;
        private readonly GatedWispsHandler gatedWispsHandler;
        private readonly LootChestsHandler lootChestsHandler;

        public ChangeClusterEventHandler(LocalPlayerHandler localPlayerHandler, PlayersHandler playersHandler, HarvestablesHandler harvestablesHandler, MobsHandler mobsHandler, DungeonsHandler dungeonsHandler, FishNodesHandler fishNodesHandler, GatedWispsHandler gatedWispsHandler, LootChestsHandler lootChestsHandler) : base(Init.PacketIndexes.ChangeCluster)
        {
            this.localPlayerHandler = localPlayerHandler;
            this.playersHandler = playersHandler;
            this.harvestablesHandler = harvestablesHandler;
            this.mobsHandler = mobsHandler;
            this.dungeonsHandler = dungeonsHandler;
            this.fishNodesHandler = fishNodesHandler;
            this.gatedWispsHandler = gatedWispsHandler;
            this.lootChestsHandler = lootChestsHandler;
        }

        protected override Task OnActionAsync(ChangeClusterEvent value)
        {
            localPlayerHandler.ChangeCluster(value.LocationId, value.DynamicClusterData);

            playersHandler.Clear();
            harvestablesHandler.Clear();
            mobsHandler.Clear();
            dungeonsHandler.Clear();
            fishNodesHandler.Clear();
            gatedWispsHandler.Clear();
            lootChestsHandler.Clear();

            return Task.CompletedTask;
        }
    }
}
