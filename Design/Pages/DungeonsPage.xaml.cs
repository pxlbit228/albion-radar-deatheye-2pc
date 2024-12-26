using X975.Settings;
using X975.Tools;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;

namespace X975.Pages
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public partial class DungeonsPage : Page
    {
        ConfigHandler configHandler = ConfigHandler.Source;
        public DungeonsPage()
        {
            InitializeComponent();
            Title = Utility.RandomString();
        }

        public void UpdateSettings()
        {
            try
            {
                SoloSwitch.IsChecked = configHandler.config.SoloDungeon;
                CorruptSwitch.IsChecked = configHandler.config.CorruptedDungeon;
                GroupSwitch.IsChecked = configHandler.config.GroupDungeon;
                HellSwitch.IsChecked = configHandler.config.HellDungeon;
                DungeonsSizeSlider.Value = ValidateValue(Convert.ToInt32(configHandler.config.DungeonsDotSize), 4, 16);

                FlameSpinnerSwitch.IsChecked = Convert.ToBoolean(configHandler.config.CorruptedTraps[0]);
                LavaThrowerSwitch.IsChecked = Convert.ToBoolean(configHandler.config.CorruptedTraps[1]);
                BombThrowerSwitch.IsChecked = Convert.ToBoolean(configHandler.config.CorruptedTraps[2]);
                SpikesSwitch.IsChecked = Convert.ToBoolean(configHandler.config.CorruptedTraps[3]);

                CorruptedTrapsSizeSlider.Value = ValidateValue(Convert.ToInt32(configHandler.config.CorruptedTraps[4]), 4, 16);


                HookerSwitch.IsChecked = Convert.ToBoolean(configHandler.config.CorruptedMobs[0]);
                LavaBatSwitch.IsChecked = Convert.ToBoolean(configHandler.config.CorruptedMobs[1]);
                SilenceBatSwitch.IsChecked = Convert.ToBoolean(configHandler.config.CorruptedMobs[2]);
                GlueBatSwitch.IsChecked = Convert.ToBoolean(configHandler.config.CorruptedMobs[3]);
                KnockBackBatSwitch.IsChecked = Convert.ToBoolean(configHandler.config.CorruptedMobs[4]);

                CorruptedSizeSlider.Value = ValidateValue(Convert.ToInt32(configHandler.config.CorruptedMobs[5]), 4, 16);

            }
            catch
            {
                ConfigHandler.Source.BadConfig();
            }
        }

        #region LEFT

        private void SoloSwitchVoid(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.SoloDungeon = (bool)SoloSwitch.IsChecked;
        }

        private void CorruptSwitchVoid(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.CorruptedDungeon = (bool)CorruptSwitch.IsChecked;
        }

        private void GroupSwitchVoid(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.GroupDungeon = (bool)GroupSwitch.IsChecked;
        }

        private void HellSwitchVoid(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.HellDungeon = (bool)HellSwitch.IsChecked;
        }

        private void DungeonsSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.DungeonsDotSize = (int)DungeonsSizeSlider.Value;
        }

        #endregion

        #region TRAPS

        private void FlameSpinnerSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.CorruptedTraps[0] = (bool)FlameSpinnerSwitch.IsChecked;
        }

        private void LavaThrowerSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.CorruptedTraps[1] = (bool)LavaThrowerSwitch.IsChecked;
        }

        private void BombThrowerSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.CorruptedTraps[2] = (bool)BombThrowerSwitch.IsChecked;
        }

        private void SpikesSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.CorruptedTraps[3] = (bool)SpikesSwitch.IsChecked;
        }

        private void CorruptedTrapsSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.CorruptedTraps[4] = (int)CorruptedTrapsSizeSlider.Value;
        }

        #endregion

        #region MOBS

        private void HookerSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.CorruptedMobs[0] = (bool)HookerSwitch.IsChecked;
        }

        private void LavaBatSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.CorruptedMobs[1] = (bool)LavaBatSwitch.IsChecked;
        }

        private void SilenceBatSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.CorruptedMobs[2] = (bool)SilenceBatSwitch.IsChecked;
        }

        private void GlueBatSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.CorruptedMobs[3] = (bool)GlueBatSwitch.IsChecked;
        }

        private void KnockBackBatSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.CorruptedMobs[4] = (bool)KnockBackBatSwitch.IsChecked;
        }

        private void CorruptedSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.CorruptedMobs[5] = (int)CorruptedSizeSlider.Value;
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
