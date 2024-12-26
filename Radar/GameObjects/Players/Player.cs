using X975.Radar.Utility;
using System.Numerics;
using System;
using System.Collections.Generic;
using X975.Protocol.Connect.Messages.ResponseObj;
using System.Reflection;

namespace X975.Radar.GameObjects.Players
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public class Player
    {
        public Player(int id, string name, string guild, string alliance, Vector2 position, Health health, Faction faction, Equipment equipment, int[] spells)
        {
            Id = id;

            Name = name;
            Guild = guild;
            Alliance = alliance;
            Faction = faction;

            Position = position;

            Health = health;

            Equipment = equipment;
            Spells = spells;
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public string Guild { get; set; }
        public string Alliance { get; set; }

        public bool IsStanding { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 NewPosition { get; set; }
        public float Speed { get; set; }
        public DateTime Time { get; set; }

        public Health Health { get; set; }
        public Faction Faction { get; set; }
        public bool IsMounted { get; set; }

        public Equipment Equipment { get; set; }
        public int[] Spells { get; set; }
    }

    [Obfuscation(Feature = "renaming", Exclude = true, ApplyToMembers = true)]
    public class Equipment
    {
        public Equipment()
        {
            AllItemPower = 0;
            Items = new List<PlayerItems> { };
        }

        public int AllItemPower { get; set; }

        public List<PlayerItems> Items { get; set; }
    }
}
