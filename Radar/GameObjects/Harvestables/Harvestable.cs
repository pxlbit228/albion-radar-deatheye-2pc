using System.Numerics;
using System.Reflection;

namespace X975.Radar.GameObjects.Harvestables
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public class Harvestable
    {
        public Harvestable(int id, string type, int tier, Vector2 position, int count, int charge)
        {
            Id = id;
            Type = type;
            Tier = tier;
            Position = position;
            Count = count;
            Charge = charge;
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public int Tier { get; set; }
        public Vector2 Position { get; set; }
        public int Count { get; set; }
        public int Charge { get; set; }
        
    }
}
