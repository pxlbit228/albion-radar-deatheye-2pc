using GameOverlay.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;
using X975.Settings;
using X975.Tools;

namespace X975.Radar.OverlaySettings
{
    public class ItemsOverlayBrushesDictionary
    {
        private readonly Graphics gfx;
        private readonly ConfigHandler configHandler;
        public readonly Dictionary<string, SolidBrush> _brushes;
        public readonly Dictionary<string, Font> _fonts;
        public Dictionary<string, Image> _itemImage;

        public ItemsOverlayBrushesDictionary(Graphics gfx)
        {
            this.gfx = gfx;
            configHandler = ConfigHandler.Source;

            _fonts = new Dictionary<string, Font>();
            _brushes = new Dictionary<string, SolidBrush>();
            _itemImage = new Dictionary<string, Image>();
        }

        public async Task Init()
        {
            _fonts["Main"] = gfx.CreateFont("Museo Sans Cyrl 900", 16, true);

            _brushes["Black"] = gfx.CreateSolidBrush(0, 0, 0);
            _brushes["White"] = gfx.CreateSolidBrush(255, 255, 255);
            _brushes["Red"] = gfx.CreateSolidBrush(255, 0, 0);
            _brushes["UnderHealth"] = gfx.CreateSolidBrush(120, 117, 117);

            _itemImage.Add("T1_TRASH", gfx.CreateImage(Pathfinder.mainFolder + "\\ITEMS\\T1_TRASH.png"));
        }
    }
}
