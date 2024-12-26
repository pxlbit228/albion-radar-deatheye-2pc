using GameOverlay.Drawing;
using System.Threading.Tasks;
using X975.Radar.GameObjects.LocalPlayer;
using X975.Radar.Utility;
using X975.Radar.OverlaySettings;
using System;
using X975.Radar.Drawing.Overlays;

namespace X975.Radar.Drawers
{
    public class InfoDrawerer : IDrawerer
    {
        private readonly InfoOverlay overlay;
        private readonly Graphics gfx;
        private readonly InfoOverlayBrushesDictionary brushesDictionary;

        private readonly LocalPlayerHandler localPlayerHandler;
        private float globalOffset = 0;

        public InfoDrawerer(InfoOverlay overlay, InfoOverlayBrushesDictionary brushesDictionary, LocalPlayerHandler localPlayerHandler)
        {
            this.overlay = overlay;
            this.gfx = overlay.Graphics;

            this.brushesDictionary = brushesDictionary;
            this.localPlayerHandler = localPlayerHandler;
        }

        public async Task DrawAsync()
        {
            switch (localPlayerHandler.localPlayer.CurrentCluster.Subtype)
            {
                case ClusterSubtype.Mist:
                    await DrawClusterHUD($"Mist ID : {localPlayerHandler.localPlayer.CurrentCluster.LobbyID}", 0);
                    await DrawClusterTimeCycle();
                    await DrawClusterObjectives();
                    break;

                case ClusterSubtype.Abbey:
                    await DrawClusterHUD($"ABBEY ID : {localPlayerHandler.localPlayer.CurrentCluster.LobbyID}", 1);
                    await DrawClusterObjectives();
                    break;
            }
        }

        private async Task DrawClusterHUD(string text, int i)
        {
            float length = gfx.MeasureString(brushesDictionary._fonts["Main"], text).X + 33;

            gfx.FillRoundedRectangle(brushesDictionary._designColors["Background"], 1, 1, length, 25, 5);
            gfx.DrawRoundedRectangle(brushesDictionary._designColors["Corner"], 1, 1, length, 25, 5, 2);

            switch (i)
            {
                case 0:
                    gfx.DrawImage(brushesDictionary._mistImages[i], 5, 5, 22, 20);
                    break;

                case 1:
                    gfx.DrawImage(brushesDictionary._mistImages[i], 5, 5, 27, 20);
                    break;
            }
            
            gfx.DrawText(brushesDictionary._fonts["Main"], brushesDictionary._designColors["White"], 26, 3, text);
        }

        private async Task DrawClusterObjectives()
        {
            if (localPlayerHandler.localPlayer.CurrentCluster.ClusterObjectives == null)
                return;

            foreach (ClusterObjective cO in localPlayerHandler.localPlayer.CurrentCluster.ClusterObjectives.Values)
            {
                int timerValue = (int)cO.Timer.Subtract(DateTime.UtcNow).TotalSeconds;

                if (timerValue > 0)
                {
                    if (!brushesDictionary._mistImages.ContainsKey(cO.Charge))
                        continue;

                    string text = $"{timerValue} sec";

                    float length = gfx.MeasureString(brushesDictionary._fonts["Main"], text).X + 36;

                    gfx.FillRoundedRectangle(brushesDictionary._designColors["Background"], globalOffset, 30, length + globalOffset, 55, 5);
                    gfx.DrawRoundedRectangle(brushesDictionary._designColors["Corner"], globalOffset, 30, length + globalOffset, 55, 5, 2);

                    if (cO.Type == "CHEST")
                    {
                        gfx.DrawImage(brushesDictionary._mistImages[cO.Charge], globalOffset + 4, 32, globalOffset + 26, 52);
                    }
                    else
                    {
                        gfx.DrawImage(brushesDictionary._mistImages[cO.Charge], globalOffset + 2, 29, globalOffset + 26, 54);
                    }

                    gfx.DrawText(brushesDictionary._fonts["Main"], brushesDictionary._designColors["White"], globalOffset + 28, 33, text);

                    globalOffset += length + 5;
                }
            }
        }

        private async Task DrawClusterTimeCycle()
        {
            TimeSpan timeSpan = localPlayerHandler.localPlayer.CurrentCluster.TimeCycle.Subtract(DateTime.UtcNow);

            if (timeSpan.TotalSeconds > 0)
            {
                string text = timeSpan.Minutes > 0 ? $"{timeSpan.Minutes}m {timeSpan.Seconds}s" : $"{timeSpan.Seconds}s";

                globalOffset = gfx.MeasureString(brushesDictionary._fonts["Main"], text).X + 36;

                gfx.FillRoundedRectangle(brushesDictionary._designColors["Background"], 1, 30, globalOffset, 55, 5);
                gfx.DrawRoundedRectangle(brushesDictionary._designColors["Corner"], 1, 30, globalOffset, 55, 5, 2);
                gfx.DrawImage(brushesDictionary._mistImages[226], 0 + 4, 32, 0 + 26, 52);

                gfx.DrawText(brushesDictionary._fonts["Main"], brushesDictionary._designColors["White"], 0 + 28, 33, text);
            }

            globalOffset += 5;
        }
    }
}
