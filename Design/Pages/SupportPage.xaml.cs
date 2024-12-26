using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace X975.Pages
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public partial class SupportPage : Page
    {
        public SupportPage()
        {
            InitializeComponent();
        }

        private void Links(object sender, RoutedEventArgs e)
        {
            switch ((e.Source as Button).Tag)
            {
                case "Discord":
                    System.Diagnostics.Process.Start("https://discord.gg/Jhr5Y7qrCY");
                    break;

                case "Github":
                    System.Diagnostics.Process.Start("https://github.com/pxlbit228");
                    break;

                case "Youtube":
                    System.Diagnostics.Process.Start("https://www.youtube.com/@goodbyesbi");
                    break;
            }
        }
    }
}
