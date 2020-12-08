using System.Collections.Generic;
using System.Threading.Tasks;
using CodingTestApi.Adapters;
using CodingTestApi.Models.Spotify;
using CodingTestApi.Services;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace CodingTestApiTests
{
    public class SpotifySearchServiceTests
    {
        private ArtistMatchingService _sut; // System Under Test
        private ISpotifySearchAdapter _fakeSpotifySearchAdapter = A.Fake<ISpotifySearchAdapter>();

        private readonly ArtistsSearchResponse _validArtistsResponse = new ArtistsSearchResponse
        {
            Artists = new Artists
            {
                Items = new List<Item>()
                {
                    new Item
                    {
                        Id = "5He6InD3axXIVS3SCQPFp6",
                        Name = "Perry Katy"
                    },
                    new Item
                    {
                        Id = "123",
                        Name = "Katy Perry"
                    },
                    new Item
                    {
                        Id = "6jsDtDfNjHeIrNtx5sx4b1",
                        Name = "Made famous by Katy Perry"
                    }
                }
            }
        };

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _sut = new ArtistMatchingService(_fakeSpotifySearchAdapter);
        }

        [TestCase("Katy Perry", "Katy Perry")]
        [TestCase("Katyperry", "Katy Perry")]
        [TestCase("katyPERRy", "Katy Perry")]
        [TestCase("Katy.Perry", "Katy Perry")]
        public async Task MatchArtist_WithValidQueries_ActualArtistNameEqualsExpected(string query, string expected)
        {
            // Arrange
            A.CallTo(() => _fakeSpotifySearchAdapter.GetArtistsAsync(A<string>._))
                .Returns(Task.FromResult(_validArtistsResponse));

            // Act
            var actual = (await _sut.GetSingleMatchingArtistAsync(query)).Name;

            // Assert
            actual.ShouldBe(expected);
        }

        [TestCase("The katy perry")]
        [TestCase("Katy perry 2")]
        public void MatchArtist_WithInvalidQueries_ThrowsNoMatchingArtistException(string query)
        {
            // TODO implement exception and write test
            Assert.Fail();
        }
    }
}