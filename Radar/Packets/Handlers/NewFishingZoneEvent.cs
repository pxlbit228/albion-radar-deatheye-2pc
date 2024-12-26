using Albion.Network;
using X975.Radar.Utility;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace X975.Radar.Packets.Handlers
{
    public class NewFishingZoneEvent : BaseEvent
    {
        byte[] offsets = Init.PacketOffsets.NewFishingZoneObject;

        public NewFishingZoneEvent(Dictionary<byte, object> parameters) : base(parameters)
        {
            Id = Convert.ToInt32(parameters[offsets[0]]);

            Position = Additions.fromFArray((float[])parameters[offsets[1]]);

            Size = parameters.ContainsKey(offsets[2]) ? Convert.ToInt32(parameters[offsets[2]]) : 0;
            RespawnCount = parameters.ContainsKey(offsets[3]) ? Convert.ToInt32(parameters[offsets[3]]) : 0;
        }

        public int Id { get; }

        public Vector2 Position { get; }

        public int Size { get; }
        public int RespawnCount { get; }
    }
}
