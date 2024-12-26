using X975.Settings;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;

namespace X975.Pages
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public partial class ItemsPage : Page
    {
        ConfigHandler configHandler = ConfigHandler.Source;

        public ItemsPage()
        {
            InitializeComponent();
        }

        public void UpdateSettings()
        {
            try
            {
                ItemsSwitch.IsChecked = configHandler.config.ShowItems;
                OverlayStyleCB.SelectedIndex = configHandler.config.ItemsStyle;
                HealthSwitch.IsChecked = configHandler.config.ShowHealth;
                IPSwitch.IsChecked = configHandler.config.ShowMinIP;
                TrashSwitch.IsChecked = configHandler.config.ShowTrash;
                CountSlider.Value = configHandler.config.LinesCount;


                XSlider.Value = configHandler.config.ItemsXoffset;
                YSlider.Value = configHandler.config.ItemsYoffset;
                ScaleSlider.Value = configHandler.config.ItemsScale;

                WeaponSwitch.IsChecked = configHandler.config.EquipmentParts[0];
                HeadSwitch.IsChecked = configHandler.config.EquipmentParts[2];
                BodySwitch.IsChecked = configHandler.config.EquipmentParts[3];
                BootsSwitch.IsChecked = configHandler.config.EquipmentParts[4];
                BagSwitch.IsChecked = configHandler.config.EquipmentParts[5];
                CapeSwitch.IsChecked = configHandler.config.EquipmentParts[6];
                MountSwitch.IsChecked = configHandler.config.EquipmentParts[7];
            }
            catch
            {
                ConfigHandler.Source.BadConfig();
            }
        }

        #region LEFT

        private void ItemsSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.ShowItems = (bool)ItemsSwitch.IsChecked;
        }

        private void OverlayStyleCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.ItemsStyle = OverlayStyleCB.SelectedIndex;
        }

        private void HealthSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.ShowHealth = (bool)HealthSwitch.IsChecked;
        }

        private void TrashSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.ShowTrash = (bool)TrashSwitch.IsChecked;
        }

        private void ShowMinIPSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.ShowMinIP = (bool)IPSwitch.IsChecked;
        }

        private void CountSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.LinesCount = (int)CountSlider.Value;
        }

        #endregion

        #region RIGHT

        private void ScaleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.ItemsScale = ScaleSlider.Value;
        }

        private void XSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.ItemsXoffset = (int)XSlider.Value;
        }

        private void YSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.ItemsYoffset = (int)YSlider.Value;
        }

        private void WeaponSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.EquipmentParts[0] = (bool)WeaponSwitch.IsChecked;
            configHandler.config.EquipmentParts[1] = (bool)WeaponSwitch.IsChecked;
        }

        private void HeadSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.EquipmentParts[2] = (bool)HeadSwitch.IsChecked;
        }

        private void BodySwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.EquipmentParts[3] = (bool)BodySwitch.IsChecked;
        }

        private void BootsSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.EquipmentParts[4] = (bool)BootsSwitch.IsChecked;
        }

        private void BagSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.EquipmentParts[5] = (bool)BagSwitch.IsChecked;
        }

        private void CapeSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.EquipmentParts[6] = (bool)CapeSwitch.IsChecked;
        }

        private void MountSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.EquipmentParts[7] = (bool)MountSwitch.IsChecked;
        }

        #endregion
    }
}
