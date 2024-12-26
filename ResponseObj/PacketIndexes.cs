namespace X975.Protocol.Connect.Messages.ResponseObj
{
    public class PacketIndexes
    {
        public int Leave { get; set; }
        public int JoinResponse { get; set; }
        public int Move { get; set; }
        public int HealthUpdateEvent { get; set; }
        public int MoveRequest { get; set; }
        public int NewCharacter { get; set; }
        public int ChangeCluster { get; set; }
        public int NewHarvestableList { get; set; }
        public int NewHarvestableObject { get; set; }
        public int HarvestableChangeState { get; set; }
        public int MobChangeState { get; set; }
        public int CharacterEquipmentChanged { get; set; }
        public int RegenerationHealthChangedEvent { get; set; }
        public int NewMobEvent { get; set; }
        public int Mounted { get; set; }
        public int LoadClusterObjects { get; set; }
        public int NewDungeonExit { get; set; }
        public int NewFishingZoneObject { get; set; }
        public int ChangeFlaggingFinished { get; set; }
        public int NewLootChest { get; set; }
        public int MistsPlayerJoinedInfo { get; set; }
        public int NewWispGate { get; set; }
        public int WispGateOpened { get; set; }
        public int KeySync { get; set; }
    }
}
