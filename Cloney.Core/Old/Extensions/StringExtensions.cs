namespace Cloney.Core.Old.Extensions
{
    public static class StringExtensions
    {
        public static string AdjustPathSlashes(this string path)
        {
            return path.Replace("/", "\\");
        }
    }
}
