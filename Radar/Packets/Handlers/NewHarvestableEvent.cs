using Albion.Network;
using X975.Radar.Utility;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace X975.Radar.Packets.Handlers
{
    public class NewHarvestableEvent : BaseEvent
    {
        byte[] offsets = Init.PacketOffsets.NewHarvestableObject;

        public NewHarvestableEvent(Dictionary<byte, object> parameters): base(parameters)
        {
            Id = Convert.ToInt32(parameters[offsets[0]]);

            Type = Convert.ToInt32(parameters[offsets[1]]);
            Tier = Convert.ToInt32(parameters[offsets[2]]);

            Position = Additions.fromFArray((float[])parameters[offsets[3]]);

            Count = parameters.ContainsKey(offsets[4]) ? Convert.ToInt32(parameters[offsets[4]]) : 0;
            Charge = parameters.ContainsKey(offsets[5]) ? Convert.ToInt32(parameters[offsets[5]]) : 0;
        }

        public int Id { get; }

        public int Type { get; }
        public int Tier { get; }

        public Vector2 Position { get; }

        public int Count { get; }
        public int Charge { get; }
    }
}
