using GameOverlay.Drawing;
using System;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace X975.Radar.Utility
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public static class Additions
    {
        private const double angle = -45 * Math.PI / 180;

        public static Vector2 fromValues(float x, float y)
        {
            return new Vector2(y, x);
        }

        public static Vector2 fromFArray(float[] array)
        {
            if (array.Length == 2)
            {
                return new Vector2(array[1], array[0]);
            }
            else
            {
                return new Vector2();
            }
        }

        public static Vector2 Rotate(this Vector2 v)
        {
            return new Vector2(
                (float)(v.X * Math.Cos(angle) - v.Y * Math.Sin(angle)),
                (float)(v.X * Math.Sin(angle) + v.Y * Math.Cos(angle))
            );
        }

        public static float Magnitude(this Vector2 vector)
        {
            return (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        private static float CornerColorValue(float value, float minus)
        {
            if (value == 0 || value - minus < 0)
            {
                return 0;
            }
            else
            {
                return value - minus;
            }
        }

        public static void DrawIconDot(
        this Graphics graphics,
        IBrush cornerBrush,
        Image image,
        Vector2 position,
        float size)
        {
            graphics.DrawImage(image, Rectangle.Create(position.X - size / 2f, position.Y - size / 2f, size, size), 1, true);
            graphics.DrawEllipse(cornerBrush, position.X, position.Y, size / 2f, size / 2f, 1f);
        }

        public static void DrawHarvestableDot(
        this Graphics graphics,
        Image image,
        IBrush mainbrush,
        IBrush accentbrush,
        Vector2 position,
        float size)
        {
            graphics.FillEllipse(mainbrush, position.X, position.Y, size / 2f, size / 2f);
            graphics.DrawEllipse(accentbrush, position.X, position.Y, size / 2f, size / 2f, 1f);

            size -= 1.5f;
            graphics.DrawImage(image, Rectangle.Create(position.X - size / 2f, position.Y - size / 2f, size, size));
        }

        public static void DrawTextCentered(
        this Graphics graphics,
        Font font,
        IBrush brush,
        float x,
        float y,
        string text)
        {
            graphics.DrawText(font, brush, x - graphics.MeasureString(font, text).X / 2f, y, text);
        }

        public static void DrawDotWithStringIcon(
        this Graphics graphics,
        IBrush mainbrush,
        IBrush cornerbrush,
        IBrush textbrush,
        Vector2 pos,
        Font font,
        string text,
        float size)
        {
            graphics.FillEllipse(mainbrush, pos.X, pos.Y, size / 2f, size / 2f);
            graphics.DrawEllipse(cornerbrush, pos.X, pos.Y, size / 2f, size / 2f, 1f);
            graphics.DrawText(font, size - 2f, textbrush, pos.X - graphics.MeasureString(font, size - 2f, text).X / 2f, pos.Y - graphics.MeasureString(font, size - 2f, text).Y / 2f, text);
        }

        public static void PlayerDot(
        this Graphics graphics,
        SolidBrush mainbrush,
        IBrush textbrush,
        Vector2 pos,
        Font font,
        string text,
        float size)
        {
            Color tempColor = mainbrush.Color;

            graphics.FillEllipse(mainbrush, pos.X, pos.Y, size / 2f, size / 2f);
            mainbrush.Color = new Color(CornerColorValue(mainbrush.Color.R, 0.25f), CornerColorValue(mainbrush.Color.G, 0.25f), CornerColorValue(mainbrush.Color.B, 0.25f));
            graphics.DrawEllipse(mainbrush, pos.X, pos.Y, size / 2f, size / 2f, 1f);
            mainbrush.Color = tempColor;

            if (text.Length != 0)
                graphics.DrawText(font, size - 2.5f, textbrush, pos.X - graphics.MeasureString(font, size - 2.5f, text).X / 2f, pos.Y - graphics.MeasureString(font, size - 2.5f, text).Y / 2f, text);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117
        }

        public static double GetWindowsScreenScalingFactor()
        {
            System.Drawing.Graphics GraphicsObject = System.Drawing.Graphics.FromHwnd(IntPtr.Zero);
            IntPtr DeviceContextHandle = GraphicsObject.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(DeviceContextHandle, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(DeviceContextHandle, (int)DeviceCap.DESKTOPVERTRES);
            double ScreenScalingFactor = Math.Round(PhysicalScreenHeight / (double)LogicalScreenHeight, 2);
            GraphicsObject.ReleaseHdc(DeviceContextHandle);
            GraphicsObject.Dispose();
            return ScreenScalingFactor;
        }

        public static System.Drawing.Size GetDisplayResolution()
        {
            var sf = GetWindowsScreenScalingFactor();
            var screenWidth = Screen.PrimaryScreen.Bounds.Width * sf;
            var screenHeight = Screen.PrimaryScreen.Bounds.Height * sf;
            return new System.Drawing.Size((int)screenWidth, (int)screenHeight);
        }
    }
}
