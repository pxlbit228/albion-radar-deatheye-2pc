using System;
using System.Windows.Forms;

namespace X975.Tools
{
    public static class Diagnostics
    {
        public static T DoVital<T>(Func<T> func, string message) where T : class
        {
            try
            {
                return func();
            }
            catch (Exception e)
            {
                ReportFatal(message, e);
                return null;
            }
        }
        
        public static void ReportFatal(string message, Exception e = null)
        {
            MessageBox.Show(message + (e != null ? "\n\n" + e : ""), "Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(1);
        }
    }
}