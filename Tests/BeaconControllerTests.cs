using FluentAssertions;
using System.Net;

namespace Tests
{
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
    }
}
