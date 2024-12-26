using X975.Settings;
using X975.Radar.GameObjects.Players;
using X975.Radar.Drawing.OverlaySettings;
using X975.Radar.Utility;
using System.Numerics;
using GameOverlay.Drawing;
using System;
using System.Linq;
using System.Threading.Tasks;
using X975.Radar.GameObjects.LocalPlayer;

namespace X975.Radar.Drawers
{
    public class PlayersDrawerer : IDrawerer
    {
        private readonly ConfigHandler configHandler = ConfigHandler.Source;
        private readonly Graphics gfx;
        private readonly RadarOverlayBrushesDictionary brushesDictionary;

        private readonly LocalPlayerHandler localPlayerHandler;
        private readonly PlayersHandler playersHandler;

        public PlayersDrawerer(Graphics gfx, RadarOverlayBrushesDictionary brushesDictionary, LocalPlayerHandler localPlayerHandler, PlayersHandler playersHandler)
        {
            this.gfx = gfx;
            this.brushesDictionary = brushesDictionary;

            this.localPlayerHandler = localPlayerHandler;
            this.playersHandler = playersHandler;
        }

        public async Task DrawAsync()
        {
            if (configHandler.config.EspEnabled)
            {
                lock (playersHandler.playersList)
                {
                    foreach (Player p in playersHandler.playersList.Values)
                    {
                        Vector2 pos = (p.Position - localPlayerHandler.localPlayer.Position).Rotate();

                        #region CUSTOM LISTS

                        if (configHandler.config.FriendlyLists)
                        {
                            if (configHandler.config.FriendlyPlayersList.Contains(p.Name, StringComparer.OrdinalIgnoreCase))
                            {
                                PlayerDrow(configHandler.config.FriendlyPlayer, pos, p);
                                continue;
                            }

                            if (p.Guild == localPlayerHandler.localPlayer.Guild || configHandler.config.FriendlyGuildsList.Contains(p.Guild, StringComparer.OrdinalIgnoreCase))
                            {
                                PlayerDrow(configHandler.config.FriendlyGuild, pos, p);
                                continue;
                            }

                            if (p.Alliance == localPlayerHandler.localPlayer.Alliance || configHandler.config.FriendlyAlliancesList.Contains(p.Alliance, StringComparer.OrdinalIgnoreCase))
                            {
                                PlayerDrow(configHandler.config.FriendlyAlliance, pos, p);
                                continue;
                            }
                        }

                        if (configHandler.config.EnemyLists)
                        {
                            if (configHandler.config.EnemyPlayersList.Contains(p.Name, StringComparer.OrdinalIgnoreCase))
                            {
                                PlayerDrow(configHandler.config.EnemyPlayer, pos, p);
                                continue;
                            }

                            if (configHandler.config.EnemyGuildsList.Contains(p.Guild, StringComparer.OrdinalIgnoreCase))
                            {
                                PlayerDrow(configHandler.config.EnemyGuild, pos, p);
                                continue;
                            }

                            if (configHandler.config.EnemyAlliancesList.Contains(p.Alliance, StringComparer.OrdinalIgnoreCase))
                            {
                                PlayerDrow(configHandler.config.EnemyAlliance, pos, p);
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
                                PlayerDrow(configHandler.config.FriendlyFaction, pos, p);
                            }
                            else if (p.Faction != localPlayerHandler.localPlayer.Faction && p.Faction != Faction.PVP)
                            {
                                PlayerDrow(configHandler.config.EnemyFaction, pos, p);
                            }
                            else
                            {
                                PlayerDrow(configHandler.config.Pvp, pos, p);
                            }

                            continue;
                        }

                        #endregion

                        if (localPlayerHandler.localPlayer.CurrentCluster.ClusterColor == ClusterColor.City)
                        {
                            PlayerDrow(configHandler.config.NoPvp, pos, p);
                        }
                        else if (localPlayerHandler.localPlayer.CurrentCluster.ClusterColor == ClusterColor.Default)
                        {
                            switch (p.Faction)
                            {
                                case Faction.NoPVP:
                                    PlayerDrow(configHandler.config.NoPvp, pos, p);
                                    break;

                                case Faction.Martlock:
                                case Faction.Lymhurst:
                                case Faction.Bridjewatch:
                                case Faction.ForthSterling:
                                case Faction.Thetford:
                                case Faction.Caerleon:
                                    PlayerDrow(configHandler.config.Faction, pos, p, true);
                                    break;

                                case Faction.PVP:
                                    PlayerDrow(configHandler.config.Pvp, pos, p);
                                    break;
                            }
                        }
                        else
                        {
                            PlayerDrow(configHandler.config.Pvp, pos, p);
                        }
                    }
                }
            }
        }

