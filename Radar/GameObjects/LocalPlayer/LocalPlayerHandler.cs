using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using X975.Radar.Dependencies.Template;
using X975.Radar.Packets.Handlers;
using X975.Radar.Utility;

namespace X975.Radar.GameObjects.LocalPlayer
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public class LocalPlayerHandler
    {
        public LocalPlayer localPlayer = new LocalPlayer();

        private readonly Dictionary<string, Cluster> clusterList = new Dictionary<string, Cluster>();

        public LocalPlayerHandler(Dictionary<string, Cluster> clusterList)
        {
            this.clusterList = clusterList;
        }

        public bool ChangeCluster(string id, DynamicClusterData dynamicClusterData = null)
        {
            lock (localPlayer)
            {
                if (localPlayer.CurrentCluster.Id == id)
                    return false;
                
                localPlayer.CurrentCluster.Id = id;

                if (clusterList != null && clusterList.ContainsKey(id))
                {
                    Cluster cluster = clusterList[id];

                    localPlayer.DynamicClusterData = null;
                    localPlayer.CurrentCluster.DisplayName = cluster.DisplayName;
                    localPlayer.CurrentCluster.ClusterColor = cluster.ClusterColor;
                }
                else
                {
                    localPlayer.CurrentCluster.ClusterColor = ClusterColor.Unknown;
                    localPlayer.CurrentCluster.DisplayName = "Unknown";
                }

                if (id.Contains("@"))
                {
                    string[] temp = id.Split('@');

                    if (string.IsNullOrEmpty(temp[1]) || string.IsNullOrEmpty(temp[2]))
                    {
                        localPlayer.CurrentCluster.Subtype = ClusterSubtype.Unknown;
                        localPlayer.CurrentCluster.LobbyID = string.Empty;
                    }

                    switch (temp[1])
                    {
                        case "MISTS":
                            localPlayer.CurrentCluster.DisplayName = "Mists Dungeon";
                            localPlayer.CurrentCluster.Subtype = ClusterSubtype.Mist;
                            break;

                        case "MISTSDUNGEON":
                            localPlayer.CurrentCluster.DisplayName = "Knightfall Abbey";
                            localPlayer.CurrentCluster.Subtype = ClusterSubtype.Abbey;
                            break;

                        default:
                            localPlayer.CurrentCluster.DisplayName = temp[1];
                            localPlayer.CurrentCluster.Subtype = ClusterSubtype.Unknown;
                            break;
                    }

                    if (localPlayer.CurrentCluster.LobbyID != temp[2])
                    {
                        localPlayer.CurrentCluster.LobbyID = temp[2];
                        localPlayer.DynamicClusterData = dynamicClusterData;
                        UpdateChests();
                    }
                }
                else
                {
                    localPlayer.CurrentCluster.Subtype = ClusterSubtype.Unknown;
                    localPlayer.CurrentCluster.LobbyID = string.Empty;
                }

                return true;
            }
        }

        private void UpdateChests()
        {
            try
            {
                var clusterData = localPlayer.DynamicClusterData;
                
                // we need to know cluster color to get the correct chest, but it is TODO
                
                if (clusterData != null)
                {
                    var chests = new List<string>();

                    clusterData.TemplateInstances?
                        .ForEach(instance =>
                        {
                            // dirty, but should work

                            var template = TemplateData.GetTemplate("DEAD", instance.TemplateName) ??
                                           TemplateData.GetTemplate("RED", instance.TemplateName) ??
                                           TemplateData.GetTemplate("GREEN", instance.TemplateName) ??
                                           TemplateData.GetTemplate("NONE", instance.TemplateName);

                            if (template != null)
                            {
                                var foundChests = template.TilesRoot?.LayerGroups?
                                    .Where(layerGroup => layerGroup.Layers != null)
                                    .SelectMany(layerGroup => layerGroup.Layers)
                                    .Where(layer => layer.Tiles != null && instance.Layers.Contains(layer.Id))
                                    .SelectMany(layer => layer.Tiles!)
                                    .Select(tile => GetChest(tile.Name))
                                    .Where(e => e != null)
                                    .ToList();
                                
                                if (foundChests != null)
                                {
                                    chests.AddRange(foundChests);
                                }
                            }
                        });
                    
                    localPlayer.Chests = chests;
                }
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Can't update chests");
            }
        }

        private static string GetChest(string name)
        {
            var type = name.Contains("BOOKCHEST")
                ? "Book"
                : name.Contains("LOOTCHEST")
                    ? "Loot"
                    : null;

            if (type == null)
            {
                return null;
            }

            var rarity = name.Contains("_LEGENDARY")
                ? "Legendary"
                : name.Contains("_RARE")
                    ? "Rare"
                    : name.Contains("_UNCOMMON")
                        ? "Uncommon"
                        : name.Contains("_STANDARD")
                            ? "Standard"
                            : "Unknown";

            return type + " - " + rarity;
        }

        public void UpdateClusterObjectives(Dictionary<int, ClusterObjective> clusterObjectives)
        {
            lock (localPlayer)
            {
                localPlayer.CurrentCluster.ClusterObjectives = clusterObjectives;
            }
        }

        public void UpdateClusterTimeCycle(DateTime timeCycle)
        {
            lock (localPlayer)
            {
                localPlayer.CurrentCluster.TimeCycle = timeCycle;
            }
        }

        public void UpdateInfo(int id, string nickname, string guild, string alliance, Faction faction,
            Vector2 position)
        {
            lock (localPlayer)
            {
                localPlayer.Id = id;
                localPlayer.Nickname = nickname;
                localPlayer.Guild = guild.Length > 1 ? guild : "!!!!";
                localPlayer.Alliance = alliance.Length > 1 ? alliance : "!!!!";

                localPlayer.Faction = faction;

                localPlayer.IsStanding = true;
                localPlayer.Position = position;
            }
        }

        public void Move(Vector2 position, Vector2 newPosition, float speed, DateTime time)
        {
            lock (localPlayer)
            {
                localPlayer.IsStanding = (localPlayer.Position - position).Magnitude() <= 0.05;
                localPlayer.Position = position;
                localPlayer.Speed = speed;
                localPlayer.Time = time;
                localPlayer.NewPosition = newPosition;
            }
        }

        public void SetFaction(int id, Faction faction)
        {
            lock (localPlayer)
            {
                if (localPlayer.Id == id)
                    localPlayer.Faction = faction;
            }
        }

        public void SyncPosition()
        {
            lock (localPlayer)
            {
                if (localPlayer.IsStanding) return;

                Vector2 posDiff = localPlayer.Position - localPlayer.NewPosition;

                if (posDiff == Vector2.Zero) return;

                localPlayer.Position -= posDiff * (float)((DateTime.UtcNow - localPlayer.Time).TotalSeconds /
                                                          (posDiff.Magnitude() / (localPlayer.Speed / 10)));
            }
        }
    }
}