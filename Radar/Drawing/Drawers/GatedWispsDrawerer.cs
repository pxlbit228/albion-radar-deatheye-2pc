using X975.Settings;
using X975.Radar.Drawing.OverlaySettings;
using X975.Radar.Utility;
using System.Numerics;
using GameOverlay.Drawing;
using System;
using System.Threading.Tasks;
using X975.Radar.GameObjects.GatedWisps;
using System.Linq;
using X975.Radar.GameObjects.LocalPlayer;
using X975.Radar.GameObjects.Players;

namespace X975.Radar.Drawers
{
    public class GatedWispsDrawerer : IDrawerer
    {
        private readonly ConfigHandler configHandler = ConfigHandler.Source;
        private readonly RadarOverlayBrushesDictionary brushesDictionary;
        private readonly Graphics gfx;

        private readonly LocalPlayerHandler localPlayerHandler;
        private readonly GatedWispsHandler wispInGateHandler;

        public GatedWispsDrawerer(Graphics gfx, RadarOverlayBrushesDictionary brushesDictionary, LocalPlayerHandler localPlayerHandler, GatedWispsHandler wispInGateHandler)
        {
            this.gfx = gfx;
            this.brushesDictionary = brushesDictionary;

            this.localPlayerHandler = localPlayerHandler;
            this.wispInGateHandler = wispInGateHandler;
        }

        public async Task DrawAsync()
        {
            if (Convert.ToBoolean(configHandler.config.MistWisps[2]))
            {
                lock (wispInGateHandler.gatedWispsList)
                {
                    foreach (GatedWisp w in wispInGateHandler.gatedWispsList.Values)
                    {
                        Vector2 pos = (w.Position - localPlayerHandler.localPlayer.Position).Rotate();

                        gfx.DrawIconDot(brushesDictionary._brushes["Black"], brushesDictionary._mobsImages["MIST_GATE"], pos, Convert.ToSingle(configHandler.config.MistWisps[1]));
                    }
                }
            }
        }
    }
}
