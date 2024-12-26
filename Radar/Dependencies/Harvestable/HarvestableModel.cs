using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace X975.Radar.Dependencies.Harvestable
{
    [XmlRoot(ElementName = "Charge")]
    public class HarvestableChargeMetaData
    {
        [XmlAttribute(AttributeName = "level")]
        public string Level { get; set; }

        [XmlAttribute(AttributeName = "yield")]
        public float Yield { get; set; }
    }

    [XmlRoot(ElementName = "Tier")]
    public class HarvestableTierMetaData
    {
        [XmlElement(ElementName = "Charge")] public List<HarvestableChargeMetaData> Charge { get; set; }

        [XmlAttribute(AttributeName = "tier")] public int Tier { get; set; }

        [XmlAttribute(AttributeName = "item")] public string Item { get; set; }
    }

    [XmlRoot(ElementName = "Harvestable")]
    public class HarvestableMetaData
    {
        [XmlElement(ElementName = "Tier")] public List<HarvestableTierMetaData> Tier { get; set; }

        [XmlAttribute(AttributeName = "name")] public string Name { get; set; }

        [XmlAttribute(AttributeName = "resource")]
        public string Resource { get; set; }

        [XmlIgnore] public readonly Dictionary<long, long> MaxChargesByTier = new Dictionary<long, long>();
    }

    [XmlRoot(ElementName = "AO-Harvestables")]
    public class Root
    {
        [XmlElement(ElementName = "Harvestable")]
        public List<HarvestableMetaData> Harvestables { get; set; }
    }

    public static class HarvestableData
    {
        // shit code
        
        public static Dictionary<int, HarvestableMetaData> Harvestable;
        public static Dictionary<string, HarvestableMetaData> HarvestableByName;
        
        public static Dictionary<int, string> Load(string filename)
        {
            var root = XmlTools.Deserialize<Root>(filename);

            var data = new Dictionary<int, HarvestableMetaData>();
            var dataByName = new Dictionary<string, HarvestableMetaData>();

            var i = 0;
            foreach (var harvestable in root.Harvestables)
            {
                harvestable.Tier?.ForEach(tier =>
                {
                    var maxCharges = tier.Charge?
                        .Select(charge =>
                        {
                            var level = charge.Level?.Split('-');
                            if (level != null && level.Length != 0)
                            {
                                return long.Parse(level[level.Length - 1]);
                            }

                            return 0;
                        })
                        .Max() ?? 0;
                    harvestable.MaxChargesByTier[tier.Tier] = maxCharges;
                });

                data[i++] = harvestable;
                dataByName[harvestable.Name] = harvestable;
            }
            
            Harvestable = data;
            HarvestableByName = dataByName;

            return data
                .Where(e => !string.IsNullOrWhiteSpace(e.Value.Resource))
                .ToDictionary(e => e.Key, e => e.Value.Resource);
        }
    }
}