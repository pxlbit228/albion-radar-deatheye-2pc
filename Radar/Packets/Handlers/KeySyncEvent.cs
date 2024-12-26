using Albion.Network;
using System;
using System.Collections.Generic;

namespace X975.Radar.Packets.Handlers
{
    public class KeySyncEvent : BaseEvent
    {
        byte[] offsets = Init.PacketOffsets.KeySync;

        public KeySyncEvent(Dictionary<byte, object> parameters) : base(parameters)
        {
            Code = parameters.ContainsKey(offsets[0]) ? parameters[offsets[0]] as byte[] : null;
        }

        public byte[] Code { get; }
    }
}
