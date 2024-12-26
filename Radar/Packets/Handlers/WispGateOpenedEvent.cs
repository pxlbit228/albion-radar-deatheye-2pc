using Albion.Network;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace X975.Radar.Packets.Handlers
{
    public class WispGateOpenedEvent : BaseEvent
    {
        public WispGateOpenedEvent(Dictionary<byte, object> parameters) : base(parameters)
        {
            Id = Convert.ToInt32(parameters[0]);
        }

        public int Id { get; }
    }
}
