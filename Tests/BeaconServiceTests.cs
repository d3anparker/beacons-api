using Beacons.Data;
using Beacons.Services.Beacons;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class Tests
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

            _sut = new BeaconService(_context);
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
    }
}