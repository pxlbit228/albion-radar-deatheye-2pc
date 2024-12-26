using Albion.Network;
using X975.Radar.GameObjects.Mobs;
using X975.Radar.GameObjects.Players;
using System.Threading.Tasks;

namespace X975.Radar.Packets.Handlers
{
    public class MoveEventHandler : EventPacketHandler<MoveEvent>
    {
        private readonly PlayersHandler playerHandler;
        private readonly MobsHandler mobHandler;

        public MoveEventHandler(PlayersHandler playerHandler, MobsHandler mobsHandler) : base(Init.PacketIndexes.Move)
        {
            this.playerHandler = playerHandler;
            this.mobHandler = mobsHandler;
        }

        protected override Task OnActionAsync(MoveEvent value)
        {
            playerHandler.UpdatePlayerPosition(value.Id, value.PositionBytes, value.NewPositionBytes, value.Speed, value.Time);
            mobHandler.UpdateMobPosition(value.Id, value.PositionBytes, value.NewPositionBytes, value.Speed, value.Time);

            return Task.CompletedTask;
        }
    }
}
