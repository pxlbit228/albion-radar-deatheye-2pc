using X975.Settings;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;

namespace X975.Pages
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public partial class MistsPage : Page
    {
        ConfigHandler configHandler = ConfigHandler.Source;

        public MistsPage()
        {
            InitializeComponent();
        }

        public void UpdateSettings()
        {
            try
            {
                EnabledSwitch.IsChecked = configHandler.config.MistOverlayEnabled;
                XSlider.Value = configHandler.config.MistOverlayX;
                YSlider.Value = configHandler.config.MistOverlayY;
            }
            catch
            {
                ConfigHandler.Source.BadConfig();
            }
        }

        private void EnabledSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.MistOverlayEnabled = (bool)EnabledSwitch.IsChecked;
        }

        private void XSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.MistOverlayX = (int)XSlider.Value;
        }

        private void YSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.MistOverlayY = (int)YSlider.Value;
        }
    }
}
