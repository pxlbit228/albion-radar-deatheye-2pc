using Albion.Network;
using System.Collections.Generic;
using System.Numerics;
using System;
using X975.Radar.GameObjects.LocalPlayer;
using X975.Radar.Utility;
using System.Linq;

namespace X975.Radar.Packets.Handlers
{
    public class LoadClusterObjectsEvent : BaseEvent
    {
        public LoadClusterObjectsEvent(Dictionary<byte, object> parameters) : base(parameters)
        {
            if (((byte[][])parameters[1]).Length != 0)
            {
                ClusterObjectives = new Dictionary<int, ClusterObjective>();

                for (int i = 0; i < ((byte[][])parameters[1]).Length; i++)
                {
                    int id = ConvertId(parameters, i);
                    byte charge = ((byte[])parameters[2])[i];
                    Vector2 position = Additions.fromValues(((float[])parameters[5])[i], ((float[])parameters[5])[i + 1]);
                    string type = ((string[])parameters[8])[i];
                    DateTime time = type == "CHEST" ? new DateTime(((long[])parameters[6])[i]) : new DateTime(((long[])parameters[7])[i]);

                    if (type != "CHEST" && type != "WISPS") 
                        continue;

                    ClusterObjectives.Add(id, new ClusterObjective()
                    {
                        Id = id,
                        Charge = charge,
                        Position = position,
                        Timer = time,
                        Type = type,
                    });
                }

                if (ClusterObjectives.Count() == 0)
                    ClusterObjectives = null;
            }
        }

        public Dictionary<int, ClusterObjective> ClusterObjectives { get; set; }

        private int ConvertId(Dictionary<byte, object> value, int i)
        {
            int id = 0;

            switch (value[4])
            {
                case byte[] byteArr:
                    id = byteArr[i];
                    break;

                case short[] shortArr:
                    id = shortArr[i];
                    break;

                default:
                    id = ((int[])value[4])[i];
                    break;
            }

            return id;
        }
    }
}
