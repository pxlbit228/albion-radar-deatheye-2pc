using Albion.Network;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace X975.Radar.Packets.Handlers
{
    public class WispGateOpenedEvent : BaseEvent
    {
        byte[] offsets = Init.PacketOffsets.WispGateOpened;
        
        public WispGateOpenedEvent(Dictionary<byte, object> parameters) : base(parameters)
        {
            Id = Convert.ToInt32(parameters[offsets[0]]);
            isCollected = parameters.ContainsKey(offsets[1]) && parameters[offsets[1]].ToString() == "2";
        }

        public int Id { get; }
        
        public bool isCollected { get; }
    }
}
