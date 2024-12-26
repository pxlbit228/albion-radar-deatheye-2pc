using System.Numerics;
using System.Reflection;

namespace X975.Radar.GameObjects.FishNodes
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public class FishNode
    {
        public FishNode(int id, Vector2 position, int size, int respawnCount)
        {
            Id = id;
            Position = position;
            Size = size;
            RespawnCount = respawnCount;
        }
        public int Id { get; set; }
        public Vector2 Position { get; set; }
        public int Size { get; set; }
        public int RespawnCount { get; set; }
    }
}
