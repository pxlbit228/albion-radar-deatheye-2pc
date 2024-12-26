using System;
using System.Linq;

namespace X975.Tools
{
    public static class Utility
    {
        private static Random random = new Random();

        public static string RandomString()
        {
            return new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", random.Next(5, 25)).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
