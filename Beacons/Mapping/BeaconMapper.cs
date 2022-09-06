using Beacons.Data;
using Beacons.Models;

namespace Beacons.Mapping
{
    public class BeaconMapper : IBeaconMapper
    {
        public BeaconModel MapToModel(Beacon beacon)
        {
            var model = new BeaconModel()
            {
                Id = beacon.Id,
                Longitude = beacon.Longitude,
                Latitude = beacon.Latitude,
                Expiry = beacon.Expiry
            };

            return model;
        }

        public Beacon MapToEntity(BeaconModel model)
        {
            var beacon = new Beacon()
            {
                Longitude = model.Longitude,
                Latitude = model.Latitude
            };

            return beacon;
        }
    }
}