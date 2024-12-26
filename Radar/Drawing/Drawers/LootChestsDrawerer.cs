using X975.Settings;
using X975.Radar.Drawing.OverlaySettings;
using X975.Radar.Utility;
using System.Numerics;
using GameOverlay.Drawing;
using System;
using System.Threading.Tasks;
using System.Linq;
using X975.Radar.GameObjects.LootChests;
using X975.Radar.GameObjects.LocalPlayer;

namespace X975.Radar.Drawers
{
    public class LootChestsDrawerer : IDrawerer
    {
        private readonly ConfigHandler configHandler = ConfigHandler.Source;
        private readonly RadarOverlayBrushesDictionary brushesDictionary;
        private readonly Graphics gfx;

        private readonly LocalPlayerHandler localPlayerHandler;
        private readonly LootChestsHandler worldChestHandler;

        public LootChestsDrawerer(Graphics gfx, RadarOverlayBrushesDictionary brushesDictionary, LocalPlayerHandler localPlayerHandler, LootChestsHandler worldChestHandler)
        {
            this.gfx = gfx;
            this.brushesDictionary = brushesDictionary;

            this.localPlayerHandler = localPlayerHandler;
            this.worldChestHandler = worldChestHandler;
        }

        public async Task DrawAsync()
        {
            if (Convert.ToBoolean(configHandler.config.HiddenTreasures[2]))
            {
                lock (worldChestHandler.lootChestsList)
                {
                    foreach (LootChest d in worldChestHandler.lootChestsList.Values)
                    {
                        Vector2 pos = (d.Position - localPlayerHandler.localPlayer.Position).Rotate();

                        gfx.DrawIconDot(brushesDictionary._chargesColors[d.Charge], brushesDictionary._mobsImages["CHEST"], pos, Convert.ToSingle(configHandler.config.HiddenTreasures[1]));
                    }
                }
            }
        }
    }
}
