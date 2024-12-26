using Albion.Network;
using X975.Radar.GameObjects.Players;
using System.Threading.Tasks;
using X975.Radar.GameObjects.LocalPlayer;

namespace X975.Radar.Packets.Handlers
{
    class ChangeFlaggingFinishedEventHandler : EventPacketHandler<ChangeFlaggingFinishedEvent>
    {
        private readonly LocalPlayerHandler localPlayerHandler;
        private readonly PlayersHandler playerHandler;

        public ChangeFlaggingFinishedEventHandler(LocalPlayerHandler localPlayerHandler, PlayersHandler playerHandler) : base(Init.PacketIndexes.ChangeFlaggingFinished)
        {
            this.localPlayerHandler = localPlayerHandler;
            this.playerHandler = playerHandler;
        }

        protected override Task OnActionAsync(ChangeFlaggingFinishedEvent value)
        {
            localPlayerHandler.SetFaction(value.Id, value.Faction);
            playerHandler.SetFaction(value.Id, value.Faction);

            return Task.CompletedTask;
        }
    }
}
