using System.Numerics;
using System.Reflection;
using System.Collections.Concurrent;

namespace X975.Radar.GameObjects.FishNodes
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public class FishNodesHandler
    {
        public ConcurrentDictionary<int, FishNode> fishNodesList = new ConcurrentDictionary<int, FishNode>();

        public void AddFishZone(int id, Vector2 position, int size, int respawnCount)
        {
            lock (fishNodesList)
            {
                if (fishNodesList.ContainsKey(id))
                    fishNodesList.TryRemove(id, out FishNode d);

                fishNodesList.TryAdd(id, new FishNode(id, position, size, respawnCount));
            }
        }

        public void Remove(int id)
        {
            lock (fishNodesList)
                fishNodesList.TryRemove(id, out FishNode d);
        }

        public void Clear()
        {
            lock (fishNodesList)
                fishNodesList.Clear();
        }
    }
}
