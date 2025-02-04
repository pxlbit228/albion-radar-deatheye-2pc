namespace X975.Protocol.Connect.Messages.ResponseObj
{
    public class PacketOffsets
    {
        public byte[] ChangeCluster { get; set; }
        public byte[] ChangeFlaggingFinished { get; set; }
        public byte[] CharacterEquipmentChanged { get; set; }
        public byte[] HarvestableChangeState { get; set; }
        public byte[] HealthUpdateEvent { get; set; }
        public byte[] JoinResponse { get; set; }
        public byte[] Leave { get; set; }
        public byte[] MobChangeState { get; set; }
        public byte[] Mounted { get; set; }
        public byte[] Move { get; set; }
        public byte[] MoveRequest { get; set; }
        public byte[] NewCharacter { get; set; }
        public byte[] NewDungeonExit { get; set; }
        public byte[] NewFishingZoneObject { get; set; }
        public byte[] NewHarvestableObject { get; set; }
        public byte[] NewLootChest { get; set; }
        public byte[] NewMobEvent { get; set; }
        public byte[] NewWispGate { get; set; }
        public byte[] WispGateOpened { get; set; }
        public byte[] RegenerationHealthChangedEvent { get; set; }
        public byte[] KeySync { get; set; }
    }
}
