using X975.Tools;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace X975.Settings
{
    public class ConfigHandler
    {
        #region SINGLETON

        private static ConfigHandler source = null;
        private static readonly object threadlock = new object();

        public static ConfigHandler Source
        {
            get
            {
                lock (threadlock)
                {
                    if (source == null)
                        source = new ConfigHandler();

                    return source;
                }
            }
        }

        #endregion

        public string selectedConfig = string.Empty;
        public List<string> ConfigList = new List<string>();
        public Config config = new Config();

        private ConfigHandler()
        {
            if (new DirectoryInfo(Pathfinder.mainFolder).GetFiles().Where(x=> x.Extension == ".cfg").Count() == 0) CreateConfig();

            IEnumerable files = new DirectoryInfo(Pathfinder.mainFolder).GetFiles().Where(x => x.Extension == ".cfg");

            foreach (FileInfo file in files)
            {
                ConfigList.Add(file.Name.Replace(file.Extension, ""));
            }

            selectedConfig = ConfigList.First();

            LoadConfig();
        }

        public void LoadConfig()
        {
            try
            {
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Pathfinder.mainFolder + "\\" + selectedConfig + ".cfg"));
            }
            catch 
            {
                CreateConfig(selectedConfig);
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Pathfinder.mainFolder + "\\" + selectedConfig + ".cfg"));
            }
        }

        public void BadConfig()
        {
            CreateConfig(selectedConfig);
            config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Pathfinder.mainFolder + "\\" + selectedConfig + ".cfg"));
        }

        public async void SaveConfig()
        {
            await Task.Run(() => File.WriteAllText(Pathfinder.mainFolder + "\\" + selectedConfig + ".cfg", JsonConvert.SerializeObject(config, Formatting.Indented)));
        }

        public void CreateConfig(string name = null)
        {
            if (!Directory.Exists(Pathfinder.mainFolder))
            {
                Directory.CreateDirectory(Pathfinder.mainFolder);
            }

            Config settingsJson = new Config();

            #region MAIN

            settingsJson.Language = new CultureInfo("en-US");
            settingsJson.ToggleMenyKey = KeyboardHook.VKeys.INSERT;
            settingsJson.ToggleRadarKey = KeyboardHook.VKeys.END;
            settingsJson.CloseKey = KeyboardHook.VKeys.HOME;

            #endregion

            #region RADAR

            #region PLAYERS

            #region MAIN SETTINGS

            settingsJson.EspEnabled = true;
            settingsJson.ExtendedESP = true;
            settingsJson.FactionWar = true;
            settingsJson.FriendlyLists = true;
            settingsJson.EnemyLists = true;

            #endregion

            #region ESP

            object[] s = { true, 0, "#FFFF0100", "#FFFF0100", 4, 0, false, "#FFFF0100", false, "#FFFF0100", false, "#FFFF0100", false, "#FFFF0100", false, "#FFFF0100", 0, "#FFFF0100", false, "#FFFF0100", "#FFFF0100", false };
            settingsJson.NoPvp = s;
            settingsJson.Pvp = s;
            settingsJson.Faction = s;
            settingsJson.FriendlyFaction = s;
            settingsJson.EnemyFaction = s;
            settingsJson.Bounty = s;
            settingsJson.FriendlyPlayer = s;
            settingsJson.FriendlyGuild = s;
            settingsJson.FriendlyAlliance = s;
            settingsJson.EnemyPlayer = s;
            settingsJson.EnemyGuild = s;
            settingsJson.EnemyAlliance = s;

            #endregion

            #region MANUAL LISTS

            settingsJson.FriendlyPlayersList = new List<string>();
            settingsJson.FriendlyGuildsList = new List<string>();
            settingsJson.FriendlyAlliancesList = new List<string>();
            settingsJson.EnemyPlayersList = new List<string>();
            settingsJson.EnemyGuildsList = new List<string>();
            settingsJson.EnemyAlliancesList = new List<string>();

            #endregion

            #endregion

            #region RESOURCES

            settingsJson.ResourcesEnabled = true;
            settingsJson.StackFilter = 0;
            settingsJson.HarvestableDotSize = 4;
            settingsJson.ResourcesMobsEnabled = true;
            settingsJson.OnlyAspectedMode = false;
            settingsJson.HarvestableList = new List<string>();

            #endregion

            #region MOBS

            settingsJson.WorldMobs = new object[] { false, false, 4};
            settingsJson.DroneMobs = new object[] { false, 4};
            settingsJson.MistMobs = new object[] { false, 4};
            settingsJson.FishNodes = new object[] { false, 4 };
            settingsJson.MistWisps = new object[] { false, 4 , false };
            settingsJson.HiddenTreasures = new object[] { false, 4 , false};
            settingsJson.EventMobs = new object[] { false, 4 };

            settingsJson.CorruptedMobs = new object[] { false, false, false, false, false, 4};
            settingsJson.CorruptedTraps = new object[] { false, false, false, false, 4};

            #endregion

            #region DUNGEONS

            settingsJson.CorruptedDungeon = false;
            settingsJson.SoloDungeon = false;
            settingsJson.GroupDungeon = false;
            settingsJson.HellDungeon = false;

            #endregion

            #region STYLE
                                //BACK         //MESH           //OUTLINE       //FIRST LAYER         //SECOND LAYER         //THIRD LAYER        //CENTER
            object[] g = { "#FFFF0100", "#FFFF0100", 0, "#FFFF0100", 0, "#FFFF0100", 0, 0, 0, "#FFFF0100", 0, 0, 0, "#FFFF0100", 0, 0, 0, "#FFFF0100", 0, 0 };
            settingsJson.StyleSettings = g;

            settingsJson.Zoom = 2.2;
            settingsJson.Height = 400;
            settingsJson.Width = 400;
            settingsJson.SyncHaW = true;
            settingsJson.X = 0;
            settingsJson.Y = 0;

            #endregion

            #endregion

            #region ADDONS

            settingsJson.MapOpacity = 0;
            settingsJson.MapUnderRadar = false;

            settingsJson.ShowItems = false;
            settingsJson.ShowTrash = false;
            settingsJson.LinesCount = 0;

            settingsJson.EquipmentParts = new bool[] { false, false, false, false, false, false, false, false };

            settingsJson.ItemsXoffset = 0;
            settingsJson.ItemsYoffset = 0;
            settingsJson.ItemsScale = 1.0;

            #endregion

            string output = JsonConvert.SerializeObject(settingsJson, Formatting.Indented);

            if (name == null)
            {
                File.WriteAllText(Pathfinder.mainFolder + "\\Config 1.cfg", output);
                File.WriteAllText(Pathfinder.mainFolder + "\\Config 2.cfg", output);
                File.WriteAllText(Pathfinder.mainFolder + "\\Config 3.cfg", output);
            }
            else
            {
                File.WriteAllText(Pathfinder.mainFolder + "\\" + name + ".cfg", output);
            }
        }
    }
}
