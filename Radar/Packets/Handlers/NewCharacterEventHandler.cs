using Albion.Network;
using System.Threading.Tasks;
using X975.Settings;
using X975.Radar.GameObjects.Players;
using System.IO;
using System.Media;
using X975.Radar.GameObjects.LocalPlayer;
using System;
using System.Linq;
using System.Numerics;
using X975.Radar.Utility;

namespace X975.Radar.Packets.Handlers
{
    public class NewCharacterEventHandler : EventPacketHandler<NewCharacterEvent>
    {
        private readonly ConfigHandler configHandler = ConfigHandler.Source;
        private readonly LocalPlayerHandler localPlayerHandler;
        private readonly PlayersHandler playerHandler;

        Stream beep = Properties.Resources.beep;
        SoundPlayer player;

        public NewCharacterEventHandler(PlayersHandler playerHandler, LocalPlayerHandler localPlayerHandler) : base(Init.PacketIndexes.NewCharacter)
        {
            player = new SoundPlayer(beep);

            this.playerHandler = playerHandler;
            this.localPlayerHandler = localPlayerHandler;
        }

        protected override Task OnActionAsync(NewCharacterEvent value)
        {
            Vector2 pos = Vector2.Zero;
            
            if (playerHandler.XorCode != null && value.EncryptedPosition != null)
            {
                var coords = playerHandler.Decrypt(value.EncryptedPosition);
                pos = new Vector2(coords[1], coords[0]);
            }
            else if (value.Position != Vector2.Zero)
            {
                pos = value.Position;
            }
            
            playerHandler.AddPlayer(value.Id, value.Name, value.Guild, value.Alliance, pos, value.Health, value.Faction, value.Equipments, value.Spells);

            if (localPlayerHandler.localPlayer.CurrentCluster.ClusterColor != ClusterColor.City)
            {
                #region CUSTOM LISTS

                if (configHandler.config.FriendlyLists)
                {
                    if (configHandler.config.FriendlyPlayersList.Contains(value.Name, StringComparer.OrdinalIgnoreCase))
                    {
                        if (Convert.ToBoolean(configHandler.config.FriendlyPlayer[0]))
                            PlayBeep(Convert.ToInt32(configHandler.config.FriendlyPlayer[5]));
                        return Task.CompletedTask;
                    }

                    if (value.Guild == localPlayerHandler.localPlayer.Guild || configHandler.config.FriendlyGuildsList.Contains(value.Guild, StringComparer.OrdinalIgnoreCase))
                    {
                        if (Convert.ToBoolean(configHandler.config.FriendlyGuild[0]))
                            PlayBeep(Convert.ToInt32(configHandler.config.FriendlyGuild[5]));
                        return Task.CompletedTask;
                    }

                    if (value.Alliance == localPlayerHandler.localPlayer.Alliance || configHandler.config.FriendlyAlliancesList.Contains(value.Alliance, StringComparer.OrdinalIgnoreCase))
                    {
                        if (Convert.ToBoolean(configHandler.config.FriendlyAlliance[0]))
                            PlayBeep(Convert.ToInt32(configHandler.config.FriendlyAlliance[5]));
                        return Task.CompletedTask;
                    }
                }

                if (configHandler.config.EnemyLists)
                {
                    if (configHandler.config.EnemyPlayersList.Contains(value.Name, StringComparer.OrdinalIgnoreCase))
                    {
                        if (Convert.ToBoolean(configHandler.config.EnemyPlayer[0]))
                            PlayBeep(Convert.ToInt32(configHandler.config.EnemyPlayer[5]));
                        return Task.CompletedTask;
                    }

                    if (configHandler.config.EnemyGuildsList.Contains(value.Guild, StringComparer.OrdinalIgnoreCase))
                    {
                        if (Convert.ToBoolean(configHandler.config.EnemyGuildsList[0]))
                            PlayBeep(Convert.ToInt32(configHandler.config.EnemyGuildsList[5]));
                        return Task.CompletedTask;
                    }

                    if (configHandler.config.EnemyAlliancesList.Contains(value.Alliance, StringComparer.OrdinalIgnoreCase))
                    {
                        if (Convert.ToBoolean(configHandler.config.EnemyAlliancesList[0]))
                            PlayBeep(Convert.ToInt32(configHandler.config.EnemyAlliancesList[5]));
                        return Task.CompletedTask;
                    }
                }

                #endregion

                #region FACTION WAR

                if (configHandler.config.FactionWar && (localPlayerHandler.localPlayer.Faction != Faction.NoPVP && localPlayerHandler.localPlayer.Faction != Faction.PVP))
                {
                    if (value.Faction == Faction.NoPVP) return Task.CompletedTask;

                    if (value.Faction == localPlayerHandler.localPlayer.Faction)
                    {
                        if (Convert.ToBoolean(configHandler.config.FriendlyFaction[0]))
                            PlayBeep(Convert.ToInt32(configHandler.config.FriendlyFaction[5]));
                        return Task.CompletedTask;
                    }
                    else if (value.Faction != localPlayerHandler.localPlayer.Faction && value.Faction != Faction.PVP)
                    {
                        if (Convert.ToBoolean(configHandler.config.EnemyFaction[0]))
                            PlayBeep(Convert.ToInt32(configHandler.config.EnemyFaction[5]));
                        return Task.CompletedTask;
                    }
                    else
                    {
                        if (Convert.ToBoolean(configHandler.config.Pvp[0]))
                            PlayBeep(Convert.ToInt32(configHandler.config.Pvp[5]));
                        return Task.CompletedTask;
                    }
                }

                #endregion

                if (localPlayerHandler.localPlayer.CurrentCluster.ClusterColor == ClusterColor.Default)
                {
                    switch (value.Faction)
                    {
                        case Faction.NoPVP:
                            if (Convert.ToBoolean(configHandler.config.NoPvp[0]))
                                PlayBeep(Convert.ToInt32(configHandler.config.NoPvp[5]));
                            break;

                        case Faction.Martlock:
                        case Faction.Lymhurst:
                        case Faction.Bridjewatch:
                        case Faction.ForthSterling:
                        case Faction.Thetford:
                        case Faction.Caerleon:
                            if (Convert.ToBoolean(configHandler.config.Faction[0]))
                                PlayBeep(Convert.ToInt32(configHandler.config.Faction[5]));
                            break;

                        case Faction.PVP:
                            if (Convert.ToBoolean(configHandler.config.Pvp[0]))
                                PlayBeep(Convert.ToInt32(configHandler.config.Pvp[5]));
                            break;
                    }
                }
                else
                {
                    if (Convert.ToBoolean(configHandler.config.Pvp[0]))
                        PlayBeep(Convert.ToInt32(configHandler.config.Pvp[5]));
                }
            }

            return Task.CompletedTask;
        }

        private void PlayBeep(int play)
        {
            if (play == 0) return;

            player.Play();
        }
    }
}
