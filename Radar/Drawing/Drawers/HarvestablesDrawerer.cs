using X975.Settings;
using X975.Radar.Drawing.OverlaySettings;
using X975.Radar.Utility;
using GameOverlay.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using System.Linq;
using X975.Radar.GameObjects.Harvestables;
using X975.Radar.GameObjects.LocalPlayer;

namespace X975.Radar.Drawers
{
    public class HarvestablesDrawerer : IDrawerer
    {
        private readonly ConfigHandler configHandler;
        private readonly RadarOverlayBrushesDictionary brushesDictionary;
        private readonly Graphics gfx;

        private readonly LocalPlayerHandler localPlayerHandler;
        private readonly HarvestablesHandler harvestablesHandler;

        public HarvestablesDrawerer(Graphics gfx, RadarOverlayBrushesDictionary brushesDictionary, LocalPlayerHandler localPlayerHandler, HarvestablesHandler harvestablesHandler)
        {
            configHandler = ConfigHandler.Source;
            this.gfx = gfx;
            this.brushesDictionary = brushesDictionary;

            this.localPlayerHandler = localPlayerHandler;
            this.harvestablesHandler = harvestablesHandler;
        }

        public async Task DrawAsync()
        {
            if (configHandler.config.ResourcesEnabled)
            {
                lock (harvestablesHandler.harvestableList)
                {
                    foreach (Harvestable h in harvestablesHandler.harvestableList.Values)
                    {
                        if (!configHandler.config.HarvestableList.Contains($"T{h.Tier}-{h.Charge}-{h.Type}")) continue;

                        Vector2 pos = (h.Position - localPlayerHandler.localPlayer.Position).Rotate();

                        if (h.Count >= configHandler.config.StackFilter)
                        {
                            if (h.Charge > 0)
                            {
                                Additions.DrawHarvestableDot(gfx, brushesDictionary._resoucesImages[h.Type], brushesDictionary._resourcesColors[h.Tier], brushesDictionary._chargesColors[h.Charge], pos, configHandler.config.HarvestableDotSize);
                            }
                            else
                            {
                                Additions.DrawHarvestableDot(gfx, brushesDictionary._resoucesImages[h.Type], brushesDictionary._resourcesColors[h.Tier], brushesDictionary._resourcesHightlights[h.Tier], pos, configHandler.config.HarvestableDotSize);
                            }

                            if (configHandler.config.StackSize)
                                Additions.DrawTextCentered(gfx, brushesDictionary._fonts["Main"], brushesDictionary._resourcesColors[8], pos.X, pos.Y + configHandler.config.HarvestableDotSize / 2 + 1.5f, h.Count.ToString());
                        }
                    }
                }
            }
        }
    }
}
