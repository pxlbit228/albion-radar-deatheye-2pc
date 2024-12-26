using System.Numerics;
using System.Collections.Generic;
using System.Reflection;
using System.Collections.Concurrent;

namespace X975.Radar.GameObjects.LootChests
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public class LootChestsHandler
    {
        public ConcurrentDictionary<int, LootChest> lootChestsList = new ConcurrentDictionary<int, LootChest>();

        public void AddWorldChest(int id, Vector2 position, string name, int enchLvl)
        {
            lock (lootChestsList)
            {
                if (lootChestsList.ContainsKey(id))
                    lootChestsList.TryRemove(id, out LootChest w);

                lootChestsList.TryAdd(id, new LootChest(id, position, name, GetCharge(name, enchLvl)));
            }
        }

        public void Remove(int id)
        {
            lock (lootChestsList)
                lootChestsList.TryRemove(id, out LootChest h);
        }

        public void Clear()
        {
            lock (lootChestsList)
                lootChestsList.Clear();
        }

        public int GetCharge(string name, int enchLvl)
        {
            if (enchLvl > 0) return enchLvl;

            string[] temp = name.Split('_');

            switch (temp[temp.Length - 2])
            {
                case "STANDARD":
                    return 1;

                case "UNCOMMON":
                    return 2;

                case "RARE":
                    return 3;

                case "LEGENDARY":
                    return 4;
            }

            return 0;
        }
    }
}
