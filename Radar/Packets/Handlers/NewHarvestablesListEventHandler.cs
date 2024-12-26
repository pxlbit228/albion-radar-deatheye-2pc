using Albion.Network;
using X975.Radar.GameObjects.Harvestables;
using System.Threading.Tasks;

namespace X975.Radar.Packets.Handlers
{
    public class NewHarvestablesListEventHandler : EventPacketHandler<NewHarvestablesListEvent>
    {
        private readonly HarvestablesHandler harvestableHandler;
        public NewHarvestablesListEventHandler(HarvestablesHandler harvestableHandler) : base(Init.PacketIndexes.NewHarvestableList)
        {
            this.harvestableHandler = harvestableHandler;
        }

        protected override Task OnActionAsync(NewHarvestablesListEvent value)
        {
            foreach (var harvestableObject in value.HarvestableObjects)
            {
                harvestableHandler.AddHarvestable(harvestableObject.Id, harvestableObject.Type, harvestableObject.Tier, harvestableObject.Position, harvestableObject.Count, harvestableObject.Charge);
            }

            return Task.CompletedTask;
        }
    }
}
