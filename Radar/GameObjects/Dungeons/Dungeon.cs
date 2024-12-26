using System.Numerics;
using System.Reflection;

namespace X975.Radar.GameObjects.Dungeons
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public class Dungeon
    {
        public Dungeon(int id, string type, Vector2 position, int charges)
        {
            Id = id;
            Type = GetType(type);
            Position = position;
            Charges = charges;
        }

        private DungeonType GetType(string type)
        {
            if (type.Contains("CORRUPTED")) return DungeonType.Corrupted;
            if (type.Contains("MISTS")) return DungeonType.Mists;
            if (type.Contains("HELLGATE")) return DungeonType.Hellgate;

            if (type.Contains("_SOLO")) return DungeonType.Solo;

            return DungeonType.Group;
        }

        public int Id { get; }
        public DungeonType Type { get; }
        public Vector2 Position { get; set; }
        public int Charges { get; set; }
    }

    public enum DungeonType : byte
    {
        Solo,
        Group,
        Corrupted,
        Hellgate,
        Mists
    }
}
