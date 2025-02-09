using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using X975.Protocol.Connect.Messages.ResponseObj;
using X975.Radar.Dependencies.Harvestable;

namespace X975.Radar.Dependencies.Mob
{
    [XmlRoot(ElementName = "Loot")]
    public class LootMetaData
    {
        [XmlElement(ElementName = "Harvestable")]
        public ShortHarvestableMetaData Harvestable { get; set; }
    }

    [XmlRoot(ElementName = "Mob")]
    public class MobMetaData
    {
        [XmlAttribute(AttributeName = "uniquename")]
        public string UniqueName { get; set; }

        [XmlAttribute(AttributeName = "tier")] public int Tier { get; set; }

        [XmlAttribute(AttributeName = "npchostility")]
        public string NpcHostility { get; set; }

        [XmlAttribute(AttributeName = "fame")] public int Fame { get; set; }

        [XmlAttribute(AttributeName = "aggroradius")]
        public float AggroRadius { get; set; }

        [XmlAttribute(AttributeName = "faction")]
        public string Faction { get; set; }

        [XmlAttribute(AttributeName = "attacktype")]
        public string AttackType { get; set; }

        [XmlAttribute(AttributeName = "attackrange")]
        public float AttackRange { get; set; }

        [XmlAttribute(AttributeName = "hitpointsmax")]
        public float HitpointsMax { get; set; }

        [XmlAttribute(AttributeName = "hitpointsregeneration")]
        public float HitpointsRegeneration { get; set; }

        [XmlAttribute(AttributeName = "energymax")]
        public float EnergyMax { get; set; }

        [XmlAttribute(AttributeName = "energyregeneration")]
        public float EnergyRegeneration { get; set; }

        [XmlAttribute(AttributeName = "avatar")]
        public string Avatar { get; set; }

        [XmlAttribute(AttributeName = "avatarring")]
        public string AvatarRing { get; set; }

        [XmlAttribute(AttributeName = "dangerstate")]
        public string DangerState { get; set; }

        [XmlAttribute(AttributeName = "namelocatag")]
        public string NameLocaTag { get; set; }

        [XmlAttribute(AttributeName = "mobvalue")]
        public float MobValue { get; set; }

        [XmlAttribute(AttributeName = "category")]
        public string Category { get; set; }

        [XmlAttribute(AttributeName = "mobtypecategory")]
        public string MobTypeCategory { get; set; }

        [XmlElement(ElementName = "Loot")] public LootMetaData Loot { get; set; }
    }

    [XmlRoot(ElementName = "Harvestable")]
    public class ShortHarvestableMetaData
    {
        [XmlAttribute(AttributeName = "type")] public string Type { get; set; }

        [XmlAttribute(AttributeName = "tier")] public int Tier { get; set; }
    }

    [XmlRoot(ElementName = "Mobs")]
    public class Root
    {
        [XmlElement(ElementName = "Mob")] public List<MobMetaData> Mobs { get; set; }
    }

    public static class MobData
    {
        public static List<MobInfo> Load(string filename)
        {
            var root = XmlTools.Deserialize<Root>(filename);

            return root.Mobs
                .Select((e, i) => new MobInfo()
                {
                    Id = i,
                    Tier = e.Tier,
                    Type = ConvertMobType(e),
                    HarvestableType = ConvertHarvestableType(e),
                    Rarity = ConvertRarity(e),
                    Queue = e.UniqueName.Contains("PORTAL_WISP_DUO") ? "Duo" :
                        e.UniqueName.Contains("PORTAL_WISP") ? "Solo" : null,
                    MobName = ConvertMobName(e)
                })
                .ToList();
        }

        public static string ConvertHarvestableType(MobMetaData e)
        {
            if (e.Loot?.Harvestable?.Type != null)
            {
                var data = HarvestableData.HarvestableByName;
                return data.TryGetValue(e.Loot.Harvestable.Type, out var value)
                    ? value.Resource
                    : null;
            }

            return null;
        }

        public static string ConvertMobType(MobMetaData e)
        {
            if (ConvertHarvestableType(e) != null)
                return "HARVESTABLE";

            if (e.UniqueName.Contains("PORTAL_WISP"))
                return "MIST_PORTAL";

            if (e.UniqueName.Contains("EVENT"))
                return "EVENT";

            if (e.UniqueName.Contains("_MOB_MISTS_"))
                return "MIST_BOSS";

            if (e.UniqueName.Contains("AVALON_TREASURE_MINION"))
                return "DRONE";

            if (e.UniqueName.Contains("_CHAMPION") || e.UniqueName.Contains("_MINIBOSS") || e.UniqueName.Contains("_BOSS"))
                return "WORLD_PROCKED";

            // TODO: other types
            // [0] = {string} "MIST_PORTAL"
            // [1] = {string} "EVENT"
            // [2] = {string} "MIST_BOSS"
            // [3] = {string} "HARVESTABLE"
            // [4] = {string} "HIDDEN_TREASURE"
            // [5] = {string} "DRONE"
            // [6] = {string} "WORLD_PROCKED"
            // [7] = {string} "CORRUPTED_MOB"
            // [8] = {string} "CORRUPTED_TRAP"
            // [9] = {string} "MIST_GATE"

            return "NULL";
        }

        public static string ConvertMobName(MobMetaData e)
        {
            if (e.UniqueName.Contains("_CRYSTALSPIDER_"))
                return "CRYSTAL_SPIDER";
            if (e.UniqueName.Contains("_MISTS_SPIDER_"))
                return "SPIDER";
            if (e.UniqueName.Contains("_MISTS_FAIRYDRAGON"))
                return "DRAGON";
            if (e.UniqueName.Contains("_MISTS_GRIFFIN"))
                return "GRIFFIN";
            if (e.UniqueName.Contains("_DEMON_HOOK"))
                return "HOOKER";

            // TODO: for corrupts

            // LAVA: ??
            // GLUE: ??
            // SILENCE: ??
            // KNOCKBACK: ??
            // F: ??
            // G: ??
            // H: ??
            // I: ??

            return null;
        }
        
        public static int ConvertRarity(MobMetaData e)
        {
            if (e.UniqueName.EndsWith("_STANDARD"))
                return 1;
            if (e.UniqueName.EndsWith("_UNCOMMON"))
                return 2;
            if (e.UniqueName.EndsWith("_RARE"))
                return 3;
            if (e.UniqueName.EndsWith("_LEGENDARY"))
                return 4;

            return 0;
        }
    }
}