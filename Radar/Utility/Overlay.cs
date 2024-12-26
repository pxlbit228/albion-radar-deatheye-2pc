using GameOverlay.Drawing;
using GameOverlay.Windows;
using System.Threading.Tasks;

namespace X975.Radar
{
    public abstract class Overlay : GraphicsWindow
    {
        protected Overlay() : base(new OverlayGraphics()) { }

        protected override async void OnSetupGraphics(Graphics graphics)
        {
            await InitGraphics();
        }

        protected override async void OnDrawGraphics(int frameCount, long frameTime, long deltaTime)
        {
            try
            {
                Graphics.BeginScene();
                Graphics.ClearScene();
                await DrawAsync();
                Graphics.EndScene();
            }
            catch { }
        }

        protected abstract Task DrawAsync();

        protected abstract Task InitGraphics();
    }
}
