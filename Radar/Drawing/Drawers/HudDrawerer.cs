using X975.Settings;
using X975.Radar.Utility;
using GameOverlay.Drawing;
using System;
using System.Text;
using System.Threading.Tasks;
using X975.Radar.Drawing.OverlaySettings;
using X975.Radar.GameObjects.LocalPlayer;

namespace X975.Radar.Drawers
{
    public class HudDrawerer : IDrawerer
    {
        private readonly ConfigHandler configHandler;
        private readonly RadarOverlayBrushesDictionary brushesDictionary;
        private readonly Graphics gfx;
        private readonly LocalPlayerHandler localPlayerHandler;

        public HudDrawerer(Graphics gfx, RadarOverlayBrushesDictionary brushesDictionary, LocalPlayerHandler localPlayerHandler)
        {
            this.configHandler = ConfigHandler.Source;
            this.brushesDictionary = brushesDictionary;
            this.localPlayerHandler = localPlayerHandler;
            this.gfx = gfx;
        }

        public async Task DrawAsync()
        {
            #region BG

            gfx.FillRectangle(brushesDictionary._brushes["Background"], 0, 0, configHandler.config.Width, configHandler.config.Height);

            #endregion

            #region MESH

            int horyzontalCount = configHandler.config.Width / 10;
            int verticalCount = configHandler.config.Height / 10;

            for (int x = 0; x < configHandler.config.Width / horyzontalCount; x++)
            {
                gfx.DrawLine(
                    brushesDictionary._brushes["Grid"],
                    horyzontalCount * x,
                    0,
                    horyzontalCount * x,
                    configHandler.config.Height,
                    Convert.ToInt32(configHandler.config.StyleSettings[2]));
            }

            for (int y = 0; y < configHandler.config.Height / verticalCount; y++)
            {
                gfx.DrawLine(
                    brushesDictionary._brushes["Grid"],
                    0,
                    horyzontalCount * y,
                    configHandler.config.Width,
                    horyzontalCount * y,
                    Convert.ToInt32(configHandler.config.StyleSettings[2]));
            }

            #endregion

            #region OUTLINE

            gfx.DrawRectangle(brushesDictionary._brushes["Outline"], 0, 0, configHandler.config.Width, configHandler.config.Height, Convert.ToInt32(configHandler.config.StyleSettings[4]));

            #endregion

            #region FOV

            switch (Convert.ToInt32(configHandler.config.StyleSettings[8]))
            {
                case 0:
                    gfx.DrawEllipse(brushesDictionary._brushes["FovFirst"],
                        configHandler.config.Width / 2,
                        configHandler.config.Height / 2,
                        configHandler.config.Width * Convert.ToSingle(configHandler.config.StyleSettings[7]) / 100 / 2 - Convert.ToInt32(configHandler.config.StyleSettings[6]) / 2,
                        configHandler.config.Height * Convert.ToSingle(configHandler.config.StyleSettings[7]) / 100 / 2 - Convert.ToInt32(configHandler.config.StyleSettings[6]) / 2,
                        Convert.ToInt32(configHandler.config.StyleSettings[6]));
                    break;

                case 1:
                    gfx.DrawRectangle(brushesDictionary._brushes["FovFirst"],
                        configHandler.config.Width / 2 - configHandler.config.Width * Convert.ToSingle(configHandler.config.StyleSettings[7]) / 100 / 2 + Convert.ToInt32(configHandler.config.StyleSettings[6]) / 2,
                        configHandler.config.Height / 2 - configHandler.config.Height * Convert.ToSingle(configHandler.config.StyleSettings[7]) / 100 / 2 + Convert.ToInt32(configHandler.config.StyleSettings[6]) / 2,
                        configHandler.config.Width / 2 + configHandler.config.Width * Convert.ToSingle(configHandler.config.StyleSettings[7]) / 100 / 2 - Convert.ToInt32(configHandler.config.StyleSettings[6]) / 2,
                        configHandler.config.Height / 2 + configHandler.config.Height * Convert.ToSingle(configHandler.config.StyleSettings[7]) / 100 / 2 - Convert.ToInt32(configHandler.config.StyleSettings[6]) / 2,
                        Convert.ToInt32(configHandler.config.StyleSettings[6]));
                    break;

                case 2:
                    gfx.FillEllipse(brushesDictionary._brushes["FovFirst"],
                        configHandler.config.Width / 2,
                        configHandler.config.Height / 2,
                        configHandler.config.Width * Convert.ToSingle(configHandler.config.StyleSettings[7]) / 100 / 2,
                        configHandler.config.Height * Convert.ToSingle(configHandler.config.StyleSettings[7]) / 100 / 2);
                    break;

                case 3:
                    gfx.FillRectangle(brushesDictionary._brushes["FovFirst"],
                        configHandler.config.Width / 2 - configHandler.config.Width * Convert.ToSingle(configHandler.config.StyleSettings[7]) / 100 / 2,
                        configHandler.config.Height / 2 - configHandler.config.Height * Convert.ToSingle(configHandler.config.StyleSettings[7]) / 100 / 2,
                        configHandler.config.Width / 2 + configHandler.config.Width * Convert.ToSingle(configHandler.config.StyleSettings[7]) / 100 / 2,
                        configHandler.config.Height / 2 + configHandler.config.Height * Convert.ToSingle(configHandler.config.StyleSettings[7]) / 100 / 2);
                    break;
            }

            switch (Convert.ToInt32(configHandler.config.StyleSettings[12]))
            {
                case 0:
                    gfx.DrawEllipse(brushesDictionary._brushes["FovSecond"],
                        configHandler.config.Width / 2,
                        configHandler.config.Height / 2,
                        configHandler.config.Width * Convert.ToSingle(configHandler.config.StyleSettings[11]) / 100 / 2 - Convert.ToInt32(configHandler.config.StyleSettings[10]) / 2,
                        configHandler.config.Height * Convert.ToSingle(configHandler.config.StyleSettings[11]) / 100 / 2 - Convert.ToInt32(configHandler.config.StyleSettings[10]) / 2,
                        Convert.ToInt32(configHandler.config.StyleSettings[10]));
                    break;

                case 1:
                    gfx.DrawRectangle(brushesDictionary._brushes["FovSecond"],
                        configHandler.config.Width / 2 - configHandler.config.Width * Convert.ToSingle(configHandler.config.StyleSettings[11]) / 100 / 2 + Convert.ToInt32(configHandler.config.StyleSettings[10]) / 2,
                        configHandler.config.Height / 2 - configHandler.config.Height * Convert.ToSingle(configHandler.config.StyleSettings[11]) / 100 / 2 + Convert.ToInt32(configHandler.config.StyleSettings[10]) / 2,
                        configHandler.config.Width / 2 + configHandler.config.Width * Convert.ToSingle(configHandler.config.StyleSettings[11]) / 100 / 2 - Convert.ToInt32(configHandler.config.StyleSettings[10]) / 2,
                        configHandler.config.Height / 2 + configHandler.config.Height * Convert.ToSingle(configHandler.config.StyleSettings[11]) / 100 / 2 - Convert.ToInt32(configHandler.config.StyleSettings[10]) / 2,
                        Convert.ToInt32(configHandler.config.StyleSettings[10]));
                    break;

                case 2:
                    gfx.FillEllipse(brushesDictionary._brushes["FovSecond"],
                        configHandler.config.Width / 2,
                        configHandler.config.Height / 2,
                        configHandler.config.Width * Convert.ToSingle(configHandler.config.StyleSettings[11]) / 100 / 2,
                        configHandler.config.Height * Convert.ToSingle(configHandler.config.StyleSettings[11]) / 100 / 2);
                    break;

                case 3:
                    gfx.FillRectangle(brushesDictionary._brushes["FovSecond"],
                        configHandler.config.Width / 2 - configHandler.config.Width * Convert.ToSingle(configHandler.config.StyleSettings[11]) / 100 / 2,
                        configHandler.config.Height / 2 - configHandler.config.Height * Convert.ToSingle(configHandler.config.StyleSettings[11]) / 100 / 2,
                        configHandler.config.Width / 2 + configHandler.config.Width * Convert.ToSingle(configHandler.config.StyleSettings[11]) / 100 / 2,
                        configHandler.config.Height / 2 + configHandler.config.Height * Convert.ToSingle(configHandler.config.StyleSettings[11]) / 100 / 2);
                    break;
            }

            switch (Convert.ToInt32(configHandler.config.StyleSettings[16]))
            {
                case 0:
                    gfx.DrawEllipse(brushesDictionary._brushes["FovThird"],
                        configHandler.config.Width / 2,
                        configHandler.config.Height / 2,
                        configHandler.config.Width * Convert.ToSingle(configHandler.config.StyleSettings[15]) / 140 / 2 - Convert.ToInt32(configHandler.config.StyleSettings[14]) / 2,
                        configHandler.config.Height * Convert.ToSingle(configHandler.config.StyleSettings[15]) / 140 / 2 - Convert.ToInt32(configHandler.config.StyleSettings[14]) / 2,
                        Convert.ToInt32(configHandler.config.StyleSettings[14]));
                    break;

                case 1:
                    gfx.DrawRectangle(brushesDictionary._brushes["FovThird"],
                        configHandler.config.Width / 2 - configHandler.config.Width * Convert.ToSingle(configHandler.config.StyleSettings[15]) / 140 / 2 + Convert.ToInt32(configHandler.config.StyleSettings[14]) / 2,
                        configHandler.config.Height / 2 - configHandler.config.Height * Convert.ToSingle(configHandler.config.StyleSettings[15]) / 140 / 2 + Convert.ToInt32(configHandler.config.StyleSettings[14]) / 2,
                        configHandler.config.Width / 2 + configHandler.config.Width * Convert.ToSingle(configHandler.config.StyleSettings[15]) / 140 / 2 - Convert.ToInt32(configHandler.config.StyleSettings[14]) / 2,
                        configHandler.config.Height / 2 + configHandler.config.Height * Convert.ToSingle(configHandler.config.StyleSettings[15]) / 140 / 2 - Convert.ToInt32(configHandler.config.StyleSettings[14]) / 2,
                        Convert.ToInt32(configHandler.config.StyleSettings[14]));
                    break;

                case 2:
                    gfx.FillEllipse(brushesDictionary._brushes["FovThird"],
                        configHandler.config.Width / 2,
                        configHandler.config.Height / 2,
                        configHandler.config.Width * Convert.ToSingle(configHandler.config.StyleSettings[15]) / 140 / 2,
                        configHandler.config.Height * Convert.ToSingle(configHandler.config.StyleSettings[15]) / 140 / 2);
                    break;

                case 3:
                    gfx.FillRectangle(brushesDictionary._brushes["FovThird"],
                        configHandler.config.Width / 2 - configHandler.config.Width * Convert.ToSingle(configHandler.config.StyleSettings[15]) / 140 / 2,
                        configHandler.config.Height / 2 - configHandler.config.Height * Convert.ToSingle(configHandler.config.StyleSettings[15]) / 140 / 2,
                        configHandler.config.Width / 2 + configHandler.config.Width * Convert.ToSingle(configHandler.config.StyleSettings[15]) / 140 / 2,
                        configHandler.config.Height / 2 + configHandler.config.Height * Convert.ToSingle(configHandler.config.StyleSettings[15]) / 140 / 2);
                    break;
            }

            #endregion

            #region CENTER

            switch (Convert.ToInt32(configHandler.config.StyleSettings[19]))
            {
                case 0:
                    gfx.DrawLine(brushesDictionary._brushes["Centering"],
                        configHandler.config.Width / 2,
                        0,
                        configHandler.config.Width / 2,
                        configHandler.config.Height,
                        Convert.ToInt32(configHandler.config.StyleSettings[18]));

                    gfx.DrawLine(brushesDictionary._brushes["Centering"],
                        0,
                        configHandler.config.Height / 2,
                        configHandler.config.Width,
                        configHandler.config.Height / 2,
                        Convert.ToInt32(configHandler.config.StyleSettings[18]));
                    break;

                case 1:
                    gfx.DrawEllipse(brushesDictionary._brushes["Centering"],
                        configHandler.config.Width / 2,
                        configHandler.config.Height / 2,
                        configHandler.config.Width / 2 - (Convert.ToInt32(configHandler.config.StyleSettings[18]) / 2),
                        configHandler.config.Height / 2 - (Convert.ToInt32(configHandler.config.StyleSettings[18]) / 2),
                        Convert.ToInt32(configHandler.config.StyleSettings[18]));

                    gfx.DrawEllipse(brushesDictionary._brushes["Centering"],
                        configHandler.config.Width / 2,
                        configHandler.config.Height / 2,
                        configHandler.config.Width / 2 * 0.8f - (Convert.ToInt32(configHandler.config.StyleSettings[18]) / 2),
                        configHandler.config.Height / 2 * 0.8f - (Convert.ToInt32(configHandler.config.StyleSettings[18]) / 2),
                        Convert.ToInt32(configHandler.config.StyleSettings[18]));

                    gfx.DrawEllipse(brushesDictionary._brushes["Centering"],
                        configHandler.config.Width / 2,
                        configHandler.config.Height / 2,
                        configHandler.config.Width / 2 * 0.6f - (Convert.ToInt32(configHandler.config.StyleSettings[18]) / 2),
                        configHandler.config.Height / 2 * 0.6f - (Convert.ToInt32(configHandler.config.StyleSettings[18]) / 2),
                        Convert.ToInt32(configHandler.config.StyleSettings[18]));
                    break;
            }

            #endregion

            var clusterText = GetClusterInfo();
            gfx.DrawText(brushesDictionary._fonts["Main"], 18, brushesDictionary._brushes["White"], 4, 4, clusterText);
        }

        private string GetClusterInfo()
        {
            var chests = localPlayerHandler.localPlayer?.Chests;
            var dynamicClusterData = localPlayerHandler.localPlayer?.DynamicClusterData;

            var builder = new StringBuilder();
            builder.Append(localPlayerHandler.localPlayer?.CurrentCluster?.DisplayName ?? "Unknown");
            
            if (!string.IsNullOrWhiteSpace(localPlayerHandler.localPlayer?.CurrentCluster?.LobbyID))
            {
                builder.Append("\n");
                builder.Append(localPlayerHandler.localPlayer?.CurrentCluster?.LobbyID);
            }
            
            if (dynamicClusterData != null)
            {
                builder.Append("\n");
                builder.Append(dynamicClusterData.Level);
                builder.Append("/");
                builder.Append(dynamicClusterData.HasNextFloor ? "?" : dynamicClusterData.Level.ToString());

                chests?.ForEach(c => builder.Append("\n" + c));
            }

            return builder.ToString();
        }
    }
}
