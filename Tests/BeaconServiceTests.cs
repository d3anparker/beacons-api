using Beacons.Data;
using Beacons.Options;
using Beacons.Services.Beacons;
using Beacons.Services.Dates;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Tests
{
    [TestFixture]
    public class BeaconServiceTests
    {
        private IBeaconService _sut;
        private Context _context;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("UnitTest");

            var id = Guid.Parse("9b338478-b739-4f2a-8c7d-b99038600b81");

            _context = new Context(options.Options);

            _context.Database.EnsureCreated();
            _context.Beacons.Add(new Beacon() { Id = id });
            _context.SaveChanges();

            var dateTime = Substitute.For<IDateTime>();
            dateTime.UtcNow.Returns(DateTime.Now);

            _sut = new BeaconService(_context, new BeaconOptions() { ExpiryInMinutes = 10 }, dateTime);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task Get_missing_beacon_returns_null()
        {
            var beacon = await _sut.GetBeaconByIdAsync(Guid.NewGuid());

            beacon.Should().BeNull();
        }

        [Test]
        public async Task Get_valid_beacon_returns_beacon()
        {
            Guid id = Guid.Parse("9b338478-b739-4f2a-8c7d-b99038600b81");
            var beacon = await _sut.GetBeaconByIdAsync(id);

            beacon.Should().NotBeNull();
        }

        [Test]
        public async Task Create_beacon_returns_success()
        {
            var beacon = new Beacon()
            {
                Id = Guid.NewGuid(),
                Latitude = 50,
                Longitude = 50
            };

            var result = await _sut.CreateBeaconAsync(beacon);

            result.Success.Should().BeTrue();
        }

        [Test]
        public async Task Create_beacon_returns_valid_beacon()
        {
            var beacon = new Beacon()
            {
                Id = Guid.NewGuid(),
                Latitude = 50,
                Longitude = 50
            };

            var result = await _sut.CreateBeaconAsync(beacon);

            result.Data.Should().Be(beacon);
        }

        [Test]
        public async Task Created_beacon_expiry_should_be_set_distance_from_creation()
        {
            var beacon = new Beacon()
            {
                Id = Guid.NewGuid(),
                Latitude = 50,
                Longitude = 50
            };

            var result = await _sut.CreateBeaconAsync(beacon);

            result.Data.Expiry.Should().Be(result.Data.Created.AddMinutes(10));
        }

        [Test]
        public async Task Created_beacon_ttl_should_be_set_distance_from_creation()
        {
            var beacon = new Beacon()
            {
                Id = Guid.NewGuid(),
                Latitude = 50,
                Longitude = 50
            };

            var result = await _sut.CreateBeaconAsync(beacon);

            result.Data.TimeToLive.Should().Be(600);
        }
    }
}