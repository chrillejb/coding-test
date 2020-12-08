using System;
using System.Collections.Generic;

namespace CodingTestApi.Models.Spotify
{
    public class ArtistsSearchResponse
    {
        public Artists Artists { get; set; }
    }

    public class Artists
    {
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}