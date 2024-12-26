using X975.Tools;
using System.Collections.Generic;
using System.Globalization;

namespace X975.Settings
{
    public class Config
    {
        #region MAIN SETTINGS

        public CultureInfo Language { get; set; }

        public KeyboardHook.VKeys ToggleRadarKey { get; set; }

        public KeyboardHook.VKeys ToggleMenyKey { get; set; }

        public KeyboardHook.VKeys CloseKey { get; set; }

        #endregion

        #region RADAR 

        #region PLAYERS

        #region MAIN SETTINGS

        public bool EspEnabled { get; set; }
        public bool ExtendedESP { get; set; }
        public bool FactionWar { get; set; }
        public bool FriendlyLists { get; set; }
        public bool EnemyLists { get; set; }

        #endregion

        #region ESP

        public object[] NoPvp { get; set; }
        public object[] Pvp { get; set; }
        public object[] Faction { get; set; }
        public object[] FriendlyFaction { get; set; }
        public object[] EnemyFaction { get; set; }
        public object[] Bounty { get; set; }
        public object[] FriendlyPlayer { get; set; }
        public object[] FriendlyGuild { get; set; }
        public object[] FriendlyAlliance { get; set; }
        public object[] EnemyPlayer { get; set; }
        public object[] EnemyGuild { get; set; }
        public object[] EnemyAlliance { get; set; }

        #endregion

        #region MANUAL LISTS

        public List<string> FriendlyPlayersList { get; set; }
        public List<string> FriendlyGuildsList { get; set; }
        public List<string> FriendlyAlliancesList { get; set; }
        public List<string> EnemyPlayersList { get; set; }
        public List<string> EnemyGuildsList { get; set; }
        public List<string> EnemyAlliancesList { get; set; }

        #endregion

        #endregion

        #region RESOURCES

        #region MAIN SETTINGS

        public bool ResourcesEnabled { get; set; }
        public bool StackSize { get; set; }
        public int StackFilter { get; set; }
        public bool ResourcesMobsEnabled { get; set; }
        public bool OnlyAspectedMode { get; set; }
        public List<string> HarvestableList { get; set; }

        public int HarvestableDotSize { get; set; }

        #endregion

        #endregion

        #region MOBS

        public object[] WorldMobs { get; set; }
        public object[] DroneMobs { get; set; }
        public object[] MistMobs { get; set; }
        public object[] MistWisps { get; set; }
        public object[] FishNodes { get; set; }
        public object[] HiddenTreasures { get; set; }
        public object[] EventMobs { get; set; }
        public object[] CorruptedMobs { get; set; }
        public object[] CorruptedTraps { get; set; }

        #endregion

        #region DUNGEONS

        public bool SoloDungeon { get; set; }
        public bool CorruptedDungeon { get; set; }
        public bool GroupDungeon { get; set; }
        public bool HellDungeon { get; set; }

        public int DungeonsDotSize { get; set; }

        #endregion

        #region STYLE

        public object[] StyleSettings { get; set; }
        public double Zoom { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public bool SyncHaW { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        #endregion

        #endregion

        #region ADDONS

        public bool MapUnderRadar { get; set; }
        public int MapOpacity { get; set; }


        public bool ShowItems { get; set; }
        public int ItemsStyle { get; set; }
        public bool ShowHealth { get; set; }
        public bool ShowMinIP { get; set; }
        public bool ShowTrash { get; set; }
        public int LinesCount { get; set; }

        public int ItemsXoffset { get; set; }
        public int ItemsYoffset { get; set; }
        public double ItemsScale { get; set; }
        public bool[] EquipmentParts { get; set; }

        public bool MistOverlayEnabled { get; set; }
        public int MistOverlayX {  get; set; }
        public int MistOverlayY { get; set; }

        #endregion
    }
}
