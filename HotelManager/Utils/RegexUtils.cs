
using System.Text.RegularExpressions;

namespace HotelManager.Utils
{
    public static class RegexUtils
    {
        public static bool IsMatch(string text, string pattern)
        {
            return Regex.IsMatch(text, pattern);
        }
    }
}
