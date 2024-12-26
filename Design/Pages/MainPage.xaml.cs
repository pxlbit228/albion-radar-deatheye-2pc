using System;
using X975.Settings;
using X975.Tools;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;
using SharpPcap;

namespace X975.Pages
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public partial class MainPage : Page
    {
        ConfigHandler configHandler = ConfigHandler.Source;
        public static string gateway = string.Empty;

        #region HOOK

        private bool showMainWindow = true;
        private bool bindKeyClose, bindKeyHideRadar, bindKeyHideMeny = false;

        KeyboardHook keyboardHook = new KeyboardHook();
        MainWindow MainWindow;

        #endregion

        public MainPage (MainWindow MainWindow)
        {
            InitializeComponent();
            Title = Utility.RandomString();

            keyboardHook.KeyDown += new KeyboardHook.KeyboardHookCallback(keyboardHook_KeyDown);
            keyboardHook.Install();
            this.MainWindow = MainWindow;
        }

        private void keyboardHook_KeyDown(KeyboardHook.VKeys key)
        {
            #region UPDATE BINDS

            if (bindKeyHideMeny)
            {
                if (key == configHandler.config.ToggleRadarKey || key == configHandler.config.CloseKey)
                {
                    bindKeyHideMeny = false;
                    return;
                }

                configHandler.config.ToggleMenyKey = key;
                bindKeyHideMeny = false;
                ToggleMenyText.Text = key.ToString();
                return;
            }

            if (bindKeyHideRadar)
            {
                if (key == configHandler.config.ToggleMenyKey || key == configHandler.config.CloseKey)
                {
                    bindKeyHideRadar = false;
                    return;
                }

                configHandler.config.ToggleRadarKey = key;
                bindKeyHideRadar = false;
                ToggleMenyRadar.Text = key.ToString();
                return;
            }

            if (bindKeyClose)
            {
                if (key == configHandler.config.ToggleMenyKey || key == configHandler.config.ToggleRadarKey)
                {
                    bindKeyClose = false;
                    return;
                }

                configHandler.config.CloseKey = key;
                bindKeyClose = false;
                CloseRadarText.Text = key.ToString();
                return;
            }

            #endregion

            if (key == configHandler.config.ToggleMenyKey)
            {
                if (showMainWindow)
                {
                    MainWindow.Hide();
                    showMainWindow = false;
                }
                else
                {
                    MainWindow.Show();
                    showMainWindow = true;
                }
            }

            if (key == configHandler.config.ToggleRadarKey)
            {
                MainWindow.ShowRadar = !MainWindow.ShowRadar;
            }

            if (key == configHandler.config.CloseKey)
            {
                keyboardHook.Uninstall();
                Application.Current.Shutdown();
            }
        }

        public void UpdateSettings()
        {
            App.Language = configHandler.config.Language;

            ToggleMenyText.Text = configHandler.config.ToggleMenyKey.ToString();
            ToggleMenyRadar.Text = configHandler.config.ToggleRadarKey.ToString();
            CloseRadarText.Text = configHandler.config.CloseKey.ToString();

            switch (configHandler.config.Language.Name)
            {
                case "en-US":
                    Lang_CB.SelectedIndex = 0;
                    break;

                case "ru-RU":
                    Lang_CB.SelectedIndex = 1;
                    break;
            }
        }

        private void Lang_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            switch (Lang_CB.SelectedIndex)
            {
                case 0:
                    App.Language = new CultureInfo("en-US");
                    break;

                case 1:
                    App.Language = new CultureInfo("ru-RU");
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (bindKeyHideRadar || bindKeyHideMeny || bindKeyClose) return;

            bindKeyHideMeny = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (bindKeyHideRadar || bindKeyHideMeny || bindKeyClose) return;

            bindKeyHideRadar = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (bindKeyHideRadar || bindKeyHideMeny || bindKeyClose) return;

            bindKeyClose = true;
        }
    }
}
