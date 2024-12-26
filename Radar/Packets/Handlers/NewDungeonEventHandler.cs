using Albion.Network;
using X975.Radar.GameObjects.Dungeons;
using System.Threading.Tasks;

namespace X975.Radar.Packets.Handlers
{
    public class NewDungeonEventHandler : EventPacketHandler<NewDungeonEvent>
    {
        private readonly DungeonsHandler dungeonsHandler;

        public NewDungeonEventHandler(DungeonsHandler dungeonsHandler) : base(Init.PacketIndexes.NewDungeonExit)
        {
            this.dungeonsHandler = dungeonsHandler;
        }

        protected override Task OnActionAsync(NewDungeonEvent value)
        {
            dungeonsHandler.AddDungeon(value.Id, value.Type, value.Position, value.Charges);

            return Task.CompletedTask;
        }
    }
}
