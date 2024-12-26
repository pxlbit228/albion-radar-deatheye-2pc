using X975.Settings;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;

namespace X975.Pages
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public partial class MapPage : Page
    {
        ConfigHandler configHandler = ConfigHandler.Source;

        public MapPage()
        {
            InitializeComponent();
        }

        public void UpdateSettings()
        {
            try
            {
                MapOpacitySlider.Value = configHandler.config.MapOpacity;
                MapSwitch.IsChecked = configHandler.config.MapUnderRadar;
            }
            catch
            {
                ConfigHandler.Source.BadConfig();
            }
        }

        private void MapOpacitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.MapOpacity = (int)MapOpacitySlider.Value;
        }

        private void MapSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.MapUnderRadar = (bool)MapSwitch.IsChecked;
        }
    }
}
