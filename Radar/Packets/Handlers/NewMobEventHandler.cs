using Albion.Network;
using X975.Radar.GameObjects.Mobs;
using System.Threading.Tasks;

namespace X975.Radar.Packets.Handlers
{
    class NewMobEventHandler : EventPacketHandler<NewMobEvent>
    {
        private readonly MobsHandler mobHandler;

        public NewMobEventHandler(MobsHandler mobHandler) : base(Init.PacketIndexes.NewMobEvent)
        {
            this.mobHandler = mobHandler;
        }

        protected override Task OnActionAsync(NewMobEvent value)
        {
            mobHandler.AddMob(value.Id, value.TypeId, value.Position, value.Health, value.Charge);

            return Task.CompletedTask;
        }
    }
}
