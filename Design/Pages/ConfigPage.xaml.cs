using X975.Settings;
using X975.Tools;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;

namespace X975.Pages
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public partial class ConfigPage : Page
    {
        MainWindow MainWindow;
        public ConfigPage(MainWindow MainWindow)
        {
            InitializeComponent();
            this.MainWindow = MainWindow;

            ConfigList.ItemsSource = ConfigHandler.Source.ConfigList;
        }

        private void ConfigList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConfigList.SelectedItem != null)

            ActiveConfigTB.PlaceholderText = ConfigList.SelectedItem.ToString() + " *";
            ImportConfigTB.PlaceholderText = ConfigList.SelectedItem.ToString() + " *";
        }

        private void Actions(object sender, RoutedEventArgs e)
        {
            string configname = ActiveConfigTB.PlaceholderText.Substring(0, ActiveConfigTB.PlaceholderText.Length - 2);

            switch ((e.Source as Button).Tag)
            {
                case "Rename":

                    if (ActiveConfigTB.Text.Length == 0) return;

                    if (ConfigHandler.Source.ConfigList.Exists(x => x.Contains(ActiveConfigTB.Text))) return;

                    File.Move(Pathfinder.mainFolder + "\\" + configname + ".cfg", Pathfinder.mainFolder + "\\" + ActiveConfigTB.Text + ".cfg");

                    ConfigHandler.Source.ConfigList.Add(ActiveConfigTB.Text);
                    ConfigHandler.Source.ConfigList.Remove(configname);

                    if (ConfigHandler.Source.selectedConfig == configname) MainWindow.ConfigCB.SelectedItem = ActiveConfigTB.Text;
                    ConfigList.SelectedItem = ActiveConfigTB.Text;

                    ConfigList.Items.Refresh();
                    MainWindow.ConfigCB.Items.Refresh();
                    ActiveConfigTB.Text = string.Empty;
                    break;

                case "Copy":

                    if (configname.Contains(" (Copy)")) return;

                    if (!ConfigHandler.Source.ConfigList.Exists(x=> x.Contains(configname + " (Copy)")))
                    {
                        File.Copy(Pathfinder.mainFolder + "\\" + configname + ".cfg", Pathfinder.mainFolder + "\\" + configname + " (Copy)" + ".cfg");
                        ConfigHandler.Source.ConfigList.Add(configname + " (Copy)");
                        ConfigList.Items.Refresh();
                        MainWindow.ConfigCB.Items.Refresh();
                    }

                    break;

                case "Delete":

                    if (ConfigHandler.Source.ConfigList.Exists(x => x.Contains(configname)) && ConfigHandler.Source.ConfigList.Count() > 1)
                    {
                        ConfigHandler.Source.ConfigList.Remove(configname);
                        File.Delete(Pathfinder.mainFolder + "\\" + configname + ".cfg");

                        if (ConfigHandler.Source.selectedConfig == configname) MainWindow.ConfigCB.SelectedItem = ConfigHandler.Source.ConfigList.First();

                        ConfigList.SelectedItem = ConfigHandler.Source.ConfigList.First();
                        ConfigList.Items.Refresh();
                        MainWindow.ConfigCB.Items.Refresh();
                    }

                    break;

                case "Export":
                    Clipboard.SetText(Convert.ToBase64String(Encoding.UTF8.GetBytes(File.ReadAllText(Pathfinder.mainFolder + "\\" + configname + ".cfg"))));
                    break;
            }
        }

        private void Import(object sender, RoutedEventArgs e)
        {
            if (ImportKey.Text.Length < 0) return;

            string configname = ActiveConfigTB.PlaceholderText.Substring(0, ActiveConfigTB.PlaceholderText.Length - 2);

            try
            {
                var json = JsonConvert.DeserializeObject<Config>(Encoding.UTF8.GetString(Convert.FromBase64String(ImportKey.Text)));

                if (ImportConfigTB.Text.Length == 0)
                {
                    File.WriteAllText(Pathfinder.mainFolder + "\\" + configname + ".cfg", JsonConvert.SerializeObject(json, Formatting.Indented));
                    if (ConfigHandler.Source.selectedConfig == configname) ConfigHandler.Source.LoadConfig();
                }
                else
                {
                    File.WriteAllText(Pathfinder.mainFolder + "\\" + ImportConfigTB.Text + ".cfg", JsonConvert.SerializeObject(json, Formatting.Indented));

                    if (!ConfigHandler.Source.ConfigList.Exists(x => x.Contains(ImportConfigTB.Text)))
                    {
                        ConfigHandler.Source.ConfigList.Add(ImportConfigTB.Text);
                        ConfigList.Items.Refresh();
                        MainWindow.ConfigCB.Items.Refresh();
                    }
                    
                    if (ConfigHandler.Source.selectedConfig == ImportConfigTB.Text) ConfigHandler.Source.LoadConfig();
                }

                MainWindow.loadingCfg = true;
                MainWindow.UpdateSettings();
                MainWindow.loadingCfg = false;

                ImportKey.Text = "Imported!";
            }
            catch
            {
                ImportKey.Text = "Error!";
            }
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            if (CreateConfigTB.Text.Length < 0) return;
            if (ConfigHandler.Source.ConfigList.Exists(x => x.Contains(CreateConfigTB.Text))) return;

            ConfigHandler.Source.CreateConfig(CreateConfigTB.Text);
            ConfigHandler.Source.ConfigList.Add(CreateConfigTB.Text);
            ConfigList.Items.Refresh();
            MainWindow.ConfigCB.Items.Refresh();
        }
    }
}
