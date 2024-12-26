using X975.Settings;
using X975.Tools;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;

namespace X975.Pages
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public partial class MobsPage : Page
    {
        ConfigHandler configHandler = ConfigHandler.Source;
        public MobsPage()
        {
            InitializeComponent();
            Title = Utility.RandomString();
        }

        public void UpdateSettings()
        {
            try
            {
                #region WORLD MOBS

                WorldMobsSwitch.IsChecked = Convert.ToBoolean(configHandler.config.WorldMobs[0]);
                OnlyProckedSwitch.IsChecked = Convert.ToBoolean(configHandler.config.WorldMobs[1]);
                WorldSizeSlider.Value = ValidateValue(Convert.ToInt32(configHandler.config.WorldMobs[2]), 4, 16);

                #endregion

                #region DRONES

                DronesSwitch.IsChecked = Convert.ToBoolean(configHandler.config.DroneMobs[0]);
                DronesSizeSlider.Value = ValidateValue(Convert.ToInt32(configHandler.config.DroneMobs[1]), 4, 16);

                #endregion

                #region MIST MOBS

                WispsSwitch.IsChecked = Convert.ToBoolean(configHandler.config.MistWisps[0]);
                MistsSizeSlider.Value = ValidateValue(Convert.ToInt32(configHandler.config.MistWisps[1]), 4, 16);
                WispsInGateSwitch.IsChecked = Convert.ToBoolean(configHandler.config.MistWisps[2]);

                MistBossSwitch.IsChecked = Convert.ToBoolean(configHandler.config.MistMobs[0]);
                MistsBossSizeSlider.Value = ValidateValue(Convert.ToInt32(configHandler.config.MistMobs[1]), 4, 16);

                #endregion

                #region FISH NODES

                FishSwitch.IsChecked = Convert.ToBoolean(configHandler.config.FishNodes[0]);
                FishSizeSlider.Value = ValidateValue(Convert.ToInt32(configHandler.config.FishNodes[1]), 4, 16);

                #endregion

                #region TREASURES

                TreasureSwitch.IsChecked = Convert.ToBoolean(configHandler.config.HiddenTreasures[0]);
                TreasureSizeSlider.Value = ValidateValue(Convert.ToInt32(configHandler.config.HiddenTreasures[1]), 4, 16);
                WorldChestSwitch.IsChecked = Convert.ToBoolean(configHandler.config.HiddenTreasures[2]);

                #endregion

                #region EVENT MOBS

                EventSwitch.IsChecked = Convert.ToBoolean(configHandler.config.EventMobs[0]);
                EventSizeSlider.Value = ValidateValue(Convert.ToInt32(configHandler.config.EventMobs[1]), 4, 16);

                #endregion
            }
            catch
            {
                ConfigHandler.Source.BadConfig();
            }
        }

        #region WORLD MOBS

        private void WorldMobsSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.WorldMobs[0] = WorldMobsSwitch.IsChecked;
        }

        private void OnlyProckedSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.WorldMobs[1] = OnlyProckedSwitch.IsChecked;
        }

        private void WorldSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.WorldMobs[2] = (int)WorldSizeSlider.Value;
        }

        #endregion

        #region DRONES

        private void DronesSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.DroneMobs[0] = DronesSwitch.IsChecked;
        }

        private void DronesSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.DroneMobs[1] = (int)DronesSizeSlider.Value;
        }

        #endregion

        #region MIST MOBS

        private void WispsSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.MistWisps[0] = WispsSwitch.IsChecked;
        }

        private void MistsSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.MistWisps[1] = (int)MistsSizeSlider.Value;
        }

        private void WispsInGateSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.MistWisps[2] = WispsInGateSwitch.IsChecked;
        }

        private void MistBossSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.MistMobs[0] = MistBossSwitch.IsChecked;
        }

        private void MistsBossSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.MistMobs[1] = (int)MistsBossSizeSlider.Value;
        }

        #endregion

        #region FISH NODES

        private void FishSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.FishNodes[0] = FishSwitch.IsChecked;
        }

        private void FishSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.FishNodes[1] = (int)FishSizeSlider.Value;
        }

        #endregion

        #region TREASURES

        private void TreasureSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.HiddenTreasures[0] = TreasureSwitch.IsChecked;
        }

        private void TreasureSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.HiddenTreasures[1] = (int)TreasureSizeSlider.Value;
        }

        private void WorldChestSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.HiddenTreasures[2] = WorldChestSwitch.IsChecked;
        }

        #endregion

        #region EVENT MOBS

        private void EventSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.EventMobs[0] = EventSwitch.IsChecked;
        }

        private void EventSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.EventMobs[1] = (int)EventSizeSlider.Value;
        }

        #endregion

        private int ValidateValue(int value, int minRange, int MaxRange)
        {
            if (value <= MaxRange && value >= minRange)
            {
                return value;
            }
            else
            {
                return minRange;
            }
        }
    }
}
