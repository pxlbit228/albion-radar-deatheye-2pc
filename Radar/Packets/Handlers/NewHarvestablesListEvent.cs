using Albion.Network;
using System.Collections.Generic;

namespace X975.Radar.Packets.Handlers
{
    public class NewHarvestablesListEvent : BaseEvent
    {
        private readonly List<NewHarvestableEvent> harvestableObjects;

        public NewHarvestablesListEvent(Dictionary<byte, object> parameters) : base(parameters)
        {
            harvestableObjects = new List<NewHarvestableEvent>();

            if (parameters[0] is byte[])
            {
                var ids = (byte[])parameters[0];
                var types = (byte[])parameters[1];
                var tiers = (byte[])parameters[2];
                var positions = (float[])parameters[3];
                var sizes = (byte[])parameters[4];

                for (int i = 0; i < ids.Length; i++)
                {
                    var harvestParameters = new Dictionary<byte, object>
                    {
                        { 0, ids[i] },
                        { 5, types[i] },
                        { 7, tiers[i] },
                        { 8, new float[] { positions[i * 2], positions[i * 2 + 1] } },
                        { 10, sizes[i] }
                    };

                    harvestableObjects.Add(new NewHarvestableEvent(harvestParameters));
                }
            }
            else if (parameters[0] is short[])
            {
                var ids = (short[])parameters[0];
                var types = (byte[])parameters[1];
                var tiers = (byte[])parameters[2];
                var positions = (float[])parameters[3];
                var sizes = (byte[])parameters[4];

                for (int i = 0; i < ids.Length; i++)
                {
                    var harvestParameters = new Dictionary<byte, object>
                    {
                        { 0, ids[i] },
                        { 5, types[i] },
                        { 7, tiers[i] },
                        { 8, new float[] { positions[i * 2], positions[i * 2 + 1] } },
                        { 10, sizes[i] }
                    };

                    harvestableObjects.Add(new NewHarvestableEvent(harvestParameters));
                }
            }
        }

        public IReadOnlyCollection<NewHarvestableEvent> HarvestableObjects
        {
            get
            {
                return harvestableObjects;
            }
        }
    }
}
