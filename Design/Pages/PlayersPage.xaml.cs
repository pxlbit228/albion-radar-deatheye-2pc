using X975.Settings;
using X975.Tools;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Reflection;

namespace X975.Pages
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public partial class PlayersPage : Page
    {
        ConfigHandler configHandler = ConfigHandler.Source;

        //PLAYERS
        string activePlayerTag = "NoPVP";
        object[] activePlayerSettings;

        public PlayersPage()
        {
            InitializeComponent();
            Title = Utility.RandomString();
            activePlayerSettings = configHandler.config.NoPvp;
        }

        public void UpdateSettings()
        {
            try
            {
                #region PLAYERS

                EspSwitch.IsChecked = configHandler.config.EspEnabled;

                EspExtndSwitch.IsChecked = configHandler.config.ExtendedESP;
                ExESP.IsEnabled = (bool)EspExtndSwitch.IsChecked ? true : false;

                FactionWarSwitch.IsChecked = configHandler.config.FactionWar;

                switchToNoPVP();

                if (!(bool)FactionWarSwitch.IsChecked)
                {
                    FactionFBorder.IsEnabled = false;
                    FactionEBorder.IsEnabled = false;

                    FactionFText.Foreground = new SolidColorBrush(Color.FromRgb(64, 64, 64));
                    FactionEText.Foreground = new SolidColorBrush(Color.FromRgb(64, 64, 64));
                }
                else
                {
                    FactionFBorder.IsEnabled = true;
                    FactionEBorder.IsEnabled = true;

                    FactionFText.Foreground = new SolidColorBrush(Color.FromRgb(175, 199, 31));
                    FactionEText.Foreground = new SolidColorBrush(Color.FromRgb(199, 100, 31));
                }

                FriendlyPGASwitch.IsChecked = configHandler.config.FriendlyLists;

                if (!(bool)FriendlyPGASwitch.IsChecked)
                {
                    MPlayerFBorder.IsEnabled = false;
                    MGuildFBorder.IsEnabled = false;
                    MAllianceFBorder.IsEnabled = false;

                    MPlayerFText.Foreground = new SolidColorBrush(Color.FromRgb(64, 64, 64));
                    MGuildFText.Foreground = new SolidColorBrush(Color.FromRgb(64, 64, 64));
                    MAllianceFText.Foreground = new SolidColorBrush(Color.FromRgb(64, 64, 64));
                }
                else
                {
                    MPlayerFBorder.IsEnabled = true;
                    MGuildFBorder.IsEnabled = true;
                    MAllianceFBorder.IsEnabled = true;

                    MPlayerFText.Foreground = new SolidColorBrush(Color.FromRgb(116, 199, 31));
                    MGuildFText.Foreground = new SolidColorBrush(Color.FromRgb(13, 175, 134));
                    MAllianceFText.Foreground = new SolidColorBrush(Color.FromRgb(161, 77, 171));
                }

                EnemyPGASwitch.IsChecked = configHandler.config.EnemyLists;

                if (!(bool)EnemyPGASwitch.IsChecked)
                {
                    MPlayerEBorder.IsEnabled = false;
                    MGuildEBorder.IsEnabled = false;
                    MAllianceEBorder.IsEnabled = false;

                    MPlayerEText.Foreground = new SolidColorBrush(Color.FromRgb(64, 64, 64));
                    MGuildEText.Foreground = new SolidColorBrush(Color.FromRgb(64, 64, 64));
                    MAllianceEText.Foreground = new SolidColorBrush(Color.FromRgb(64, 64, 64));
                }
                else
                {
                    MPlayerEBorder.IsEnabled = true;
                    MGuildEBorder.IsEnabled = true;
                    MAllianceEBorder.IsEnabled = true;

                    MPlayerEText.Foreground = new SolidColorBrush(Color.FromRgb(199, 31, 31));
                    MGuildEText.Foreground = new SolidColorBrush(Color.FromRgb(199, 31, 31));
                    MAllianceEText.Foreground = new SolidColorBrush(Color.FromRgb(199, 31, 31));
                }



                CustomList.ItemsSource = configHandler.config.FriendlyPlayersList;
                ListCounter.Visibility = CustomList.Items.Count > 0 ? Visibility.Collapsed : Visibility.Visible;

                #endregion
            }
            catch
            {
                ConfigHandler.Source.BadConfig();
            }
        }

        #region RIGHT

        #region PLAYER TYPE

        private void SwitchPlyerType(object sender, MouseButtonEventArgs e)
        {
            string tag = (string)((Border)sender).Tag;

            if (tag == null || tag == string.Empty || tag == activePlayerTag) return;

            switch (activePlayerTag)
            {
                case "NoPVP":
                    NoPVPBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    NoPVPText.Foreground = new SolidColorBrush(Color.FromRgb(4, 180, 255));
                    break;

                case "PVP":
                    PVPBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    PVPText.Foreground = new SolidColorBrush(Color.FromRgb(199, 31, 31));
                    PVPText.Filled = false;
                    break;

                case "Faction":
                    FactionBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    FactionText.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    break;

                case "FactionF":
                    FactionFBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    FactionFText.Foreground = new SolidColorBrush(Color.FromRgb(175, 199, 31));
                    break;

                case "FactionE":
                    FactionEBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    FactionEText.Foreground = new SolidColorBrush(Color.FromRgb(199, 100, 31));
                    break;

                case "Bounty":
                    BountyBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    BountyText.Foreground = new SolidColorBrush(Color.FromRgb(241, 255, 0));
                    break;

                case "MPlayerF":
                    MPlayerFBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    MPlayerFText.Foreground = new SolidColorBrush(Color.FromRgb(116, 199, 31));
                    break;

                case "MGuildF":
                    MGuildFBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    MGuildFText.Foreground = new SolidColorBrush(Color.FromRgb(13, 175, 134));
                    break;

                case "MAllianceF":
                    MAllianceFBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    MAllianceFText.Foreground = new SolidColorBrush(Color.FromRgb(161, 77, 171));
                    break;

                case "MPlayerE":
                    MPlayerEBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    MPlayerEText.Foreground = new SolidColorBrush(Color.FromRgb(199, 31, 31));
                    break;

                case "MGuildE":
                    MGuildEBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    MGuildEText.Foreground = new SolidColorBrush(Color.FromRgb(199, 31, 31));
                    break;

                case "MAllianceE":
                    MAllianceEBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    MAllianceEText.Foreground = new SolidColorBrush(Color.FromRgb(199, 31, 31));
                    break;
            }

            switch (tag)
            {
                case "NoPVP":
                    activePlayerSettings = configHandler.config.NoPvp;
                    NoPVPBorder.Background = new SolidColorBrush(Color.FromRgb(30, 134, 216));
                    NoPVPText.Foreground = new SolidColorBrush(Color.FromRgb(15, 15, 15));

                    CustomListBorder.IsEnabled = false;
                    break;

                case "PVP":
                    activePlayerSettings = configHandler.config.Pvp;
                    PVPBorder.Background = new SolidColorBrush(Color.FromRgb(199, 31, 31));
                    PVPText.Foreground = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    PVPText.Filled = true;

                    CustomListBorder.IsEnabled = false;
                    break;

                case "Faction":
                    activePlayerSettings = configHandler.config.Faction;
                    FactionBorder.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    FactionText.Foreground = new SolidColorBrush(Color.FromRgb(15, 15, 15));

                    CustomListBorder.IsEnabled = false;
                    break;

                case "FactionF":
                    activePlayerSettings = configHandler.config.FriendlyFaction;
                    FactionFBorder.Background = new SolidColorBrush(Color.FromRgb(175, 199, 31));
                    FactionFText.Foreground = new SolidColorBrush(Color.FromRgb(15, 15, 15));

                    CustomListBorder.IsEnabled = false;
                    break;

                case "FactionE":
                    activePlayerSettings = configHandler.config.EnemyFaction;
                    FactionEBorder.Background = new SolidColorBrush(Color.FromRgb(199, 100, 31));
                    FactionEText.Foreground = new SolidColorBrush(Color.FromRgb(15, 15, 15));

                    CustomListBorder.IsEnabled = false;
                    break;

                case "Bounty":
                    activePlayerSettings = configHandler.config.Bounty;
                    BountyBorder.Background = new SolidColorBrush(Color.FromRgb(241, 255, 0));
                    BountyText.Foreground = new SolidColorBrush(Color.FromRgb(15, 15, 15));

                    CustomListBorder.IsEnabled = false;
                    break;

                case "MPlayerF":
                    activePlayerSettings = configHandler.config.FriendlyPlayer;
                    MPlayerFBorder.Background = new SolidColorBrush(Color.FromRgb(116, 199, 31));
                    MPlayerFText.Foreground = new SolidColorBrush(Color.FromRgb(15, 15, 15));

                    CustomList.ItemsSource = configHandler.config.FriendlyPlayersList;
                    CustomListBorder.IsEnabled = true;
                    break;

                case "MGuildF":
                    activePlayerSettings = configHandler.config.FriendlyGuild;
                    MGuildFBorder.Background = new SolidColorBrush(Color.FromRgb(13, 175, 134));
                    MGuildFText.Foreground = new SolidColorBrush(Color.FromRgb(15, 15, 15));

                    CustomList.ItemsSource = configHandler.config.FriendlyGuildsList;
                    CustomListBorder.IsEnabled = true;
                    break;

                case "MAllianceF":
                    activePlayerSettings = configHandler.config.FriendlyAlliance;
                    MAllianceFBorder.Background = new SolidColorBrush(Color.FromRgb(161, 77, 171));
                    MAllianceFText.Foreground = new SolidColorBrush(Color.FromRgb(15, 15, 15));

                    CustomList.ItemsSource = configHandler.config.FriendlyAlliancesList;
                    CustomListBorder.IsEnabled = true;
                    break;

                case "MPlayerE":
                    activePlayerSettings = configHandler.config.EnemyPlayer;
                    MPlayerEBorder.Background = new SolidColorBrush(Color.FromRgb(199, 31, 31));
                    MPlayerEText.Foreground = new SolidColorBrush(Color.FromRgb(15, 15, 15));

                    CustomList.ItemsSource = configHandler.config.EnemyPlayersList;
                    CustomListBorder.IsEnabled = true;
                    break;

                case "MGuildE":
                    activePlayerSettings = configHandler.config.EnemyGuild;
                    MGuildEBorder.Background = new SolidColorBrush(Color.FromRgb(199, 31, 31));
                    MGuildEText.Foreground = new SolidColorBrush(Color.FromRgb(15, 15, 15));

                    CustomList.ItemsSource = configHandler.config.EnemyGuildsList;
                    CustomListBorder.IsEnabled = true;
                    break;

                case "MAllianceE":
                    activePlayerSettings = configHandler.config.EnemyAlliance;
                    MAllianceEBorder.Background = new SolidColorBrush(Color.FromRgb(199, 31, 31));
                    MAllianceEText.Foreground = new SolidColorBrush(Color.FromRgb(15, 15, 15));

                    CustomList.ItemsSource = configHandler.config.EnemyAlliancesList;
                    CustomListBorder.IsEnabled = true;
                    break;
            }

            ListCounter.Visibility = CustomList.Items.Count > 0 ? Visibility.Collapsed : Visibility.Visible;

            activePlayerTag = tag;
            LoadPlayerSettings();
        }

        void LoadPlayerSettings()
        {
            DotEnabled.IsChecked = Convert.ToBoolean(activePlayerSettings[0]);
            DotStyleComboBox.SelectedIndex = Convert.ToInt32(activePlayerSettings[1]);

            DotStyleLogic();

            PDotSizeSlider.Value = Convert.ToInt32(activePlayerSettings[4]);

            DetectSoundCB.SelectedIndex = Convert.ToInt32(activePlayerSettings[5]);

            NickToggle.IsChecked = Convert.ToBoolean(activePlayerSettings[6]);
            NickColorCP.Color = (Color)ColorConverter.ConvertFromString(activePlayerSettings[7].ToString());
            NickColor.Background = new SolidColorBrush(NickColorCP.Color);

            AllianceToggle.IsChecked = Convert.ToBoolean(activePlayerSettings[8]);
            AllianceColorCP.Color = (Color)ColorConverter.ConvertFromString(activePlayerSettings[9].ToString());
            AllianceColor.Background = new SolidColorBrush(AllianceColorCP.Color);

            DistanceToggle.IsChecked = Convert.ToBoolean(activePlayerSettings[10]);
            DistanceColorCP.Color = (Color)ColorConverter.ConvertFromString(activePlayerSettings[11].ToString());
            DistanceColor.Background = new SolidColorBrush(DistanceColorCP.Color);

            MountedToggle.IsChecked = Convert.ToBoolean(activePlayerSettings[12]);
            MountedColorCP.Color = (Color)ColorConverter.ConvertFromString(activePlayerSettings[13].ToString());
            MountedColor.Background = new SolidColorBrush(MountedColorCP.Color);

            VisibleContactToggle.IsChecked = Convert.ToBoolean(activePlayerSettings[14]);
            VisibleContactColorCP.Color = (Color)ColorConverter.ConvertFromString(activePlayerSettings[15].ToString());
            VisibleContactColor.Background = new SolidColorBrush(VisibleContactColorCP.Color);

            HealthComboBox.SelectedIndex = Convert.ToInt32(activePlayerSettings[16]);

            if (HealthComboBox.SelectedIndex == 2)
            {
                HealthColor.Visibility = Visibility.Visible;
            }
            else
            {
                HealthColor.Visibility = Visibility.Collapsed;
            }

            HealthColorCP.Color = (Color)ColorConverter.ConvertFromString(activePlayerSettings[17].ToString());
            HealthColor.Background = new SolidColorBrush(HealthColorCP.Color);

            FocusLineToggle.IsChecked = Convert.ToBoolean(activePlayerSettings[18]);
            FocusLineColorCP.Color = (Color)ColorConverter.ConvertFromString(activePlayerSettings[19].ToString());
            FocusLineColor.Background = new SolidColorBrush(FocusLineColorCP.Color);

            VisibleContactColorCP2.Color = (Color)ColorConverter.ConvertFromString(activePlayerSettings[20].ToString());
            VisibleContactColor2.Background = new SolidColorBrush(VisibleContactColorCP2.Color);

            ItemsToggle.IsChecked = Convert.ToBoolean(activePlayerSettings[21]);
        }

        private void DotStyleLogic()
        {
            if (Convert.ToInt32(activePlayerSettings[1]) == 0)
            {
                DotMainColor.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(activePlayerSettings[2].ToString()));
                DotMainColorCP.Color = (Color)ColorConverter.ConvertFromString(activePlayerSettings[2].ToString());
                DotMainColor.IsEnabled = true;

                DotAccentColor.Background = new SolidColorBrush(Color.FromRgb(70, 70, 70));
                DotAccentColor.IsEnabled = false;

                PDotSizeSlider.IsEnabled = true;
            }
            else
            {
                DotMainColor.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(activePlayerSettings[2].ToString()));
                DotMainColorCP.Color = (Color)ColorConverter.ConvertFromString(activePlayerSettings[2].ToString());
                DotMainColor.IsEnabled = true;

                DotAccentColor.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(activePlayerSettings[3].ToString()));
                DotAccentColorCP.Color = (Color)ColorConverter.ConvertFromString(activePlayerSettings[3].ToString());
                DotAccentColor.IsEnabled = true;

                PDotSizeSlider.IsEnabled = true;
            }

            if (activePlayerTag == "Faction")
            {
                DotMainColor.Background = new SolidColorBrush(Color.FromRgb(70, 70, 70));
                DotMainColor.IsEnabled = false;
            }

            if (!Convert.ToBoolean(activePlayerSettings[0]))
            {
                DotMainColor.Background = new SolidColorBrush(Color.FromRgb(70, 70, 70));
                DotMainColor.IsEnabled = false;

                DotAccentColor.Background = new SolidColorBrush(Color.FromRgb(70, 70, 70));
                DotAccentColor.IsEnabled = false;

                PDotSizeSlider.IsEnabled = false;
            }
        }

        #endregion

        private void DotEnabled_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[0] = DotEnabled.IsChecked;
            DotStyleLogic();
        }

        private void DotStyleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[1] = DotStyleComboBox.SelectedIndex;

            DotStyleLogic();
        }

        private void DotMainColorCP_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[2] = e.NewValue;
            DotMainColor.Background = new SolidColorBrush(e.NewValue);
        }

        private void DotAccentColorCP_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[3] = e.NewValue;
            DotAccentColor.Background = new SolidColorBrush(e.NewValue);
        }

        private void PDotSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[4] = (int)PDotSizeSlider.Value;
        }

        private void DetectSoundCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[5] = DetectSoundCB.SelectedIndex;
        }

        private void ItemsToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[21] = ItemsToggle.IsChecked;
        }

        private void NickToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[6] = NickToggle.IsChecked;
        }

        private void NickColorCP_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[7] = e.NewValue;
            NickColor.Background = new SolidColorBrush(e.NewValue);
        }

        private void AllianceToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[8] = AllianceToggle.IsChecked;
        }

        private void AllianceColorCP_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[9] = e.NewValue;
            AllianceColor.Background = new SolidColorBrush(e.NewValue);
        }

        private void DistanceToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[10] = DistanceToggle.IsChecked;
        }

        private void DistanceColorCP_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[11] = e.NewValue;
            DistanceColor.Background = new SolidColorBrush(e.NewValue);
        }

        private void MountedToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[12] = MountedToggle.IsChecked;
        }

        private void MountedColorCP_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[13] = e.NewValue;
            MountedColor.Background = new SolidColorBrush(e.NewValue);
        }

        private void VisibleContactToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[14] = VisibleContactToggle.IsChecked;
        }

        private void VisibleContactColorCP_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[15] = e.NewValue;
            VisibleContactColor.Background = new SolidColorBrush(e.NewValue);
        }

        private void HealthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            if (HealthComboBox.SelectedIndex == 2)
            {
                HealthColor.Visibility = Visibility.Visible;
            }
            else
            {
                HealthColor.Visibility = Visibility.Collapsed;
            }

            activePlayerSettings[16] = HealthComboBox.SelectedIndex;
        }

        private void HealthColorCP_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[17] = e.NewValue;
            HealthColor.Background = new SolidColorBrush(e.NewValue);
        }

        private void FocusLineToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[18] = FocusLineToggle.IsChecked;
        }

        private void FocusLineColorCP_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[19] = e.NewValue;
            FocusLineColor.Background = new SolidColorBrush(e.NewValue);
        }

        private void VisibleContactColorCP2_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (MainWindow.loadingCfg) return;

            activePlayerSettings[20] = e.NewValue;
            VisibleContactColor2.Background = new SolidColorBrush(e.NewValue);
        }

        #endregion

        #region LEFT

        private void EspSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.EspEnabled = (bool)EspSwitch.IsChecked;
        }

        private void EspExtndSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.ExtendedESP = (bool)EspExtndSwitch.IsChecked;
            ExESP.IsEnabled = (bool)EspExtndSwitch.IsChecked ? true : false;
        }

        private void FactionWarSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.FactionWar = (bool)FactionWarSwitch.IsChecked;

            if (!(bool)FactionWarSwitch.IsChecked)
            {
                switchToNoPVP();

                FactionFBorder.IsEnabled = false;
                FactionEBorder.IsEnabled = false;

                FactionFText.Foreground = new SolidColorBrush(Color.FromRgb(64, 64, 64));
                FactionEText.Foreground = new SolidColorBrush(Color.FromRgb(64, 64, 64));
            }
            else
            {
                FactionFBorder.IsEnabled = true;
                FactionEBorder.IsEnabled = true;

                FactionFText.Foreground = new SolidColorBrush(Color.FromRgb(175, 199, 31));
                FactionEText.Foreground = new SolidColorBrush(Color.FromRgb(199, 100, 31));
            }
        }

        private void FriendlyPGASwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.FriendlyLists = (bool)FriendlyPGASwitch.IsChecked;

            if (!(bool)FriendlyPGASwitch.IsChecked)
            {
                switchToNoPVP();

                MPlayerFBorder.IsEnabled = false;
                MGuildFBorder.IsEnabled = false;
                MAllianceFBorder.IsEnabled = false;

                MPlayerFText.Foreground = new SolidColorBrush(Color.FromRgb(64, 64, 64));
                MGuildFText.Foreground = new SolidColorBrush(Color.FromRgb(64, 64, 64));
                MAllianceFText.Foreground = new SolidColorBrush(Color.FromRgb(64, 64, 64));
            }
            else
            {
                MPlayerFBorder.IsEnabled = true;
                MGuildFBorder.IsEnabled = true;
                MAllianceFBorder.IsEnabled = true;

                MPlayerFText.Foreground = new SolidColorBrush(Color.FromRgb(116, 199, 31));
                MGuildFText.Foreground = new SolidColorBrush(Color.FromRgb(13, 175, 134));
                MAllianceFText.Foreground = new SolidColorBrush(Color.FromRgb(161, 77, 171));
            }
        }

        private void EnemyPGASwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.EnemyLists = (bool)EnemyPGASwitch.IsChecked;

            if (!(bool)EnemyPGASwitch.IsChecked)
            {
                switchToNoPVP();

                MPlayerEBorder.IsEnabled = false;
                MGuildEBorder.IsEnabled = false;
                MAllianceEBorder.IsEnabled = false;

                MPlayerEText.Foreground = new SolidColorBrush(Color.FromRgb(64, 64, 64));
                MGuildEText.Foreground = new SolidColorBrush(Color.FromRgb(64, 64, 64));
                MAllianceEText.Foreground = new SolidColorBrush(Color.FromRgb(64, 64, 64));
            }
            else
            {
                MPlayerEBorder.IsEnabled = true;
                MGuildEBorder.IsEnabled = true;
                MAllianceEBorder.IsEnabled = true;

                MPlayerEText.Foreground = new SolidColorBrush(Color.FromRgb(199, 31, 31));
                MGuildEText.Foreground = new SolidColorBrush(Color.FromRgb(199, 31, 31));
                MAllianceEText.Foreground = new SolidColorBrush(Color.FromRgb(199, 31, 31));
            }
        }

        private void switchToNoPVP()
        {
            switch (activePlayerTag)
            {
                case "NoPVP":
                    NoPVPBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    NoPVPText.Foreground = new SolidColorBrush(Color.FromRgb(4, 180, 255));
                    break;

                case "PVP":
                    PVPBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    PVPText.Foreground = new SolidColorBrush(Color.FromRgb(199, 31, 31));
                    PVPText.Filled = false;
                    break;

                case "Faction":
                    FactionBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    FactionText.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    break;

                case "FactionF":
                    FactionFBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    FactionFText.Foreground = new SolidColorBrush(Color.FromRgb(175, 199, 31));
                    break;

                case "FactionE":
                    FactionEBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    FactionEText.Foreground = new SolidColorBrush(Color.FromRgb(199, 100, 31));
                    break;

                case "Bounty":
                    BountyBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    BountyText.Foreground = new SolidColorBrush(Color.FromRgb(241, 255, 0));
                    break;

                case "MPlayerF":
                    MPlayerFBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    MPlayerFText.Foreground = new SolidColorBrush(Color.FromRgb(116, 199, 31));
                    break;

                case "MGuildF":
                    MGuildFBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    MGuildFText.Foreground = new SolidColorBrush(Color.FromRgb(13, 175, 134));
                    break;

                case "MAllianceF":
                    MAllianceFBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    MAllianceFText.Foreground = new SolidColorBrush(Color.FromRgb(161, 77, 171));
                    break;

                case "MPlayerE":
                    MPlayerEBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    MPlayerEText.Foreground = new SolidColorBrush(Color.FromRgb(199, 31, 31));
                    break;

                case "MGuildE":
                    MGuildEBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    MGuildEText.Foreground = new SolidColorBrush(Color.FromRgb(199, 31, 31));
                    break;

                case "MAllianceE":
                    MAllianceEBorder.Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    MAllianceEText.Foreground = new SolidColorBrush(Color.FromRgb(199, 31, 31));
                    break;
            }

            CustomListBorder.IsEnabled = false;
            activePlayerSettings = configHandler.config.NoPvp;
            NoPVPBorder.Background = new SolidColorBrush(Color.FromRgb(30, 134, 216));
            NoPVPText.Foreground = new SolidColorBrush(Color.FromRgb(15, 15, 15));

            activePlayerTag = "NoPVP";
            activePlayerSettings = configHandler.config.NoPvp;
            LoadPlayerSettings();
        }

        private void ManualList_KeyDown(object sender, KeyEventArgs e)
        {
            if (MainWindow.loadingCfg) return;
            if (e.Key != Key.Delete) return;

            var list = CustomList.ItemsSource as List<string>;

            list.Remove((string)CustomList.SelectedItem);
            CustomList.Items.Refresh();
            ListCounter.Visibility = CustomList.Items.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        private void ManualListTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (MainWindow.loadingCfg) return;
            if (e.Key != Key.Enter) return;

            var list = CustomList.ItemsSource as List<string>;
            if (list.Contains(ListTextBox.Text) || ListTextBox.Text == string.Empty) return;

            list.Add(ListTextBox.Text);
            CustomList.Items.Refresh();
            
            if (App.Language.Name == "ru-RU")
            {
                ListTextBox.Text = "Введи текст . . .";
            }
            else
            {
                ListTextBox.Text = "Input text . . .";
            }

            ListCounter.Visibility = CustomList.Items.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        private void ListTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ListTextBox.Text = string.Empty;
        }

        private void ListTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (App.Language.Name == "ru-RU")
            {
                ListTextBox.Text = "Введи текст . . .";
            }
            else
            {
                ListTextBox.Text = "Input text . . .";
            }
        }

        #endregion

        #region POPUP

        private void DotMainColor_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            DotMainColorPP.IsOpen = true;
        }

        private void DotAccentColor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DotAccentColorPP.IsOpen = true;
        }

        private void NickColor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NickColorPP.IsOpen = true;
        }

        private void AllianceColor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AllianceColorPP.IsOpen = true;
        }

        private void DistanceColor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DistanceColorPP.IsOpen = true;
        }

        private void MountedColor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MountedColorPP.IsOpen = true;
        }

        private void VisibleContactColor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            VisibleContactColorPP.IsOpen = true;
        }

        private void VisibleContactColor2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            VisibleContactColorPP2.IsOpen = true;
        }

        private void HealthColor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HealthColorPP.IsOpen = true;
        }

        private void FocusLineColor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FocusLineColorPP.IsOpen = true;
        }

        #endregion
    }
}
