namespace Globogames.Common
{
    using System;

    public static class Utils
    {
        public static decimal Converter(string s)
        {
            s = s.Replace(".", ","); // for a simplification
            return decimal.Parse(s);
        }

        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search, StringComparison.Ordinal);
            if (pos < 0)
            {
                return text;
            }

            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        public static string ReplaceAt(this string text, string search, string replace, int index)
        {
            return text.Substring(0, index) + replace + text.Substring(index + search.Length);
        }
    }
}