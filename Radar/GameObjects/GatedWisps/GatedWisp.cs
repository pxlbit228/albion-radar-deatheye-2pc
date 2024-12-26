using System.Numerics;
using System.Reflection;

namespace X975.Radar.GameObjects.GatedWisps
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public class GatedWisp
    {
        public GatedWisp(int id, Vector2 position)
        {
            Id = id;
            Position = position;
        }

        public int Id { get; set; }
        public Vector2 Position { get; set; }
    }
}
