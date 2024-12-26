using Albion.Network;
using X975.Radar.Utility;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;

namespace X975.Radar.Packets.Handlers
{
    public class NewDungeonEvent : BaseEvent
    {
        byte[] offsets = Init.PacketOffsets.NewDungeonExit;

        public NewDungeonEvent(Dictionary<byte, object> parameters) : base(parameters)
        {
            Id = Convert.ToInt32(parameters[offsets[0]]);

            Position = Additions.fromFArray((float[])parameters[offsets[1]]);

            Type = parameters.ContainsKey(offsets[2]) ? parameters[offsets[2]] as string : "NULL";

            Charges = Convert.ToInt32(parameters[offsets[3]]);
        }

        public int Id { get; }

        public Vector2 Position { get; }

        public string Type { get; }

        public int Charges { get; }
    }
}
