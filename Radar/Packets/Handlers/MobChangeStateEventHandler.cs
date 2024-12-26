using Albion.Network;
using X975.Radar.GameObjects.Mobs;
using System.Threading.Tasks;

namespace X975.Radar.Packets.Handlers
{
    public class MobChangeStateEventHandler : EventPacketHandler<MobChangeStateEvent>
    {
        private readonly MobsHandler mobHandler;
        public MobChangeStateEventHandler(MobsHandler mobHandler) : base(Init.PacketIndexes.MobChangeState)
        {
            this.mobHandler = mobHandler;
        }

        protected override Task OnActionAsync(MobChangeStateEvent value)
        {
            mobHandler.UpdateMobCharge(value.Id, value.Charge);

            return Task.CompletedTask;
        }
    }
}
