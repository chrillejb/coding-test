using System.Threading.Tasks;

public class SpotifySearch
{
    private readonly SpotifyTokenFetcher _spotifyTokenFetcher;

    public SpotifySearch(SpotifyTokenFetcher spotifyTokenFetcher)
    {
        _spotifyTokenFetcher = spotifyTokenFetcher;
    }

    public async Task<Artist> GetSingleMatchingArtistAsync(string queryArtistName)
    {
        var token = await _spotifyTokenFetcher.FetchTokenAsync();
        // TODO logic for getting from Spotify API (url from config?)
        // TODO matching with search argument
        // TODO return artist name as fetched from Spotify search

        // TODO add case for empty "items" from API response => 404
        return new Artist
        {
            ArtistId = "123",
            ArtistName = "hello artist"
        };
    }
}