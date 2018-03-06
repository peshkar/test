namespace Common
{
    public static class Constants
    {
        /// <summary>
        /// The token for regex(signed decimal value).
        /// </summary>
        public static string Token => "-?[\\d]{1,30}([.,][\\d]{1,30})?";
    }
}