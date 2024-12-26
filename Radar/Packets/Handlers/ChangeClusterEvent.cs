using System;
using X975.Protocol;
using Albion.Network;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Reflection;

namespace X975.Radar.Packets.Handlers
{
    public class ChangeClusterEvent : BaseOperation
    {
        byte[] offsets = Init.PacketOffsets.ChangeCluster;

        public ChangeClusterEvent(Dictionary<byte, object> parameters) : base(parameters)
        {
            LocationId = parameters[offsets[0]] as string;
            Type = parameters.ContainsKey(offsets[1]) ? parameters[offsets[1]] as string : "NULL";
            DynamicClusterData = parameters.ContainsKey(offsets[2]) && parameters[offsets[2]] is byte[]
                ? ReadClusterData(parameters[offsets[2]] as byte[])
                : null;
        }

        public string LocationId { get; }

        public string Type { get; }

        public DynamicClusterData DynamicClusterData { get; }


        private static DynamicClusterData ReadClusterData(byte[] data)
        {
            try
            {
                var clusterData = new DynamicClusterData();

                using var ms = new MemoryStream(data);
                using var reader = new BinaryReader(ms);

                clusterData.Type = reader.ReadByte();

                if (clusterData.Type == 1)
                {
                    clusterData.ClusterId = reader.ReadString();
                    clusterData.ClusterName = reader.ReadString();
                    clusterData.UnknownVector1 = new Vector2(reader.ReadSingle(), reader.ReadSingle());
                    clusterData.UnknownVector2 = new Vector2(reader.ReadSingle(), reader.ReadSingle());
                    reader.ReadBytes(0x2B); // unknown
                    clusterData.TemplateInstances = new List<TemplateInstance>();

                    for (int templateInstancesCount = reader.ReadInt32(), i = 0;
                         i < templateInstancesCount;
                         ++i)
                    {
                        var layers = new List<string>();

                        var templateInstanceId = reader.ReadString();
                        var templateName = reader.ReadString();
                        var position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        var rotation = reader.ReadSingle();
                        reader.ReadBytes(1); // unknown

                        for (int layersCount = reader.ReadInt32(), j = 0;
                             j < layersCount;
                             ++j)
                        {
                            layers.Add(reader.ReadString());
                        }

                        clusterData.TemplateInstances.Add(new TemplateInstance
                        {
                            TemplateInstanceId = templateInstanceId,
                            TemplateName = templateName,
                            Position = position,
                            Rotation = rotation,
                            Layers = layers
                        });
                    }

                    clusterData.ClusterName = reader.ReadString();
                    clusterData.ClusterType = reader.ReadString();
                    clusterData.Level = reader.ReadByte();
                    reader.ReadBytes(2); // unknown

                    clusterData.Exits = new List<DynamicExit>();
                    for (var i = 0; i < 2; ++i)
                    {
                        clusterData.Exits.Add(new DynamicExit
                        {
                            Str1 = reader.ReadString(),
                            Str2 = reader.ReadString(),
                            Str3 = reader.ReadString(),
                            Str4 = reader.ReadString()
                        });
                    }

                    return clusterData;
                }
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Can't read cluster data");
            }

            return null;
        }
    }

    public class DynamicClusterData
    {
        public byte Type;
        public string ClusterId;
        public string LongClusterId;
        public Vector2 UnknownVector1;
        public Vector2 UnknownVector2;
        public List<TemplateInstance> TemplateInstances;
        public string ClusterName;
        public string ClusterType;
        public byte Level;
        public byte UnknownByte3;
        public List<DynamicExit> Exits;

        public bool HasNextFloor => Exits?.Count == 2 && !string.IsNullOrWhiteSpace(Exits[1].Str1);
    }

    public class DynamicExit
    {
        public string Str1;
        public string Str2;
        public string Str3;
        public string Str4;
    }

    public class TemplateInstance
    {
        public string TemplateInstanceId;
        public string TemplateName;
        public Vector3 Position;
        public float Rotation;
        public List<string> Layers;
    }
}