using X975.Settings;
using X975.Radar.Drawing.OverlaySettings;
using X975.Radar.Utility;
using GameOverlay.Drawing;
using System;
using System.Numerics;
using System.Threading.Tasks;
using X975.Radar.GameObjects.FishNodes;
using X975.Radar.GameObjects.LocalPlayer;

namespace X975.Radar.Drawers
{
    public class FishNodesDrawerer : IDrawerer
    {
        private readonly ConfigHandler configHandler = ConfigHandler.Source;
        
        private readonly RadarOverlayBrushesDictionary brushesDictionary;
        private readonly Graphics gfx;

        private readonly LocalPlayerHandler localPlayerHandler;
        private readonly FishNodesHandler fishNodesHandler;

        public FishNodesDrawerer(Graphics gfx, RadarOverlayBrushesDictionary brushesDictionary, LocalPlayerHandler localPlayerHandler, FishNodesHandler fishNodesHandler)
        {
            this.gfx = gfx;
            this.brushesDictionary = brushesDictionary;

            this.localPlayerHandler = localPlayerHandler;
            this.fishNodesHandler = fishNodesHandler;
        }

        public async Task DrawAsync()
        {
            if (Convert.ToBoolean(configHandler.config.FishNodes[0]))
            {
                lock (fishNodesHandler.fishNodesList)
                {
                    foreach (FishNode d in fishNodesHandler.fishNodesList.Values)
                    {
                        Vector2 pos = (d.Position - localPlayerHandler.localPlayer.Position).Rotate();

                        if (d.Size >= 1 && d.Size <= 5)
                        {
                            gfx.DrawDotWithStringIcon(brushesDictionary._resourcesColors[8], brushesDictionary._resourcesHightlights[8], brushesDictionary._brushes["Black"], pos, brushesDictionary._fonts["Icon"], "M", Convert.ToSingle(configHandler.config.FishNodes[1]));
                            Additions.DrawTextCentered(gfx, brushesDictionary._fonts["Main"], brushesDictionary._resourcesColors[8], pos.X, pos.Y + Convert.ToSingle(configHandler.config.FishNodes[1]) / 2 + 1.5f, d.Size.ToString());
                        }
                    }
                }
            }
        }
    }
}
