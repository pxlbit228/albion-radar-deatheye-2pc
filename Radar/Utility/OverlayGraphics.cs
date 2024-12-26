using GameOverlay.Drawing;

namespace X975.Radar
{
    public class OverlayGraphics : Graphics
    {
        public OverlayGraphics()
        {
            this.MeasureFPS = true;
            this.PerPrimitiveAntiAliasing = true;
            this.TextAntiAliasing = true;
            this.UseMultiThreadedFactories = true;
        }
    }
}
