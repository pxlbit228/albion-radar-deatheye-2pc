using System.Collections.Generic;
using System.Xml;
using X975.Protocol.Connect.Messages.ResponseObj;

namespace X975.Radar.Dependencies.Item
{
    public class ItemData
    {
        public static List<PlayerItems> Load(string filename)
        {
            var document = new XmlDocument();
            document.Load(filename);
            
            var applicableNodes = document.SelectNodes("/items/*[@uniquename]");

            var items = new List<PlayerItems>();
            
            var id = 1;
            
            if (applicableNodes != null)
            {
                foreach (XmlNode item in applicableNodes)
                {
                    var playerItem = new PlayerItems
                    {
                        Id = id++,
                        Name = item.Attributes["uniquename"].Value,
                        Itempower = int.TryParse(item.Attributes["itempower"]?.Value ?? "0", out var ip) ? ip : 0
                    };
                    
                    if (playerItem.Itempower > 0)
                    {
                        items.Add(playerItem);
                    }
                    
                    var enchantments = item.SelectNodes("./enchantments/enchantment");
                    
                    if (enchantments != null)
                    {
                        foreach (XmlNode enchantment in enchantments)
                        {
                            var enchantmentItem = new PlayerItems
                            {
                                Id = id++,
                                Name = item.Attributes["uniquename"].Value + "@" + enchantment.Attributes["enchantmentlevel"].Value,
                                Itempower = int.TryParse(enchantment.Attributes["itempower"]?.Value ?? "0", out var eip) ? eip : 0
                            };
                            
                            if (enchantmentItem.Itempower > 0)
                            {
                                items.Add(enchantmentItem);
                            }
                        }
                    }
                }
            }

            return items;
        }
    }
}