using Albion.Network;
using X975.Radar.GameObjects.Players;
using System.Threading.Tasks;
using X975.Radar.GameObjects.Mobs;

namespace X975.Radar.Packets.Handlers
{
    class HealthUpdateEventHandler : EventPacketHandler<HealthUpdateEvent>
    {
        private readonly PlayersHandler playerHandler;
        private readonly MobsHandler mobHandler;

        public HealthUpdateEventHandler(PlayersHandler playerHandler, MobsHandler mobHandler) : base(Init.PacketIndexes.HealthUpdateEvent)
        {
            this.playerHandler = playerHandler;
            this.mobHandler = mobHandler;
        }

        protected override Task OnActionAsync(HealthUpdateEvent value)
        {
            playerHandler.UpdateHealth(value.Id, value.Health);
            mobHandler.UpdateHealth(value.Id, value.Health);

            return Task.CompletedTask;
        }
    }
}
