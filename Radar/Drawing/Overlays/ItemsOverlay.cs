using X975.Radar.Utility;
using X975.Radar.GameObjects.Players;
using X975.Radar.GameObjects.LocalPlayer;
using System.Threading.Tasks;
using X975.Radar.Drawers;
using X977.Radar.OverlaySettings;
using X975.Radar.OverlaySettings;

namespace X975.Radar.Drawing.Overlays
{
    public class ItemsOverlay : Overlay
    {
        private readonly LocalPlayerHandler localPlayerHandler;
        private readonly PlayersHandler playersHandler;

        private readonly ItemsOverlaySettings overlaySettings;
        private readonly ItemsOverlayBrushesDictionary itemsOverlayBrushesDictionary;

        private readonly IDrawerer itemsDrawerer;

        public ItemsOverlay(LocalPlayerHandler localPlayerHandler, PlayersHandler playersHandler)
        {
            FPS = 20;
            IsTopmost = true;
            IsTransparent = true;
            IsVisible = true;
            Width = 1;
            Height = 1;
            X = 0;
            Y = 0;

            this.overlaySettings = new ItemsOverlaySettings(this);
            this.localPlayerHandler = localPlayerHandler;
            this.playersHandler = playersHandler;

            itemsOverlayBrushesDictionary = new ItemsOverlayBrushesDictionary(Graphics);
            itemsDrawerer = new ItemsDrawerer(Graphics, itemsOverlayBrushesDictionary, this.localPlayerHandler, this.playersHandler);
        }

        protected override async Task InitGraphics()
        {
            await itemsOverlayBrushesDictionary.Init();
        }

        protected override async Task DrawAsync()
        {
            if (localPlayerHandler.localPlayer.CurrentCluster.ClusterColor > ClusterColor.City)
            {
                await overlaySettings.PrepareDraw();
                await itemsDrawerer.DrawAsync();
                await overlaySettings.EndDraw();
            }
        }
    }
}
