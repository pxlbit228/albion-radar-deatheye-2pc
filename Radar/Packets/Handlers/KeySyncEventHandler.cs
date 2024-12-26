using X975.Radar.GameObjects.Players;
using Albion.Network;
using System.Threading.Tasks;

namespace X975.Radar.Packets.Handlers
{
    public class KeySyncEventHandler : EventPacketHandler<KeySyncEvent>
    {
        private readonly PlayersHandler playersHandler;

        public KeySyncEventHandler(PlayersHandler playersHandler) : base(Init.PacketIndexes.KeySync)
        {
            this.playersHandler = playersHandler;
        }

        protected override Task OnActionAsync(KeySyncEvent value)
        {
            playersHandler.XorCode = value.Code;
            return Task.CompletedTask;
        }
    }
}
