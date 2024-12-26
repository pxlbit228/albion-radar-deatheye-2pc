using X975.Settings;
using GameOverlay.Drawing;
using System.Threading.Tasks;
using X975.Radar.Drawing.Overlays;

namespace X975.Radar.Drawing.OverlaySettings
{
    public class RadarOverlaySettings
    {
        private readonly RadarOverlay overlay;
        private readonly ConfigHandler configHandler;

        public RadarOverlaySettings(RadarOverlay overlay)
        {
            this.overlay = overlay;
            this.configHandler = ConfigHandler.Source;
        }

        public async Task PrepareDraw()
        {
            overlay.X = configHandler.config.X;
            overlay.Y = configHandler.config.Y;
            overlay.Width = configHandler.config.Width;
            overlay.Height = configHandler.config.Height;

            overlay.Graphics.TransformStart(
                TransformationMatrix.Transformation(
                (float)configHandler.config.Zoom,
                (float)configHandler.config.Zoom,
                0.0f,
                configHandler.config.Width / 2,
                configHandler.config.Height / 2));
        }

        public async Task EndDraw()
        {
            overlay.Graphics.TransformEnd();
        }
    }
}
