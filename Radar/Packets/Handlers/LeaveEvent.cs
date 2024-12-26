using Albion.Network;
using System;
using System.Collections.Generic;

namespace X975.Radar.Packets.Handlers
{
    public class LeaveEvent : BaseEvent
    {
        byte[] offsets = Init.PacketOffsets.Leave;

        public LeaveEvent(Dictionary<byte, object> parameters) : base(parameters)
        {
            Id = parameters.ContainsKey(offsets[0]) ? Convert.ToInt32(parameters[offsets[0]]) : 0;
        }

        public int Id { get; }
    }
}
