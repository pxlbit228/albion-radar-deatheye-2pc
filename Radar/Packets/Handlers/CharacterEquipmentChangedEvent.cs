using Albion.Network;
using System;
using System.Collections.Generic;

namespace X975.Radar.Packets.Handlers
{
    public class CharacterEquipmentChanged : BaseEvent
    {
        byte[] offsets = Init.PacketOffsets.CharacterEquipmentChanged;
        
        public CharacterEquipmentChanged(Dictionary<byte, object> parameters): base(parameters)
        {
            Id = Convert.ToInt32(parameters[offsets[0]]);
            Equipments = ConvertArray(parameters[offsets[1]]);
            Spells = ConvertArray(parameters[offsets[2]]);
        }

        public int Id { get; }

        public int[] Equipments { get; }

        public int[] Spells { get; }

        private int[] ConvertArray(object value)
        {
            int[] numArray1;
            switch (value)
            {
                case byte[] numArray2:
                    numArray1 = new int[numArray2.Length];
                    for (int index = 0; index < numArray2.Length; ++index)
                        numArray1[index] = (int)numArray2[index];
                    break;
                case short[] numArray3:
                    numArray1 = new int[numArray3.Length];
                    for (int index = 0; index < numArray3.Length; ++index)
                        numArray1[index] = (int)numArray3[index];
                    break;
                default:
                    numArray1 = (int[])value;
                    break;
            }
            return numArray1;
        }
    }
}
