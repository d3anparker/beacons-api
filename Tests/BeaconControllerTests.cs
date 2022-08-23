using Beacons.Models.Requests;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace Tests
{
    [TestFixture]
    public class BeaconControllerTests
    {
        private BeaconWebAppFactory _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void SetUp()
        {
            _factory = new BeaconWebAppFactory();
            _client = _factory.CreateClient();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Test]
        public async Task Get_beacon_returns_not_found_for_missing_beacon()
        {
            var response = await _client.GetAsync($"api/beacon/{Guid.NewGuid()}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task Get_beacon_returns_ok_for_retrieving_valid_beacon()
        {
            var response = await _client.GetAsync("api/beacon/9b338478-b739-4f2a-8c7d-b99038600b81");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Create_beacon_returns_created()
        {
            var request = new BeaconCreateRequest()
            {
                Latitude = 50,
                Longitude = 50
            };

            var content = JsonContent.Create(request);            
            var response = await _client.PostAsync("api/beacon", content);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}
