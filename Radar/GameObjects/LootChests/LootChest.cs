using System.Numerics;
using System.Reflection;

namespace X975.Radar.GameObjects.LootChests
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public class LootChest
    {
        public LootChest(int id, Vector2 position, string name, int charge)
        {
            Id = id;
            Position = position;
            Name = name;
            Charge = charge;
        }

        public int Id { get; set; }
        public Vector2 Position { get; set; }
        public string Name { get; set; }
        public int Charge { get; set; }
    }
}
