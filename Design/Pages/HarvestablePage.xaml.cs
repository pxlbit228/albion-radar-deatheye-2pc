using X975.Settings;
using X975.Tools;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Wpf.Ui.Controls;
using Button = System.Windows.Controls.Button;
using System.Reflection;

namespace X975.Pages
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public partial class HarvestablePage : Page
    {
        ConfigHandler configHandler = ConfigHandler.Source;

        #region DESIGN

        string activeResource = "WOOD";
        bool loadRes = true;
        SolidColorBrush select = new SolidColorBrush(Color.FromArgb(255, 116, 199, 31));
        SolidColorBrush deselect = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

        #endregion

        public HarvestablePage()
        {
            InitializeComponent();
            Title = Utility.RandomString();
        }

        public void UpdateSettings()
        {
            try
            {
                ResourcesSwitch.IsChecked = configHandler.config.ResourcesEnabled;
                StackSwitch.IsChecked = configHandler.config.StackSize;
                StackFilterSlider.Value = ValidateValue(configHandler.config.StackFilter, 0, 10);

                ResMobsSwitch.IsChecked = configHandler.config.ResourcesMobsEnabled;
                OnlyAspectedSwitch.IsChecked = configHandler.config.OnlyAspectedMode;

                SizeSlider.Value = ValidateValue(configHandler.config.HarvestableDotSize, 4, 16);

                LoadResources();
            }
            catch
            {
                ConfigHandler.Source.BadConfig();
            }
        }

        #region LEFT

        private void ShowResources(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.ResourcesEnabled = (bool)ResourcesSwitch.IsChecked;
        }

        private void ShowStacks(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.StackSize = (bool)StackSwitch.IsChecked;
        }

        private void StackFilterSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.StackFilter = (int)StackFilterSlider.Value;
        }

        private void ShowResMobs(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.ResourcesMobsEnabled = (bool)ResMobsSwitch.IsChecked;
        }

        private void OnlyAspectedMode(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.OnlyAspectedMode = (bool)OnlyAspectedSwitch.IsChecked;
        }

        private void SizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.HarvestableDotSize = (int)SizeSlider.Value;
        }

        #endregion

        #region RIGHT

        private void SwitchResource(object sender, RoutedEventArgs e)
        {
            string chooseResource = (e.Source as Button).Tag.ToString();

            if (activeResource == chooseResource) return;

            switch (activeResource)
            {
                case "WOOD":
                    WoodBtn.BorderBrush = deselect;
                    break;

                case "ROCK":
                    RockBtn.BorderBrush = deselect;
                    break;

                case "ORE":
                    OreBtn.BorderBrush = deselect;
                    break;

                case "FIBER":
                    FiberBtn.BorderBrush = deselect;
                    break;

                case "HIDE":
                    HideBtn.BorderBrush = deselect;
                    break;
            }

            switch (chooseResource)
            {
                case "WOOD":
                    WoodBtn.BorderBrush = select;
                    T1_0.IsEnabled = true;
                    break;

                case "ROCK":
                    RockBtn.BorderBrush = select;
                    T1_0.IsEnabled = true;
                    break;

                case "ORE":
                    OreBtn.BorderBrush = select;
                    T1_0.IsEnabled = false;
                    break;

                case "FIBER":
                    FiberBtn.BorderBrush = select;
                    T1_0.IsEnabled = false;
                    break;

                case "HIDE":
                    HideBtn.BorderBrush = select;
                    T1_0.IsEnabled = true;
                    break;
            }

            activeResource = chooseResource;

            loadRes = true;
            LoadResources();
        }

        private void LoadResources()
        {
            var resList = configHandler.config.HarvestableList.FindAll(s => s.Contains(activeResource));

            #region BTN DESIGN
            T1_0.IsChecked = false;
            T2_0.IsChecked = false;
            T3_0.IsChecked = false;

            T4_0.IsChecked = false;
            T4_1.IsChecked = false;
            T4_2.IsChecked = false;
            T4_3.IsChecked = false;

            T5_0.IsChecked = false;
            T5_1.IsChecked = false;
            T5_2.IsChecked = false;
            T5_3.IsChecked = false;

            T6_0.IsChecked = false;
            T6_1.IsChecked = false;
            T6_2.IsChecked = false;
            T6_3.IsChecked = false;

            T7_0.IsChecked = false;
            T7_1.IsChecked = false;
            T7_2.IsChecked = false;
            T7_3.IsChecked = false;

            T8_0.IsChecked = false;
            T8_1.IsChecked = false;
            T8_2.IsChecked = false;
            T8_3.IsChecked = false;
            #endregion

            foreach (string res in resList)
            {
                var temp = res.Split('-');

                switch (temp[0])
                {
                    case "T1":
                        T1_0.IsChecked = true;
                        break;

                    case "T2":
                        T2_0.IsChecked = true;
                        break;

                    case "T3":
                        T3_0.IsChecked = true;
                        break;

                    case "T4":
                        switch (temp[1])
                        {
                            case "0":
                                T4_0.IsChecked = true;
                                break;

                            case "1":
                                T4_1.IsChecked = true;
                                break;

                            case "2":
                                T4_2.IsChecked = true;
                                break;

                            case "3":
                                T4_3.IsChecked = true;
                                break;
                        }
                        break;

                    case "T5":
                        switch (temp[1])
                        {
                            case "0":
                                T5_0.IsChecked = true;
                                break;

                            case "1":
                                T5_1.IsChecked = true;
                                break;

                            case "2":
                                T5_2.IsChecked = true;
                                break;

                            case "3":
                                T5_3.IsChecked = true;
                                break;
                        }
                        break;

                    case "T6":
                        switch (temp[1])
                        {
                            case "0":
                                T6_0.IsChecked = true;
                                break;

                            case "1":
                                T6_1.IsChecked = true;
                                break;

                            case "2":
                                T6_2.IsChecked = true;
                                break;

                            case "3":
                                T6_3.IsChecked = true;
                                break;
                        }
                        break;

                    case "T7":
                        switch (temp[1])
                        {
                            case "0":
                                T7_0.IsChecked = true;
                                break;

                            case "1":
                                T7_1.IsChecked = true;
                                break;

                            case "2":
                                T7_2.IsChecked = true;
                                break;

                            case "3":
                                T7_3.IsChecked = true;
                                break;
                        }
                        break;

                    case "T8":
                        switch (temp[1])
                        {
                            case "0":
                                T8_0.IsChecked = true;
                                break;

                            case "1":
                                T8_1.IsChecked = true;
                                break;

                            case "2":
                                T8_2.IsChecked = true;
                                break;

                            case "3":
                                T8_3.IsChecked = true;
                                break;
                        }
                        break;

                }
            }

            loadRes = false;
        }

        private void AddResource(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            if (loadRes) return;

            string resource = (string)((ToggleSwitch)sender).Tag + "-" + activeResource;

            if (configHandler.config.HarvestableList.Contains(resource))
            {
                configHandler.config.HarvestableList.Remove(resource);
            }
            else
            {
                configHandler.config.HarvestableList.Add(resource);
            }
        }

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

        #endregion

    }
}
