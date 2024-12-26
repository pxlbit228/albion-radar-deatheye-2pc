using GameOverlay.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace X975.Radar.OverlaySettings
{
    public class InfoOverlayBrushesDictionary
    {
        private readonly Graphics gfx;

        public readonly Dictionary<string, Font> _fonts;
        public readonly Dictionary<string, SolidBrush> _designColors;
        public readonly Dictionary<int, Image> _mistImages;

        public InfoOverlayBrushesDictionary(Graphics gfx)
        {
            this.gfx = gfx;
            
            _designColors = new Dictionary<string, SolidBrush>();
            _fonts = new Dictionary<string, Font>();
            _mistImages = new Dictionary<int, Image>();
        }

        public async Task Init()
        {
            _fonts["Main"] = gfx.CreateFont("Museo Sans Cyrl 900", 16, true);

            _designColors["Background"] = gfx.CreateSolidBrush(21, 21, 21);
            _designColors["Corner"] = gfx.CreateSolidBrush(116, 199, 31);
            _designColors["White"] = gfx.CreateSolidBrush(255, 255, 255);

            _mistImages[0] = gfx.CreateImage(ImageToByte(Properties.Resources.MIST_PORTAL));
            _mistImages[1] = gfx.CreateImage(ImageToByte(Properties.Resources.abbey));

            _mistImages[70] = gfx.CreateImage(ImageToByte(Properties.Resources.mist_chest));

            _mistImages[226] = gfx.CreateImage(ImageToByte(Properties.Resources.green_mist));
            _mistImages[227] = gfx.CreateImage(ImageToByte(Properties.Resources.blue_mist));
            _mistImages[228] = gfx.CreateImage(ImageToByte(Properties.Resources.purple_mist));
            _mistImages[229] = gfx.CreateImage(ImageToByte(Properties.Resources.legend_mist));

            _mistImages[230] = gfx.CreateImage(ImageToByte(Properties.Resources.green_mist));
            _mistImages[231] = gfx.CreateImage(ImageToByte(Properties.Resources.blue_mist));
            _mistImages[232] = gfx.CreateImage(ImageToByte(Properties.Resources.purple_mist));
            _mistImages[233] = gfx.CreateImage(ImageToByte(Properties.Resources.legend_mist));
        }

        public byte[] ImageToByte(System.Drawing.Bitmap img)
        {
            return (byte[])new System.Drawing.ImageConverter().ConvertTo(img, typeof(byte[]));
        }
    }
}
