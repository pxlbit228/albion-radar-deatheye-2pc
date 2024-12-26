using Albion.Network;
using System.Threading.Tasks;
using X975.Radar.GameObjects.LootChests;

namespace X975.Radar.Packets.Handlers
{
    public class NewLootChestEventHandler : EventPacketHandler<NewLootChestEvent>
    {
        private readonly LootChestsHandler worldChestHandler;

        public NewLootChestEventHandler(LootChestsHandler worldChestHandler) : base(Init.PacketIndexes.NewLootChest)
        {
            this.worldChestHandler = worldChestHandler;
        }

        protected override Task OnActionAsync(NewLootChestEvent value)
        {
            worldChestHandler.AddWorldChest(value.Id, value.Position, value.Name, value.EnchLvl);

            return Task.CompletedTask;
        }
    }
}
