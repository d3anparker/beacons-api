using Beacons.Data;
using Beacons.Mapping;
using FluentAssertions;

namespace Tests
{
    [TestFixture]
    public class BeaconMapperTests
    {
        public IBeaconMapper _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new BeaconMapper();
        }

        [Test]
        public void Mapper_maps_model_to_beacon_correctly()
        {
            var beacon = new Beacon()
            {
                Id = Guid.NewGuid(),
                Longitude = 50,
                Latitude = 50,
                Created = DateTime.Now,
                Expiry = DateTime.Now.AddMinutes(10)
            };

            var actual = _sut.MapToModel(beacon);

            actual.Id.Should().Be(beacon.Id);
            actual.Longitude.Should().Be(beacon.Longitude);
            actual.Latitude.Should().Be(beacon.Latitude);
        }
    }
}