using Albion.Network;
using System;
using System.Collections.Generic;

namespace X975.Radar.Packets.Handlers
{
    class MistsPlayerJoinedInfoEvent : BaseEvent
    {
        public MistsPlayerJoinedInfoEvent(Dictionary<byte, object> parameters) : base(parameters)
        {
            TimeCycle = parameters.ContainsKey(5) ? new DateTime(Convert.ToInt64(parameters[5])) : DateTime.MinValue;
        }

        public DateTime TimeCycle { get; }
    }
}
