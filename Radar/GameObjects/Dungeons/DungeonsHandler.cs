using System.Numerics;
using System.Collections.Generic;
using System.Reflection;
using System.Collections.Concurrent;

namespace X975.Radar.GameObjects.Dungeons
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public class DungeonsHandler
    {
        public ConcurrentDictionary<int, Dungeon> dungeonsList = new ConcurrentDictionary<int, Dungeon>();

        public void AddDungeon(int id, string type, Vector2 position, int charges)
        {
            lock (dungeonsList)
            {
                if (dungeonsList.ContainsKey(id))
                    dungeonsList.TryRemove(id, out Dungeon d);

                dungeonsList.TryAdd(id, new Dungeon(id, type, position, charges));
            }
        }

        public void Remove(int id)
        {
            lock (dungeonsList)
                dungeonsList.TryRemove(id, out Dungeon d);
        }

        public void Clear()
        {
            lock (dungeonsList)
                dungeonsList.Clear();
        }
    }
}
