using GameOverlay.Drawing;
using System.Threading.Tasks;
using X975.Radar.Drawing;
using X975.Radar.Drawing.Overlays;
using X975.Settings;

namespace X977.Radar.OverlaySettings
{
    public class ItemsOverlaySettings
    {
        private readonly ItemsOverlay overlay;
        private readonly ConfigHandler configHandler;

        public ItemsOverlaySettings(ItemsOverlay overlay)
        {
            this.overlay = overlay;
            this.configHandler = ConfigHandler.Source;
        }

        public async Task PrepareDraw()
        {
            if (configHandler.config.ItemsStyle == 0)
            {
                overlay.Width = (int)(400 * configHandler.config.ItemsScale);
                overlay.Height = (int)(configHandler.config.LinesCount * 80 * configHandler.config.ItemsScale);
            }
            else 
            {
                overlay.Width = (int)(configHandler.config.LinesCount * 400 * configHandler.config.ItemsScale);
                overlay.Height = (int)(80 * configHandler.config.ItemsScale);
            }

            overlay.X = configHandler.config.ItemsXoffset;
            overlay.Y = configHandler.config.ItemsYoffset;
            
            overlay.Graphics.TransformStart(
                TransformationMatrix.Transformation(
                (float)configHandler.config.ItemsScale,//DEFAULT
                (float)configHandler.config.ItemsScale,//DEFAULT
                0, 0, 0));
        }

        public async Task EndDraw()
        {
            overlay.Graphics.TransformEnd();
        }
    }
}
