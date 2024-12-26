using System.Numerics;
using System.Reflection;
using System.Collections.Concurrent;

namespace X975.Radar.GameObjects.GatedWisps
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public class GatedWispsHandler
    {
        public ConcurrentDictionary<int, GatedWisp> gatedWispsList = new ConcurrentDictionary<int, GatedWisp>();

        public void AddWispInGate(int id, Vector2 position)
        {
            lock (gatedWispsList)
            {
                if (gatedWispsList.ContainsKey(id))
                    gatedWispsList.TryRemove(id, out GatedWisp w);

                gatedWispsList.TryAdd(id, new GatedWisp(id, position));
            } 
        }

        public void Remove(int id)
        {
            lock (gatedWispsList)
                gatedWispsList.TryRemove(id, out GatedWisp w);
        }

        public void Clear()
        {
            lock (gatedWispsList)
                gatedWispsList.Clear();
        }
    }
}
