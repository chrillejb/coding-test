using System;
using System.Linq;

namespace CodingTestApi.Extensions
{
    public static class CustomStringExtensions
    {
        /// <summary>
        /// Converts the <see langword="string"/> <see paramref="str"/> to a string
        /// without whitespaces and punctuation characters.
        /// The resulting string will also be all lower case.
        /// </summary>
        /// <param name="str">The string to convert</param>
        /// <returns>A new <see langword="string"/> without whitespaces and punctuation characters in all lower case.</returns>
        public static string ToArtistString(this string str) =>
            new string(str.ToCharArray().Where(c => !Char.IsPunctuation(c) && !Char.IsWhiteSpace(c)).ToArray())
                .ToLowerInvariant();
    }
}