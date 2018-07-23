using System.Text.RegularExpressions;

namespace Api.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Remove special characters from a string
        /// <![CDATA[https://stackoverflow.com/a/1120248/1752882]]>
        /// </summary>
        /// <param name="string"></param>
        /// <returns>Clean string</returns>
        public static string RemoveSpecialCharacters(this string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }
    }
}