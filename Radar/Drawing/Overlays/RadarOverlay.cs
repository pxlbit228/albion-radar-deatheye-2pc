using X975.Radar.Drawers;
using X975.Radar.Utility;
using X975.Radar.GameObjects.Mobs;
using X975.Radar.GameObjects.Players;
using X975.Radar.GameObjects.Dungeons;
using X975.Radar.GameObjects.GatedWisps;
using X975.Radar.GameObjects.Harvestables;
using X975.Radar.GameObjects.FishNodes;
using X975.Radar.GameObjects.LootChests;
using X975.Radar.GameObjects.LocalPlayer;
using System.Threading.Tasks;
using X975.Radar.Drawing.OverlaySettings;

namespace X975.Radar.Drawing.Overlays
{
    public class RadarOverlay : Overlay
    {
        private readonly LocalPlayerHandler localPlayerHandler;
        private readonly PlayersHandler playersHandler;
        private readonly HarvestablesHandler harvestablesHandler;
        private readonly MobsHandler mobsHandler;
        private readonly DungeonsHandler dungeonsHandler;
        private readonly FishNodesHandler fishNodesHandler;
        private readonly GatedWispsHandler gatedWispsHandler;
        private readonly LootChestsHandler lootChestsHandler;

        private readonly RadarOverlayBrushesDictionary brushesDictionary;
        private readonly RadarOverlaySettings overlaySettings;

        private readonly IDrawerer hudDrawerer;
        private readonly IDrawerer playersDrawerer;
        private readonly IDrawerer harvestablesDrawerer;
        private readonly IDrawerer mobsDrawerer;
        private readonly IDrawerer dungeonsDrawerer;
        private readonly IDrawerer fishNodesDrawerer;
        private readonly IDrawerer gatedWispsDrawerer;
        private readonly IDrawerer lootChestsDrawerer;

        public RadarOverlay(LocalPlayerHandler localPlayerHandler, PlayersHandler playersHandler,
            HarvestablesHandler harvestablesHandler, MobsHandler mobsHandler, DungeonsHandler dungeonsHandler,
            FishNodesHandler fishNodesHandler, GatedWispsHandler gatedWispsHandler, LootChestsHandler lootChestsHandler)
        {
            FPS = 30;
            IsTopmost = true;
            IsTransparent = false;
            IsVisible = true;
            Width = Additions.GetDisplayResolution().Width;
            Height = Additions.GetDisplayResolution().Height;
            X = 0;
            Y = 0;

            this.localPlayerHandler = localPlayerHandler;
            this.playersHandler = playersHandler;
            this.harvestablesHandler = harvestablesHandler;
            this.mobsHandler = mobsHandler;
            this.dungeonsHandler = dungeonsHandler;
            this.fishNodesHandler = fishNodesHandler;
            this.gatedWispsHandler = gatedWispsHandler;
            this.lootChestsHandler = lootChestsHandler;

            brushesDictionary = new RadarOverlayBrushesDictionary(Graphics);

            hudDrawerer = new HudDrawerer(Graphics, brushesDictionary, localPlayerHandler);

            overlaySettings = new RadarOverlaySettings(this);

            playersDrawerer =
                new PlayersDrawerer(Graphics, brushesDictionary, this.localPlayerHandler, this.playersHandler);
            harvestablesDrawerer = new HarvestablesDrawerer(Graphics, brushesDictionary, this.localPlayerHandler,
                this.harvestablesHandler);
            mobsDrawerer = new MobsDrawerer(Graphics, brushesDictionary, this.localPlayerHandler, this.mobsHandler);
            dungeonsDrawerer = new DungeonsDrawerer(Graphics, brushesDictionary, this.localPlayerHandler,
                this.dungeonsHandler);
            fishNodesDrawerer = new FishNodesDrawerer(Graphics, brushesDictionary, this.localPlayerHandler,
                this.fishNodesHandler);
            gatedWispsDrawerer = new GatedWispsDrawerer(Graphics, brushesDictionary, this.localPlayerHandler,
                this.gatedWispsHandler);
            lootChestsDrawerer = new LootChestsDrawerer(Graphics, brushesDictionary, this.localPlayerHandler,
                this.lootChestsHandler);
        }

        protected override async Task InitGraphics()
        {
            await brushesDictionary.Init();
        }

        protected override async Task DrawAsync()
        {
            await brushesDictionary.UpdateColors();
            await hudDrawerer.DrawAsync();
            
            await overlaySettings.PrepareDraw();

            await harvestablesDrawerer.DrawAsync();
            await mobsDrawerer.DrawAsync();
            await gatedWispsDrawerer.DrawAsync();
            await lootChestsDrawerer.DrawAsync();
            await fishNodesDrawerer.DrawAsync();
            await dungeonsDrawerer.DrawAsync();
            await playersDrawerer.DrawAsync();

            await overlaySettings.EndDraw();
        }
    }
}