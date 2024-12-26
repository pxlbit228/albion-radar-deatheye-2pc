using Albion.Network;
using X975.Radar.Utility;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace X975.Radar.Packets.Handlers
{
    public class NewLootChestEvent : BaseEvent
    {
        byte[] offsets = Init.PacketOffsets.NewLootChest;

        public NewLootChestEvent(Dictionary<byte, object> parameters) : base(parameters)
        {
            Id = Convert.ToInt32(parameters[offsets[0]]);
            Position = Additions.fromFArray((float[])parameters[offsets[1]]);
            Name = (string)parameters[offsets[2]];
            EnchLvl = 0;
        }

        public int Id { get; }
        public Vector2 Position { get; }
        public string Name { get; }
        public int EnchLvl { get; }
    }
}
