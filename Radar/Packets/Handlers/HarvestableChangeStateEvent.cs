using Albion.Network;
using System;
using System.Collections.Generic;

namespace X975.Radar.Packets.Handlers
{
    public class HarvestableChangeStateEvent : BaseEvent
    {
        byte[] offsets = Init.PacketOffsets.HarvestableChangeState;

        public HarvestableChangeStateEvent(Dictionary<byte, object> parameters): base(parameters)
        {
            Id = Convert.ToInt32(parameters[offsets[0]]);

            Count = parameters.ContainsKey(offsets[1]) ? Convert.ToInt32(parameters[offsets[1]]) : 0;
            Charge = parameters.ContainsKey(offsets[2]) ? Convert.ToInt32(parameters[offsets[2]]) : 0;
        }

        public int Id { get; }

        public int Count { get; }
        public int Charge { get; }
    }
}
