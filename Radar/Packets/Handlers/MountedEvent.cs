using Albion.Network;
using System;
using System.Collections.Generic;

namespace X975.Radar.Packets.Handlers
{
    public class MountedEvent : BaseEvent
    {
        byte[] offsets = Init.PacketOffsets.Mounted;

        public MountedEvent(Dictionary<byte, object> parameters) : base(parameters)
        {
            Id = Convert.ToInt32(parameters[offsets[0]]);

            IsMounted = parameters.ContainsKey(offsets[1]);
        }

        public int Id { get; }

        public bool IsMounted { get; }
    }
}
