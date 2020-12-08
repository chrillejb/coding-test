using System;

namespace CodingTestApi.Exceptions
{
    public class NoMatchingArtistException : Exception
    {
        public NoMatchingArtistException()
        {
        }

        public NoMatchingArtistException(string message)
            : base(message)
        {
        }

        public NoMatchingArtistException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}