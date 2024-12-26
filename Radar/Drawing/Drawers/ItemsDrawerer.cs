using X975.Settings;
using X975.Radar.GameObjects.Players;
using X975.Radar.Utility;
using GameOverlay.Drawing;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X975.Tools;
using X975.Radar.GameObjects.LocalPlayer;
using X975.Radar.OverlaySettings;

namespace X975.Radar.Drawers
{
    public class ItemsDrawerer : IDrawerer
    {
        private readonly ConfigHandler configHandler = ConfigHandler.Source;
        private readonly Graphics gfx;
        private readonly ItemsOverlayBrushesDictionary radarOverlayBrushesDictionary;

        private readonly LocalPlayerHandler localPlayerHandler;
        private readonly PlayersHandler playersHandler;

        private int yOffset = 0;
        private int startX = 0;
        private int xOffset = 0;
        private int lines = 0;

        public ItemsDrawerer(Graphics gfx, ItemsOverlayBrushesDictionary radarOverlayBrushesDictionary, LocalPlayerHandler localPlayerHandler, PlayersHandler playersHandler)
        {
            this.radarOverlayBrushesDictionary = radarOverlayBrushesDictionary;
            this.gfx = gfx;

            this.localPlayerHandler = localPlayerHandler;
            this.playersHandler = playersHandler;
        }

        public async Task DrawAsync()
        {
            yOffset = 0;
            xOffset = 0;

            if (configHandler.config.EspEnabled && configHandler.config.ShowItems)
            {
                lines = 0;

                lock (playersHandler.playersList)
                {
                    foreach (Player p in playersHandler.playersList.Values)
                    {
                        if (lines == configHandler.config.LinesCount) return;

                        #region CUSTOM LISTS

                        if (configHandler.config.FriendlyLists)
                        {
                            if (configHandler.config.FriendlyPlayersList.Contains(p.Name, StringComparer.OrdinalIgnoreCase))
                            {
                                ItemsDrow(configHandler.config.FriendlyPlayer, p);
                                continue;
                            }

                            if (p.Guild == localPlayerHandler.localPlayer.Guild || configHandler.config.FriendlyGuildsList.Contains(p.Guild, StringComparer.OrdinalIgnoreCase))
                            {
                                ItemsDrow(configHandler.config.FriendlyGuild, p);
                                continue;
                            }

                            if (p.Alliance == localPlayerHandler.localPlayer.Alliance || configHandler.config.FriendlyAlliancesList.Contains(p.Alliance, StringComparer.OrdinalIgnoreCase))
                            {
                                ItemsDrow(configHandler.config.FriendlyAlliance, p);
                                continue;
                            }
                        }

                        if (configHandler.config.EnemyLists)
                        {
                            if (configHandler.config.EnemyPlayersList.Contains(p.Name, StringComparer.OrdinalIgnoreCase))
                            {
                                ItemsDrow(configHandler.config.EnemyPlayer, p);
                                continue;
                            }

                            if (configHandler.config.EnemyGuildsList.Contains(p.Guild, StringComparer.OrdinalIgnoreCase))
                            {
                                ItemsDrow(configHandler.config.EnemyGuild, p);
                                continue;
                            }

                            if (configHandler.config.EnemyAlliancesList.Contains(p.Alliance, StringComparer.OrdinalIgnoreCase))
                            {
                                ItemsDrow(configHandler.config.EnemyAlliance, p);
                                continue;
                            }
                        }

                        #endregion

                        #region FACTION WAR

                        if (configHandler.config.FactionWar && (localPlayerHandler.localPlayer.Faction != Faction.NoPVP && localPlayerHandler.localPlayer.Faction != Faction.PVP))
                        {
                            if (p.Faction == Faction.NoPVP) continue;

                            if (p.Faction == localPlayerHandler.localPlayer.Faction)
                            {
                                ItemsDrow(configHandler.config.FriendlyFaction, p);
                            }
                            else if (p.Faction != localPlayerHandler.localPlayer.Faction && p.Faction != Faction.PVP)
                            {
                                ItemsDrow(configHandler.config.EnemyFaction, p);
                            }
                            else
                            {
                                ItemsDrow(configHandler.config.Pvp, p);
                            }

                            continue;
                        }

                        #endregion

                        if (localPlayerHandler.localPlayer.CurrentCluster.ClusterColor == ClusterColor.Default)
                        {
                            switch (p.Faction)
                            {
                                case Faction.NoPVP:
                                    ItemsDrow(configHandler.config.NoPvp, p);
                                    break;

                                case Faction.Martlock:
                                case Faction.Lymhurst:
                                case Faction.Bridjewatch:
                                case Faction.ForthSterling:
                                case Faction.Thetford:
                                case Faction.Caerleon:
                                    ItemsDrow(configHandler.config.Faction, p);
                                    break;

                                case Faction.PVP:
                                    ItemsDrow(configHandler.config.Pvp, p);
                                    break;
                            }
                        }
                        else
                        {
                            ItemsDrow(configHandler.config.Pvp, p);
                        }
                    }
                }
            }
        }

