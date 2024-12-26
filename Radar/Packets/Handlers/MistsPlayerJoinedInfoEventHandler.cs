using Albion.Network;
using System.Threading.Tasks;
using X975.Radar.GameObjects.LocalPlayer;

namespace X975.Radar.Packets.Handlers
{
    class MistsPlayerJoinedInfoEventHandler : EventPacketHandler<MistsPlayerJoinedInfoEvent>
    {
        private readonly LocalPlayerHandler localPlayerHandler;
        
        public MistsPlayerJoinedInfoEventHandler(LocalPlayerHandler localPlayerHandler) : base(Init.PacketIndexes.MistsPlayerJoinedInfo)
        {
            this.localPlayerHandler = localPlayerHandler;
        }

        protected override Task OnActionAsync(MistsPlayerJoinedInfoEvent value)
        {
            localPlayerHandler.UpdateClusterTimeCycle(value.TimeCycle);

            return Task.CompletedTask;
        }
    }
}
