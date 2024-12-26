using X975.Radar.GameObjects.Players;
using X975.Radar.GameObjects.Mobs;
using X975.Radar.GameObjects.Dungeons;
using X975.Radar.GameObjects.FishNodes;
using X975.Radar.GameObjects.GatedWisps;
using X975.Radar.GameObjects.LootChests;
using Albion.Network;
using System.Threading.Tasks;

namespace X975.Radar.Packets.Handlers
{
    public class LeaveEventHandler : EventPacketHandler<LeaveEvent>
    {
        private readonly PlayersHandler playersHandler;
        private readonly MobsHandler mobsHandler;
        private readonly DungeonsHandler dungeonsHandler;
        private readonly FishNodesHandler fishNodesHandler;
        private readonly GatedWispsHandler gatedWispsHandler;
        private readonly LootChestsHandler lootChestsHandler;

        public LeaveEventHandler(PlayersHandler playersHandler, MobsHandler mobsHandler, DungeonsHandler dungeonsHandler, FishNodesHandler fishNodesHandler, GatedWispsHandler gatedWispsHandler, LootChestsHandler lootChestsHandler) : base(Init.PacketIndexes.Leave)
        {
            this.playersHandler = playersHandler;
            this.mobsHandler = mobsHandler;
            this.dungeonsHandler = dungeonsHandler;
            this.fishNodesHandler = fishNodesHandler;
            this.gatedWispsHandler = gatedWispsHandler;
            this.lootChestsHandler = lootChestsHandler;
        }

        protected override Task OnActionAsync(LeaveEvent value)
        {
            playersHandler.Remove(value.Id);
            mobsHandler.Remove(value.Id);
            dungeonsHandler.Remove(value.Id);
            fishNodesHandler.Remove(value.Id);
            gatedWispsHandler.Remove(value.Id);
            lootChestsHandler.Remove(value.Id);

            return Task.CompletedTask;
        }
    }
}