        private async void ItemsDrow(object[] settings, Player player)
        {
            if (!Convert.ToBoolean(settings[0])
                || !Convert.ToBoolean(settings[21]) 
                || player.Equipment == null 
                || configHandler.config.EquipmentParts.All(x=> x == false)) 
                return;

            switch (configHandler.config.ItemsStyle)
            {
                case 0:
                    ListStyle(player);
                    break;

                case 1:
                    LineStyle(player);
                    break;
            }
        }

        private void ListStyle(Player player)
        {
            lines += 1;

            xOffset = 0;

            gfx.DrawTextWithBackground(radarOverlayBrushesDictionary._fonts["Main"], radarOverlayBrushesDictionary._brushes["White"],
                radarOverlayBrushesDictionary._brushes["Black"], xOffset, yOffset + 1, " " + player.Name);

            xOffset += 15 + (int)gfx.MeasureString(radarOverlayBrushesDictionary._fonts["Main"], player.Name).X;

            if (configHandler.config.ShowHealth)
                DrawHealth(player.Health);

            xOffset += 160;

            if (configHandler.config.ShowMinIP)
                gfx.DrawTextWithBackground(radarOverlayBrushesDictionary._fonts["Main"], radarOverlayBrushesDictionary._brushes["White"],
                    radarOverlayBrushesDictionary._brushes["Black"], xOffset, yOffset + 1, $" Min IP : {player.Equipment.AllItemPower}");

            yOffset += 25;
            xOffset = 0;

            for (int i = 0; i < 8; i++)
            {
                if (!configHandler.config.EquipmentParts[i]) continue;

                if (player.Equipment.Items[i].Name == "NULL") continue;

                if (player.Equipment.Items[i].Name == "T1_TRASH" && !configHandler.config.ShowTrash) continue;

                if (radarOverlayBrushesDictionary._itemImage.ContainsKey(player.Equipment.Items[i].Name))
                {
                    gfx.DrawImage(radarOverlayBrushesDictionary._itemImage[player.Equipment.Items[i].Name], Rectangle.Create(xOffset - 5, yOffset, 50, 50), 1, true);

                    xOffset += 45;
                }
                else
                {
                    if (File.Exists(Pathfinder.mainFolder + "\\ITEMS\\" + player.Equipment.Items[i].Name + ".png"))
                    {
                        radarOverlayBrushesDictionary._itemImage.Add(player.Equipment.Items[i].Name, gfx.CreateImage(Pathfinder.mainFolder + "\\ITEMS\\" + player.Equipment.Items[i].Name + ".png"));

                        gfx.DrawImage(radarOverlayBrushesDictionary._itemImage[player.Equipment.Items[i].Name], Rectangle.Create(xOffset - 5, yOffset, 50, 50), 1, true);

                        xOffset += 45;
                    }
                    else
                    {
                        if (configHandler.config.ShowTrash)
                        {
                            gfx.DrawImage(radarOverlayBrushesDictionary._itemImage["T1_TRASH"], Rectangle.Create(xOffset - 5, yOffset, 50, 50), 1, true);

                            xOffset += 45;
                        }
                    }
                }
            }

            yOffset += 55;
        }

