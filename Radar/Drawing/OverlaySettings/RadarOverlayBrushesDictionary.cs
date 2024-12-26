using X975.Settings;
using X975.Radar.Utility;
using GameOverlay.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace X975.Radar.Drawing.OverlaySettings
{
    public class RadarOverlayBrushesDictionary
    {
        private readonly ConfigHandler configHandler;
        public readonly Dictionary<string, SolidBrush> _brushes, _players;
        public readonly Dictionary<int, SolidBrush> _factionColors, _resourcesColors, _resourcesHightlights, _chargesColors, _aspectedColors, _aspectedHighlightColors, _proksColors;
        public readonly Dictionary<string, Font> _fonts;
        public readonly Dictionary<string, Image> _resoucesImages, _dungeonsImages, _mobsImages;
        private readonly Graphics gfx;

        public RadarOverlayBrushesDictionary(Graphics gfx)
        {
            this.gfx = gfx;
            configHandler = ConfigHandler.Source;

            _brushes = new Dictionary<string, SolidBrush>();
            _players = new Dictionary<string, SolidBrush>();
            _factionColors = new Dictionary<int, SolidBrush>();
            _resourcesColors = new Dictionary<int, SolidBrush>();
            _resourcesHightlights = new Dictionary<int, SolidBrush>();
            _chargesColors = new Dictionary<int, SolidBrush>();
            _proksColors = new Dictionary<int, SolidBrush>();

            _aspectedColors = new Dictionary<int, SolidBrush>();
            _aspectedHighlightColors = new Dictionary<int, SolidBrush>();

            _fonts = new Dictionary<string, Font>();

            _resoucesImages = new Dictionary<string, Image>();
            _dungeonsImages = new Dictionary<string, Image>();
            _mobsImages = new Dictionary<string, Image>();
        }

        public async Task Init()
        {
            #region FONT

            _fonts["Main"] = gfx.CreateFont("Museo Sans Cyrl 900", 4, true);
            _fonts["Icon"] = gfx.CreateFont("DEATHEYE", 5, true);

            #endregion

            #region DEFAULT BRUSHES

            _brushes["Zero"] = gfx.CreateSolidBrush(0, 0, 0, 0);
            _brushes["Black"] = gfx.CreateSolidBrush(0, 0, 0);
            _brushes["Gray"] = gfx.CreateSolidBrush(128, 128, 128);
            _brushes["White"] = gfx.CreateSolidBrush(255, 255, 255);
            _brushes["Olive"] = gfx.CreateSolidBrush(78, 92, 20);
            _brushes["Red"] = gfx.CreateSolidBrush(255, 0, 0);
            _brushes["Yellow"] = gfx.CreateSolidBrush(255, 255, 0);
            _brushes["Green"] = gfx.CreateSolidBrush(0, 255, 0);
            _brushes["Orange"] = gfx.CreateSolidBrush(208, 194, 0);
            _brushes["UnderHealth"] = gfx.CreateSolidBrush(120, 117, 117);

            #endregion

            #region STYLE

            _brushes["Background"] = gfx.CreateSolidBrush(0, 0, 0);
            _brushes["Grid"] = gfx.CreateSolidBrush(0, 0, 0);
            _brushes["Outline"] = gfx.CreateSolidBrush(0, 0, 0);
            _brushes["FovFirst"] = gfx.CreateSolidBrush(0, 0, 0);
            _brushes["FovSecond"] = gfx.CreateSolidBrush(0, 0, 0);
            _brushes["FovThird"] = gfx.CreateSolidBrush(0, 0, 0);
            _brushes["Centering"] = gfx.CreateSolidBrush(0, 0, 0);

            #endregion

            #region PLAYERS

            _players["MainColor"] = gfx.CreateSolidBrush(0, 0, 0);
            _players["AccentColor"] = gfx.CreateSolidBrush(0, 0, 0);
            _players["NickColor"] = gfx.CreateSolidBrush(0, 0, 0);
            _players["AllianceColor"] = gfx.CreateSolidBrush(0, 0, 0);
            _players["DistanceColor"] = gfx.CreateSolidBrush(0, 0, 0);
            _players["MountedColor"] = gfx.CreateSolidBrush(0, 0, 0);
            _players["VisibleContactColor"] = gfx.CreateSolidBrush(0, 0, 0);
            _players["VisibleContactColor2"] = gfx.CreateSolidBrush(0, 0, 0);
            _players["HealthColor"] = gfx.CreateSolidBrush(0, 0, 0);
            _players["FocusLineColor"] = gfx.CreateSolidBrush(0, 0, 0);

            #endregion

            #region FACTION COLORS
            _factionColors[1] = gfx.CreateSolidBrush(91, 163, 231);
            _factionColors[2] = gfx.CreateSolidBrush(62, 136, 20);
            _factionColors[3] = gfx.CreateSolidBrush(227, 147, 49);
            _factionColors[4] = gfx.CreateSolidBrush(255, 255, 255);
            _factionColors[5] = gfx.CreateSolidBrush(170, 0, 255);
            _factionColors[6] = gfx.CreateSolidBrush(154, 45, 48);
            #endregion

            #region Resources COLORS

            _resourcesColors[1] = gfx.CreateSolidBrush(151, 151, 151);
            _resourcesColors[2] = gfx.CreateSolidBrush(172, 139, 117);
            _resourcesColors[3] = gfx.CreateSolidBrush(5, 139, 5);
            _resourcesColors[4] = gfx.CreateSolidBrush(5, 78, 143);
            _resourcesColors[5] = gfx.CreateSolidBrush(163, 5, 5);
            _resourcesColors[6] = gfx.CreateSolidBrush(209, 109, 8);
            _resourcesColors[7] = gfx.CreateSolidBrush(208, 194, 0);
            _resourcesColors[8] = gfx.CreateSolidBrush(255, 255, 255);

            _resourcesHightlights[1] = gfx.CreateSolidBrush(55, 55, 55);
            _resourcesHightlights[2] = gfx.CreateSolidBrush(90, 70, 50);
            _resourcesHightlights[3] = gfx.CreateSolidBrush(4, 55, 4);
            _resourcesHightlights[4] = gfx.CreateSolidBrush(4, 43, 77);
            _resourcesHightlights[5] = gfx.CreateSolidBrush(75, 5, 5);
            _resourcesHightlights[6] = gfx.CreateSolidBrush(97, 50, 3);
            _resourcesHightlights[7] = gfx.CreateSolidBrush(94, 88, 0);
            _resourcesHightlights[8] = gfx.CreateSolidBrush(58, 58, 58);

            _resoucesImages["WOOD"] = gfx.CreateImage(RadarImages.wood);
            _resoucesImages["ROCK"] = gfx.CreateImage(RadarImages.rock);
            _resoucesImages["FIBER"] = gfx.CreateImage(RadarImages.fiber);
            _resoucesImages["HIDE"] = gfx.CreateImage(RadarImages.hide);
            _resoucesImages["ORE"] = gfx.CreateImage(RadarImages.ore);

            _chargesColors[0] = gfx.CreateSolidBrush(0, 0, 0, 0);
            _chargesColors[1] = gfx.CreateSolidBrush(8, 208, 25);
            _chargesColors[2] = gfx.CreateSolidBrush(0, 208, 205);
            _chargesColors[3] = gfx.CreateSolidBrush(208, 0, 208);
            _chargesColors[4] = gfx.CreateSolidBrush(255, 255, 0);

            _aspectedHighlightColors[1] = gfx.CreateSolidBrush(156, 156, 156);
            _aspectedHighlightColors[2] = gfx.CreateSolidBrush(156, 156, 156);
            _aspectedHighlightColors[3] = gfx.CreateSolidBrush(255, 255, 255);

            #endregion

            #region PROK COLORS

            _proksColors[0] = gfx.CreateSolidBrush(0, 0, 0);
            _proksColors[1] = gfx.CreateSolidBrush(0, 255, 205);
            _proksColors[2] = gfx.CreateSolidBrush(255, 255, 0);
            _proksColors[3] = gfx.CreateSolidBrush(255, 0, 0);

            #endregion

            #region DUNGEON IMAGES

            _dungeonsImages["SOLO"] = gfx.CreateImage(RadarImages.solo);
            _dungeonsImages["GROUP"] = gfx.CreateImage(RadarImages.group);
            _dungeonsImages["CORRUPT"] = gfx.CreateImage(RadarImages.corrupted);
            _dungeonsImages["HELLGATE"] = gfx.CreateImage(RadarImages.hellgate);

            #endregion

            #region MOBS IMAGES

            _mobsImages["DRONE"] = gfx.CreateImage(ImageToByte(Properties.Resources.DRONE));
            _mobsImages["CHEST"] = gfx.CreateImage(ImageToByte(Properties.Resources.CHEST));

            _mobsImages["MIST_PORTAL"] = gfx.CreateImage(ImageToByte(Properties.Resources.MIST_PORTAL));
            _mobsImages["MIST_GATE"] = gfx.CreateImage(ImageToByte(Properties.Resources.WISP));

            _mobsImages["GRIFFIN"] = gfx.CreateImage(ImageToByte(Properties.Resources.GRIFFIN));
            _mobsImages["SPIDER"] = gfx.CreateImage(ImageToByte(Properties.Resources.SPIDER));
            _mobsImages["DRAGON"] = gfx.CreateImage(ImageToByte(Properties.Resources.DRAGON));
            _mobsImages["CRYSTAL_SPIDER"] = gfx.CreateImage(ImageToByte(Properties.Resources.CRYSTALSPIDER));

            _mobsImages["HOOKER"] = gfx.CreateImage(ImageToByte(Properties.Resources.HOOK));
            _mobsImages["SILENCE"] = gfx.CreateImage(ImageToByte(Properties.Resources.SILENCE));
            _mobsImages["LAVA"] = gfx.CreateImage(ImageToByte(Properties.Resources.LAVA));
            _mobsImages["KNOCKBACK"] = gfx.CreateImage(ImageToByte(Properties.Resources.KNOCKBACK));
            _mobsImages["GLUE"] = gfx.CreateImage(ImageToByte(Properties.Resources.GLUE));

            _mobsImages["TREASURE"] = gfx.CreateImage(ImageToByte(Properties.Resources.TREASURE));
            _mobsImages["WORLDCHEST"] = gfx.CreateImage(ImageToByte(Properties.Resources.CHEST));
            _mobsImages["EVENT"] = gfx.CreateImage(ImageToByte(Properties.Resources.EVENT));

            #endregion
        }

        public Task UpdateColors()
        {
            #region STYLE

            _brushes["Background"].Color = ConvertColor(configHandler.config.StyleSettings[0].ToString());
            _brushes["Grid"].Color = ConvertColor(configHandler.config.StyleSettings[1].ToString());
            _brushes["Outline"].Color = ConvertColor(configHandler.config.StyleSettings[3].ToString());
            _brushes["FovFirst"].Color = ConvertColor(configHandler.config.StyleSettings[5].ToString());
            _brushes["FovSecond"].Color = ConvertColor(configHandler.config.StyleSettings[9].ToString());
            _brushes["FovThird"].Color = ConvertColor(configHandler.config.StyleSettings[13].ToString());
            _brushes["Centering"].Color = ConvertColor(configHandler.config.StyleSettings[17].ToString());

            #endregion

            return Task.CompletedTask;
        }

        private Color ConvertColor(string color)
        {
            System.Drawing.Color temp = System.Drawing.ColorTranslator.FromHtml(color);

            return new Color(temp.R, temp.G, temp.B, temp.A);
        }

        public byte[] ImageToByte(System.Drawing.Bitmap img)
        {
            return (byte[])new System.Drawing.ImageConverter().ConvertTo(img, typeof(byte[]));
        }
    }
}
