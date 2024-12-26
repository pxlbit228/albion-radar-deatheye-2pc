using Albion.Network;
using System.Threading.Tasks;
using X975.Radar.GameObjects.GatedWisps;

namespace X975.Radar.Packets.Handlers
{
    public class WispGateOpenedEventHandler : EventPacketHandler<WispGateOpenedEvent>
    {
        private readonly GatedWispsHandler wispInGateHandler;

        public WispGateOpenedEventHandler(GatedWispsHandler wispInGateHandler) : base(Init.PacketIndexes.WispGateOpened)
        {
            this.wispInGateHandler = wispInGateHandler;
        }

        protected override Task OnActionAsync(WispGateOpenedEvent value)
        {
            wispInGateHandler.Remove(value.Id);

            return Task.CompletedTask;
        }
    }
}