        private void LineStyle(Player player)
        {
            lines += 1;

            yOffset = 0;
            startX = xOffset;

            gfx.DrawTextWithBackground(radarOverlayBrushesDictionary._fonts["Main"], radarOverlayBrushesDictionary._brushes["White"],
                radarOverlayBrushesDictionary._brushes["Black"], xOffset, yOffset + 1, " " + player.Name);

            xOffset += 15 + (int)gfx.MeasureString(radarOverlayBrushesDictionary._fonts["Main"], player.Name).X;

            if (configHandler.config.ShowHealth)
                DrawHealth(player.Health);

            xOffset += 160;

            if (configHandler.config.ShowMinIP)
            {
                gfx.DrawTextWithBackground(radarOverlayBrushesDictionary._fonts["Main"], radarOverlayBrushesDictionary._brushes["White"],
                    radarOverlayBrushesDictionary._brushes["Black"], xOffset, yOffset + 1, $" Min IP : {player.Equipment.AllItemPower}");

                xOffset += 10 + (int)gfx.MeasureString(radarOverlayBrushesDictionary._fonts["Main"], $" Min IP : {player.Equipment.AllItemPower}").X;
            }

            yOffset += 25;

            for (int i = 0; i < 8; i++)
            {
                if (!configHandler.config.EquipmentParts[i]) continue;

                if (player.Equipment.Items[i].Name == "NULL") continue;

                if (player.Equipment.Items[i].Name == "T1_TRASH" && !configHandler.config.ShowTrash) continue;

                if (radarOverlayBrushesDictionary._itemImage.ContainsKey(player.Equipment.Items[i].Name))
                {
                    gfx.DrawImage(radarOverlayBrushesDictionary._itemImage[player.Equipment.Items[i].Name], Rectangle.Create(startX - 5, yOffset, 50, 50), 1, true);

                    startX += 45;
                }
                else
                {
                    if (File.Exists(Pathfinder.mainFolder + "\\ITEMS\\" + player.Equipment.Items[i].Name + ".png"))
                    {
                        radarOverlayBrushesDictionary._itemImage.Add(player.Equipment.Items[i].Name, gfx.CreateImage(Pathfinder.mainFolder + "\\ITEMS\\" + player.Equipment.Items[i].Name + ".png"));

                        gfx.DrawImage(radarOverlayBrushesDictionary._itemImage[player.Equipment.Items[i].Name], Rectangle.Create(startX - 5, yOffset, 50, 50), 1, true);

                        startX += 45;
                    }
                    else
                    {
                        if (configHandler.config.ShowTrash)
                        {
                            gfx.DrawImage(radarOverlayBrushesDictionary._itemImage["T1_TRASH"], Rectangle.Create(startX - 5, yOffset, 50, 50), 1, true);

                            startX += 45;
                        }
                    }
                }
            }

            if (startX > xOffset)
            {
                xOffset = startX + 10;
            }
            else 
            {
                xOffset += 10;
            }   
        }

        private void DrawHealth(Health health)
        {
            gfx.FillRectangle(radarOverlayBrushesDictionary._brushes["UnderHealth"], xOffset, yOffset, xOffset + 150, yOffset + 24);

            gfx.FillRectangle(radarOverlayBrushesDictionary._brushes["Red"], xOffset, yOffset, xOffset + health.Percent * 1.5f, yOffset + 24);

            gfx.DrawLine(radarOverlayBrushesDictionary._brushes["Black"], xOffset + 30, yOffset, xOffset + 30, yOffset + 24, 2);
            gfx.DrawLine(radarOverlayBrushesDictionary._brushes["Black"], xOffset + 60, yOffset, xOffset + 60, yOffset + 24, 2);
            gfx.DrawLine(radarOverlayBrushesDictionary._brushes["Black"], xOffset + 90, yOffset, xOffset + 90, yOffset + 24, 2);
            gfx.DrawLine(radarOverlayBrushesDictionary._brushes["Black"], xOffset + 120, yOffset, xOffset + 120, yOffset + 24, 2);

            gfx.DrawTextCentered(radarOverlayBrushesDictionary._fonts["Main"], 
                radarOverlayBrushesDictionary._brushes["White"], 
                xOffset + 75, yOffset + 2, $"{health.Value}/{health.MaxValue}");

            gfx.DrawRectangle(radarOverlayBrushesDictionary._brushes["Black"], xOffset, yOffset, xOffset + 150, yOffset + 24, 1);
        }
    }
}
