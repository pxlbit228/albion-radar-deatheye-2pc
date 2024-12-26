using X975.Radar.GameObjects.LocalPlayer;
using Albion.Network;
using System.Threading.Tasks;
using System.IO;
using System.Media;
using X975.Settings;

namespace X975.Radar.Packets.Handlers
{
    class LoadClusterObjectsEventHandler : EventPacketHandler<LoadClusterObjectsEvent>
    {
        private readonly LocalPlayerHandler localPlayerHandler;

        Stream announce = Properties.Resources.announce;
        SoundPlayer player;

        public LoadClusterObjectsEventHandler(LocalPlayerHandler localPlayerHandler) : base(Init.PacketIndexes.LoadClusterObjects)
        {
            player = new SoundPlayer(announce);
            this.localPlayerHandler = localPlayerHandler;
        }

        protected override Task OnActionAsync(LoadClusterObjectsEvent value)
        {
            if (localPlayerHandler.localPlayer.CurrentCluster.Subtype != ClusterSubtype.Unknown)
            {
                if (value.ClusterObjectives != null && ConfigHandler.Source.config.MistOverlayEnabled)
                    player.Play();

                localPlayerHandler.UpdateClusterObjectives(value.ClusterObjectives);
            }

            return Task.CompletedTask;
        }
    }
}