        private void PlayerDrow(object[] settings, Vector2 pos, Player player, bool faction = false)
        {
            if (!Convert.ToBoolean(settings[0])) return;

            brushesDictionary._players["MainColor"].Color = ConvertColor(settings[2].ToString());
            brushesDictionary._players["AccentColor"].Color = ConvertColor(settings[3].ToString());
            brushesDictionary._players["NickColor"].Color = ConvertColor(settings[7].ToString());
            brushesDictionary._players["AllianceColor"].Color = ConvertColor(settings[9].ToString());
            brushesDictionary._players["DistanceColor"].Color = ConvertColor(settings[11].ToString());
            brushesDictionary._players["MountedColor"].Color = ConvertColor(settings[13].ToString());
            brushesDictionary._players["VisibleContactColor"].Color = ConvertColor(settings[15].ToString());
            brushesDictionary._players["HealthColor"].Color = ConvertColor(settings[17].ToString());
            brushesDictionary._players["FocusLineColor"].Color = ConvertColor(settings[19].ToString());
            brushesDictionary._players["VisibleContactColor2"].Color = ConvertColor(settings[20].ToString());

            #region ESP 1.0

            if (configHandler.config.ExtendedESP && Convert.ToBoolean(settings[18]))
            {
                gfx.DrawLine(brushesDictionary._players["FocusLineColor"], 0, 0, pos.X, pos.Y, 1);
            }

            SolidBrush mainBrush = null;

            if (faction)
            {
                mainBrush = brushesDictionary._factionColors[(int)player.Faction];
            }
            else
            {
                mainBrush = brushesDictionary._players["MainColor"];
            }

            string drawText = string.Empty;

            switch (Convert.ToInt32(settings[1]))
            {
                case 1://STAR
                    drawText = "N";
                    break;

                case 2://SHIELD
                    drawText = "S";
                    break;

                case 3://FLAG
                    drawText = "E";
                    break;

                case 4://SKULL
                    drawText = "D";
                    break;

                case 5://HEART
                    drawText = "C";
                    break;

                case 6://X-MARK
                    drawText = "O";
                    break;
            }

            gfx.PlayerDot(mainBrush, brushesDictionary._players["AccentColor"], pos, brushesDictionary._fonts["Icon"], drawText, Convert.ToSingle(settings[4]));

            #endregion

            #region ESP 2.0

            if (configHandler.config.ExtendedESP && Convert.ToBoolean(settings[0]))
            {
                int distance = (int)Vector2.Distance(player.Position, localPlayerHandler.localPlayer.Position);

                //Nick
                if (Convert.ToBoolean(settings[6]))
                {
                    Additions.DrawTextCentered(gfx, brushesDictionary._fonts["Main"], brushesDictionary._players["NickColor"], pos.X, pos.Y - Convert.ToSingle(settings[4]) / 2 - 5, player.Name);
                }

                //Alliance
                if (Convert.ToBoolean(settings[8]) && player.Alliance.Length > 1)
                {
                    //Если включён ник. Ставим альянс выше, если выключен заменяем
                    if (Convert.ToBoolean(settings[6]))
                    {
                        Additions.DrawTextCentered(gfx, brushesDictionary._fonts["Main"], brushesDictionary._players["AllianceColor"], pos.X, pos.Y - Convert.ToSingle(settings[4]) / 2 - 9, "[" + player.Alliance + "]");
                    }
                    else
                    {
                        Additions.DrawTextCentered(gfx, brushesDictionary._fonts["Main"], brushesDictionary._players["AllianceColor"], pos.X, pos.Y - Convert.ToSingle(settings[4]) / 2 - 5, "[" + player.Alliance + "]");
                    }
                }

                //Distance
                if (Convert.ToBoolean(settings[10]))
                {
                    Additions.DrawTextCentered(gfx, brushesDictionary._fonts["Main"], brushesDictionary._players["DistanceColor"], pos.X - Convert.ToSingle(settings[4]), pos.Y - Convert.ToSingle(settings[4]) / 2, Convert.ToString(distance));
                    
                }

                //Mounted
                if (player.IsMounted)
                {
                    if (Convert.ToBoolean(settings[12]))
                    {
                        Additions.DrawTextCentered(gfx, brushesDictionary._fonts["Main"], brushesDictionary._players["MountedColor"], pos.X + Convert.ToSingle(settings[4]) / 2 + 4, pos.Y - Convert.ToSingle(settings[4]) / 2 + 5, "M");
                    }
                }

                //VISIBLE CONTACT
                if (Convert.ToBoolean(settings[14]))
                {
                    if (distance < 42)
                    {
                        Additions.DrawTextCentered(gfx, brushesDictionary._fonts["Icon"], brushesDictionary._players["VisibleContactColor"], pos.X + Convert.ToSingle(settings[4]) / 2 + 4, pos.Y - Convert.ToSingle(settings[4]) / 2 - 0.5f, "B");
                    }
                    else
                    {
                        Additions.DrawTextCentered(gfx, brushesDictionary._fonts["Icon"], brushesDictionary._players["VisibleContactColor2"], pos.X + Convert.ToSingle(settings[4]) / 2 + 4, pos.Y - Convert.ToSingle(settings[4]) / 2 - 0.5f, "A");
                    }
                }

                //HEALTH INDICATOR
                switch (Convert.ToInt32(settings[16]))
                {
                    case 2:
                        Additions.DrawTextCentered(gfx, brushesDictionary._fonts["Main"], brushesDictionary._players["HealthColor"], pos.X, pos.Y + Convert.ToSingle(settings[4]) / 2 + 1f, player.Health.strPercent);
                        break;

                    case 3:
                        Additions.DrawTextCentered(gfx, brushesDictionary._fonts["Main"], brushesDictionary._players["HealthColor"], pos.X, pos.Y + Convert.ToSingle(settings[4]) / 2 + 1f, player.Health.Value.ToString());
                        break;
                }
            }

            #endregion
        }

        private Color ConvertColor(string color)
        {
            System.Drawing.Color temp = System.Drawing.ColorTranslator.FromHtml(color);
            return new Color(temp.R, temp.G, temp.B, temp.A);
        }
    }
}
