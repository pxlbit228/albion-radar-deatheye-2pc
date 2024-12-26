using Albion.Network;
using X975.Radar.GameObjects.Players;
using System.Threading.Tasks;

namespace X975.Radar.Packets.Handlers
{
    class MountedEventHandler : EventPacketHandler<MountedEvent>
    {
        private readonly PlayersHandler playerHandler;

        public MountedEventHandler(PlayersHandler playerHandler) : base(Init.PacketIndexes.Mounted)
        {
            this.playerHandler = playerHandler;
        }

        protected override Task OnActionAsync(MountedEvent value)
        {
            playerHandler.Mounted(value.Id, value.IsMounted);

            return Task.CompletedTask;
        }
    }
}
