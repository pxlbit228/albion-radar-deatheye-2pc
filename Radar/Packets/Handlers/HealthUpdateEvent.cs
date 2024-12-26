using Albion.Network;
using System;
using System.Collections.Generic;

namespace X975.Radar.Packets.Handlers
{
    class HealthUpdateEvent : BaseEvent
    {
        byte[] offsets = Init.PacketOffsets.HealthUpdateEvent;

        public HealthUpdateEvent(Dictionary<byte, object> parameters) : base(parameters)
        {
            Id = Convert.ToInt32(parameters[offsets[0]]);

            Health = parameters.ContainsKey(offsets[1]) ? Convert.ToInt32(parameters[offsets[1]]) : 0;
        }

        public int Id { get; }

        public int Health { get; }
    }
}
