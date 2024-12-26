using X975.Settings;
using X975.Tools;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Reflection;

namespace X975.Pages
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public partial class StylePage : Page
    {
        ConfigHandler configHandler = ConfigHandler.Source;
        int activeLayer = 0;
        double screenWidth = SystemParameters.PrimaryScreenWidth;
        double screenHeight = SystemParameters.PrimaryScreenHeight;

        public StylePage()
        {
            InitializeComponent();
            Title = Utility.RandomString();
        }

        public void UpdateSettings()
        {
            try
            {
                #region LEFT

                BackgroundColorPicker.Color = (Color)ColorConverter.ConvertFromString(configHandler.config.StyleSettings[0].ToString());

                MeshColorPicker.Color = (Color)ColorConverter.ConvertFromString(configHandler.config.StyleSettings[1].ToString());
                MeshThiknessSlider.Value = Convert.ToInt32(configHandler.config.StyleSettings[2]);

                OutColorPicker.Color = (Color)ColorConverter.ConvertFromString(configHandler.config.StyleSettings[3].ToString());
                OutThiknessSlider.Value = Convert.ToInt32(configHandler.config.StyleSettings[4]);

                switch (activeLayer)
                {
                    case 0:
                        FigureCB.SelectedIndex = int.Parse(configHandler.config.StyleSettings[8].ToString());
                        FovScaleSlider.Value = int.Parse(configHandler.config.StyleSettings[7].ToString());
                        FovThiknessSlider.Value = int.Parse(configHandler.config.StyleSettings[6].ToString());
                        FovColorPicker.Color = (Color)ColorConverter.ConvertFromString(configHandler.config.StyleSettings[5].ToString());
                        break;

                    case 1:
                        FigureCB.SelectedIndex = int.Parse(configHandler.config.StyleSettings[12].ToString());
                        FovScaleSlider.Value = int.Parse(configHandler.config.StyleSettings[11].ToString());
                        FovThiknessSlider.Value = int.Parse(configHandler.config.StyleSettings[10].ToString());
                        FovColorPicker.Color = (Color)ColorConverter.ConvertFromString(configHandler.config.StyleSettings[9].ToString());
                        break;

                    case 2:
                        FigureCB.SelectedIndex = int.Parse(configHandler.config.StyleSettings[16].ToString());
                        FovScaleSlider.Value = int.Parse(configHandler.config.StyleSettings[15].ToString());
                        FovThiknessSlider.Value = int.Parse(configHandler.config.StyleSettings[14].ToString());
                        FovColorPicker.Color = (Color)ColorConverter.ConvertFromString(configHandler.config.StyleSettings[13].ToString());
                        break;
                }

                if (FigureCB.SelectedIndex > 1)
                {
                    ThiknessGrid.Visibility = Visibility.Collapsed;
                }
                else
                {
                    ThiknessGrid.Visibility = Visibility.Visible;
                }

                CenterColorPicker.Color = (Color)ColorConverter.ConvertFromString(configHandler.config.StyleSettings[17].ToString());
                CenterThiknessSlider.Value = Convert.ToInt32(configHandler.config.StyleSettings[18]);
                CenterCB.SelectedIndex = Convert.ToInt32(configHandler.config.StyleSettings[19]);

                #endregion

                #region RIGHT

                ScaleSlider.Value = configHandler.config.Zoom;
                HeightSlider.Value = configHandler.config.Height;
                WidthSlider.Value = configHandler.config.Width;
                SyncHaWToogle.IsChecked = configHandler.config.SyncHaW;
                WidthSlider.IsEnabled = (bool)SyncHaWToogle.IsChecked ? false : true;

                XSlider.Value = configHandler.config.X;
                YSlider.Value = configHandler.config.Y;

                #endregion
            }
            catch
            {
                ConfigHandler.Source.BadConfig();
            }
        }

        #region LEFT

        #region CENTER

        private void CenterCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.StyleSettings[19] = CenterCB.SelectedIndex;
        }

        private void CenterThiknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.StyleSettings[18] = (int)CenterThiknessSlider.Value;
        }

        private void CenterColorPicker_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.StyleSettings[17] = e.NewValue;
        }

        #endregion

        #region FOV

        private void LayerCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            activeLayer = LayerCB.SelectedIndex;

            switch (activeLayer)
            {
                case 0:
                    FigureCB.SelectedIndex = int.Parse(configHandler.config.StyleSettings[8].ToString());
                    FovScaleSlider.Value = int.Parse(configHandler.config.StyleSettings[7].ToString());
                    FovThiknessSlider.Value = int.Parse(configHandler.config.StyleSettings[6].ToString());
                    FovColorPicker.Color = (Color)ColorConverter.ConvertFromString(configHandler.config.StyleSettings[5].ToString());
                    break;

                case 1:
                    FigureCB.SelectedIndex = int.Parse(configHandler.config.StyleSettings[12].ToString());
                    FovScaleSlider.Value = int.Parse(configHandler.config.StyleSettings[11].ToString());
                    FovThiknessSlider.Value = int.Parse(configHandler.config.StyleSettings[10].ToString());
                    FovColorPicker.Color = (Color)ColorConverter.ConvertFromString(configHandler.config.StyleSettings[9].ToString());
                    break;

                case 2:
                    FigureCB.SelectedIndex = int.Parse(configHandler.config.StyleSettings[16].ToString());
                    FovScaleSlider.Value = int.Parse(configHandler.config.StyleSettings[15].ToString());
                    FovThiknessSlider.Value = int.Parse(configHandler.config.StyleSettings[14].ToString());
                    FovColorPicker.Color = (Color)ColorConverter.ConvertFromString(configHandler.config.StyleSettings[13].ToString());
                    break;
            }
        }

        private void FigureCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            if (FigureCB.SelectedIndex > 1)
            {
                ThiknessGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                ThiknessGrid.Visibility = Visibility.Visible;
            }

            switch (activeLayer)
            {
                case 0:
                    configHandler.config.StyleSettings[8] = FigureCB.SelectedIndex;
                    break;

                case 1:
                    configHandler.config.StyleSettings[12] = FigureCB.SelectedIndex;
                    break;

                case 2:
                    configHandler.config.StyleSettings[16] = FigureCB.SelectedIndex;
                    break;
            }
        }

        private void FovScaleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            switch (activeLayer)
            {
                case 0:
                    configHandler.config.StyleSettings[7] = FovScaleSlider.Value.ToString();
                    break;

                case 1:
                    configHandler.config.StyleSettings[11] = FovScaleSlider.Value.ToString();
                    break;

                case 2:
                    configHandler.config.StyleSettings[15] = FovScaleSlider.Value.ToString();
                    break;
            }
        }

        private void FovThiknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            switch (activeLayer)
            {
                case 0:
                    configHandler.config.StyleSettings[6] = FovThiknessSlider.Value.ToString();
                    break;

                case 1:
                    configHandler.config.StyleSettings[10] = FovThiknessSlider.Value.ToString();
                    break;

                case 2:
                    configHandler.config.StyleSettings[14] = FovThiknessSlider.Value.ToString();
                    break;
            }
        }

        private void FovColorPicker_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (MainWindow.loadingCfg) return;

            switch (activeLayer)
            {
                case 0:
                    configHandler.config.StyleSettings[5] = e.NewValue;
                    break;

                case 1:
                    configHandler.config.StyleSettings[9] = e.NewValue;
                    break;

                case 2:
                    configHandler.config.StyleSettings[13] = e.NewValue;
                    break;
            }
        }

        #endregion

        #region OUTLINE

        private void OutThkinessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.StyleSettings[4] = (int)OutThiknessSlider.Value;
        }

        private void OutColorPicker_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.StyleSettings[3] = e.NewValue;
        }

        #endregion

        #region MESH

        private void MeshThkinessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.StyleSettings[2] = (int)MeshThiknessSlider.Value;
        }

        private void MeshColorPicker_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.StyleSettings[1] = e.NewValue;
        }

        #endregion

        #region BACKGROUND

        private void BackgroundColorPicker_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.StyleSettings[0] = e.NewValue;
        }


        #endregion

        #endregion

        #region RIGHT

        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.Zoom = ScaleSlider.Value;
        }

        private void HeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.Height = (int)HeightSlider.Value;

            if ((bool)SyncHaWToogle.IsChecked)
            {
                WidthSlider.Value = (int)HeightSlider.Value;
            }
        }

        private void WidthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.Width = (int)WidthSlider.Value;
        }

        private void SyncHaWToogle_Checked(object sender, RoutedEventArgs e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.SyncHaW = (bool)SyncHaWToogle.IsChecked;

            WidthSlider.IsEnabled = (bool)SyncHaWToogle.IsChecked ? false : true;
        }

        private void XSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.X = (int)XSlider.Value;
        }

        private void YSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.loadingCfg) return;

            configHandler.config.Y = (int)YSlider.Value;
        }

        #region POSITION PRESETS

        private void TopLeft_Click(object sender, RoutedEventArgs e)
        {
            XSlider.Value = 0;
            YSlider.Value = 0;
        }

        private void Top_Click(object sender, RoutedEventArgs e)
        {

            XSlider.Value = screenWidth / 2 - configHandler.config.Width / 2;
            YSlider.Value = 0;
        }

        private void TopRight_Click(object sender, RoutedEventArgs e)
        {
            XSlider.Value = screenWidth - configHandler.config.Width;
            YSlider.Value = 0;
        }

        private void MidLeft_Click(object sender, RoutedEventArgs e)
        {
            XSlider.Value = 0;
            YSlider.Value = screenHeight / 2 - configHandler.config.Height / 2;
        }

        private void Center_Click(object sender, RoutedEventArgs e)
        {

            XSlider.Value = screenWidth / 2 - configHandler.config.Width / 2;
            YSlider.Value = screenHeight / 2 - configHandler.config.Height / 2;
        }

        private void MidRight_Click(object sender, RoutedEventArgs e)
        {
            XSlider.Value = screenWidth - configHandler.config.Width;
            YSlider.Value = screenHeight / 2 - configHandler.config.Height / 2;
        }

        private void BottomLeft_Click(object sender, RoutedEventArgs e)
        {
            XSlider.Value = 0;
            YSlider.Value = screenHeight - configHandler.config.Height;
        }

        private void Bottom_Click(object sender, RoutedEventArgs e)
        {

            XSlider.Value = screenWidth / 2 - configHandler.config.Width / 2;
            YSlider.Value = screenHeight - configHandler.config.Height;
        }

        private void BottomRight_Click(object sender, RoutedEventArgs e)
        {
            XSlider.Value = screenWidth - configHandler.config.Width;
            YSlider.Value = screenHeight - configHandler.config.Height;
        }
        #endregion

        #endregion
    }
}
