using X975.Pages;
using X975.Settings;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using X975.Radar;
using X975.Tools;

namespace X975
{
    public partial class MainWindow : Window
    {
        #region WINDOWS

        ConfigHandler configHandler = ConfigHandler.Source;

        string selectedPage = "Players";
        SolidColorBrush select = new SolidColorBrush(Color.FromArgb(178, 71, 71, 71));
        SolidColorBrush deselect = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

        SolidColorBrush white = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        SolidColorBrush green = new SolidColorBrush(Color.FromRgb(116, 199, 31));

        PlayersPage playersPage = new PlayersPage();
        HarvestablePage harvestablePage = new HarvestablePage();
        MobsPage mobsPage = new MobsPage();
        DungeonsPage dungeonsPage = new DungeonsPage();
        StylePage stylePage = new StylePage();

        MapPage mapPage = new MapPage();
        ItemsPage itemsPage = new ItemsPage();

        MainPage mainPage;
        SupportPage supportPage = new SupportPage();
        MistsPage mistsPage = new MistsPage();
        ConfigPage configPage;

        #endregion

        public static bool loadingCfg = true;
        public static bool ShowRadar = true;

        private Init Init = new Init();

        public MainWindow()
        {
            InitializeComponent();
            Title = Utility.RandomString();

            #region TIMER

            Thread UpdateTimer = new Thread(() => SyncDT());
            UpdateTimer.IsBackground = true;
            UpdateTimer.Start();

            #endregion

            #region PAGES LOAD

            mainPage = new MainPage(this);
            configPage = new ConfigPage(this);

            Wrapper.Navigate(playersPage);
            ConfigCB.ItemsSource = configHandler.ConfigList;
            UpdateSettings();
            loadingCfg = false;

            #endregion
        }

        #region UI

        private void SyncDT()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            while (true)
            {
                Thread.Sleep(1000);
                Dispatcher.Invoke(() => TimerText.Text = string.Format("{0:00}:{1:00}:{2:00}", stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes, stopWatch.Elapsed.Seconds));
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void DragMoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void MenyNavigate(object sender, RoutedEventArgs e)
        {
            string selectablePage = (e.Source as Button).Tag.ToString();
            if (selectedPage == selectablePage) return;

            switch (selectedPage)
            {
                case "Players":
                    PlayersButton.Background = deselect;
                    PlayersIcon.Filled = false;
                    break;

                case "Harvestable":
                    HarvestableButton.Background = deselect;
                    HarvestableIcon.Filled = false;
                    break;

                case "Mobs":
                    MobsButton.Background = deselect;
                    MobsIcon.Filled = false;
                    break;

                case "Dungeons":
                    DungeonsButton.Background = deselect;
                    DungeonsIcon.Filled = false;
                    break;

                case "Style":
                    StyleButton.Background = deselect;
                    StyleIcon.Filled = false;
                    break;

                case "Items":
                    ItemsButton.Background = deselect;
                    ItemsIcon.Filled = false;
                    break;

                case "Mists":
                    MistsButton.Background = deselect;
                    MistsIcon.Filled = false;
                    break;

                case "Main":
                    MainButton.Background = deselect;
                    MainIcon.Filled = false;
                    break;

                case "Support":
                    SupportButton.Background = deselect;
                    SupportIcon.Filled = false;
                    break;

                case "Configs":
                    ConfigsButton.Background = deselect;
                    ConfigsIcon.Filled = false;
                    break;
            }

            switch (selectablePage)
            {
                case "Players":
                    PlayersButton.Background = select;
                    PlayersIcon.Filled = true;
                    Wrapper.Navigate(playersPage);
                    break;

                case "Harvestable":
                    HarvestableButton.Background = select;
                    HarvestableIcon.Filled = true;
                    Wrapper.Navigate(harvestablePage);
                    break;

                case "Mobs":
                    MobsButton.Background = select;
                    MobsIcon.Filled = true;
                    Wrapper.Navigate(mobsPage);
                    break;

                case "Dungeons":
                    DungeonsButton.Background = select;
                    DungeonsIcon.Filled = true;
                    Wrapper.Navigate(dungeonsPage);
                    break;

                case "Style":
                    StyleButton.Background = select;
                    StyleIcon.Filled = true;
                    Wrapper.Navigate(stylePage);
                    break;

                case "Items":
                    ItemsButton.Background = select;
                    ItemsIcon.Filled = true;
                    Wrapper.Navigate(itemsPage);
                    break;

                case "Mists":
                    MistsButton.Background = select;
                    MistsIcon.Filled = true;
                    Wrapper.Navigate(mistsPage);
                    break;

                case "Main":
                    MainButton.Background = select;
                    MainIcon.Filled = true;
                    Wrapper.Navigate(mainPage);
                    break;

                case "Support":
                    SupportButton.Background = select;
                    SupportIcon.Filled = true;
                    Wrapper.Navigate(supportPage);
                    break;

                case "Configs":
                    ConfigsButton.Background = select;
                    ConfigsIcon.Filled = true;
                    Wrapper.Navigate(configPage);
                    break;
            }

            selectedPage = selectablePage;
        }

        private void ShowTimer(object sender, MouseButtonEventArgs e)
        {
            if (TimerText.Visibility == Visibility.Visible)
            {
                TimerText.Visibility = Visibility.Collapsed;
                TimerIcon.Foreground = white;
            }
            else
            {
                TimerText.Visibility = Visibility.Visible;
                TimerIcon.Foreground = green;
            }
        }

        private void ShowVersion(object sender, MouseButtonEventArgs e)
        {
            if (VersionText.Visibility == Visibility.Visible)
            {
                VersionText.Visibility = Visibility.Collapsed;
                VersionIcon.Foreground = white;
            }
            else
            {
                VersionText.Visibility = Visibility.Visible;
                VersionIcon.Foreground = green;
            }
        }

        #endregion

        #region CONFIG

        private void SaveCFG(object sender, RoutedEventArgs e)
        {
            configHandler.SaveConfig();
        }

        private void ConfigCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loadingCfg) return;

            loadingCfg = true;

            configHandler.selectedConfig = (e.Source as ComboBox).SelectedItem.ToString();
            configHandler.LoadConfig();

            UpdateSettings();

            loadingCfg = false;
        }

        public void UpdateSettings()
        {
            playersPage.UpdateSettings();
            harvestablePage.UpdateSettings();
            mobsPage.UpdateSettings();
            dungeonsPage.UpdateSettings();
            stylePage.UpdateSettings();

            mapPage.UpdateSettings();
            itemsPage.UpdateSettings();
            mistsPage.UpdateSettings();

            mainPage.UpdateSettings();
        }

        #endregion

        private async void Window_Loaded(object sender, RoutedEventArgs e) => Init.Start();
    }
}
