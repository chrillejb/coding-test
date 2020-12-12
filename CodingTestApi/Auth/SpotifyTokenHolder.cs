using System;

namespace CodingTestApi.Auth
{
    /// <summary>
    /// Holds a Spotify bearer token during the lifetime of the application.
    /// </summary>
    public class SpotifyTokenHolder
    {
        /// <summary>
        /// The current, active token.
        /// </summary>
        public string CurrentToken { get; set; }

        /// <summary>
        /// The point in time when the last token was fetched
        /// </summary>
        public DateTime LastFetched { get; set; }
        
        /// <summary>
        /// A token's expiration time expressed in number of seconds.
        /// </summary>
        /// <remarks>Default value: 10 min (600 sec).</remarks>
        public int ExpirationInSeconds { get; set; } = 600;

        /// <summary>
        /// Checks wheather or not a new token needs to be fetched.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if token has expired,
        /// <see langword="false"/> if the current token can still be used.
        /// </returns>
        public bool NeedsRefresh() =>
            DateTime.Now.AddSeconds(-10) > LastFetched.AddSeconds(ExpirationInSeconds);
    }
}