using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using X975.Radar.Packets.Handlers;
using X975.Radar.Utility;

namespace X975.Radar.GameObjects.LocalPlayer
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public class LocalPlayer
    {
        public LocalPlayer()
        {
            Guild = "!!!!";
            Alliance = "!!!!";
            Faction = Faction.NoPVP;

            IsStanding = true;
            Position = Vector2.Zero;
            NewPosition = Vector2.Zero;
            Speed = 5.5f;
            
            CurrentCluster = new Cluster()
            {
                ClusterColor = ClusterColor.Unknown,
                DisplayName = "Unknown"
            };
        }

        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Guild { get; set; }
        public string Alliance { get; set; }
        public Faction Faction { get; set; }

        public bool IsStanding { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 NewPosition { get; set; }
        public float Speed { get; set; }
        public DateTime Time { get; set; }
        
        public Cluster CurrentCluster { get; set; }
        
        public DynamicClusterData DynamicClusterData { get; set; }
        
        public List<string> Chests { get; set; }
    }

    public class Cluster
    {
        public string Id { get; set; }
        public ClusterColor ClusterColor { get; set; }
        public string DisplayName { get; set; }

        public ClusterSubtype Subtype { get; set; }
        public string LobbyID { get; set; }
        public Dictionary<int, ClusterObjective> ClusterObjectives { get; set; }
        public DateTime TimeCycle { get; set; }

        public int PlayersCount { get; set; }
    }

    public class ClusterObjective
    {
        public int Id { get; set; }
        public DateTime Timer { get; set; }
        public Vector2 Position { get; set; }
        public int Charge {get; set;}
        public string Type { get; set;}
    }

    [Obfuscation(Feature = "renaming", Exclude = true, ApplyToMembers = true)]
    public enum ClusterColor : byte
    {
        City,
        Default,
        Black,
        Unknown,
    }

    [Obfuscation(Feature = "renaming", Exclude = true, ApplyToMembers = true)]
    public enum ClusterSubtype : byte
    {
        Unknown,
        Mist,
        Abbey
    }
}
