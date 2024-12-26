using X975.Settings;
using X975.Radar.GameObjects.Dungeons;
using X975.Radar.Drawing.OverlaySettings;
using X975.Radar.Utility;
using GameOverlay.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using X975.Radar.GameObjects.LocalPlayer;

namespace X975.Radar.Drawers
{
    public class DungeonsDrawerer : IDrawerer
    {
        private readonly ConfigHandler configHandler = ConfigHandler.Source;
        private readonly RadarOverlayBrushesDictionary brushesDictionary;
        private readonly Graphics gfx;

        private readonly LocalPlayerHandler localPlayerHandler;
        private readonly DungeonsHandler dungeonsHandler;

        public DungeonsDrawerer(Graphics gfx, RadarOverlayBrushesDictionary brushesDictionary, LocalPlayerHandler localPlayerHandler, DungeonsHandler dungeonsHandler)
        {
            this.gfx = gfx;
            this.brushesDictionary = brushesDictionary;

            this.localPlayerHandler = localPlayerHandler;
            this.dungeonsHandler = dungeonsHandler;
        }

        public async Task DrawAsync()
        {
            lock (dungeonsHandler.dungeonsList)
            {
                foreach (Dungeon d in dungeonsHandler.dungeonsList.Values)
                {
                    Vector2 pos = (d.Position - localPlayerHandler.localPlayer.Position).Rotate();

                    switch (d.Type)
                    {
                        case DungeonType.Solo:

                            if (configHandler.config.SoloDungeon)
                                gfx.DrawIconDot(brushesDictionary._chargesColors[d.Charges], brushesDictionary._dungeonsImages["SOLO"], pos, configHandler.config.DungeonsDotSize);

                            break;

                        case DungeonType.Corrupted:

                            if (configHandler.config.CorruptedDungeon)
                                gfx.DrawIconDot(brushesDictionary._chargesColors[d.Charges], brushesDictionary._dungeonsImages["CORRUPT"], pos, configHandler.config.DungeonsDotSize);

                            break;

                        case DungeonType.Hellgate:

                            if (configHandler.config.HellDungeon)
                                gfx.DrawIconDot(brushesDictionary._chargesColors[d.Charges], brushesDictionary._dungeonsImages["HELLGATE"], pos, configHandler.config.DungeonsDotSize);

                            break;

                        case DungeonType.Group:

                            if (configHandler.config.GroupDungeon)
                                gfx.DrawIconDot(brushesDictionary._chargesColors[d.Charges], brushesDictionary._dungeonsImages["GROUP"], pos, configHandler.config.DungeonsDotSize);

                            break;
                    }
                }
            }
        }
    }
}
