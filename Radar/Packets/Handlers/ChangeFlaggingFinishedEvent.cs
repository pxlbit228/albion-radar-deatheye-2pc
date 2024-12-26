using Albion.Network;
using X975.Radar.Utility;
using System;
using System.Collections.Generic;

namespace X975.Radar.Packets.Handlers
{
    public class ChangeFlaggingFinishedEvent : BaseEvent
    {
        byte[] offsets = Init.PacketOffsets.ChangeFlaggingFinished;

        public ChangeFlaggingFinishedEvent(Dictionary<byte, object> parameters): base(parameters)
        {
            Id = Convert.ToInt32(parameters[offsets[0]]);
            Faction = (Faction)parameters[offsets[1]];
        }
        
        public int Id { get; }
        public Faction Faction { get; }
    }
}
