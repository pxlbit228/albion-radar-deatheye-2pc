using Albion.Network;
using X975.Radar.GameObjects.Harvestables;
using System.Threading.Tasks;

namespace X975.Radar.Packets.Handlers
{
    public class NewHarvestableEventHandler : EventPacketHandler<NewHarvestableEvent>
    {
        private readonly HarvestablesHandler harvestableHandler;

        public NewHarvestableEventHandler(HarvestablesHandler harvestableHandler) : base(Init.PacketIndexes.NewHarvestableObject)
        {
            this.harvestableHandler = harvestableHandler;
        }

        protected override Task OnActionAsync(NewHarvestableEvent value)
        {
            harvestableHandler.AddHarvestable(value.Id, value.Type, value.Tier, value.Position, value.Count, value.Charge);

            return Task.CompletedTask;
        }
    }
}
