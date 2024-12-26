using Albion.Network;
using X975.Radar.Utility;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;

namespace X975.Radar.Packets.Handlers
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public class JoinResponseOperation : BaseOperation
    {
        byte[] offsets = Init.PacketOffsets.JoinResponse;

        public JoinResponseOperation(Dictionary<byte, object> parameters) : base(parameters)
        {
            Id = Convert.ToInt32(parameters[offsets[0]]);
            Nick = parameters[offsets[1]] as string;

            Guild = parameters.ContainsKey(offsets[2]) ? parameters[offsets[2]] as string : "!";
            Alliance = parameters.ContainsKey(offsets[3]) ? parameters[offsets[3]] as string : "!";

            Location = parameters[offsets[4]] as string;

            Faction = (Faction)parameters[offsets[5]];

            Position = Additions.fromFArray((float[])parameters[offsets[6]]);
        }

        public int Id { get; }
        public string Nick { get; }
        public Faction Faction { get; }
        public string Guild { get; }
        public string Alliance { get; }
        public string Location { get; }
        public Vector2 Position { get; }
    }
}
