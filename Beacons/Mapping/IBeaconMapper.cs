using Beacons.Data;
using Beacons.Models;

namespace Beacons.Mapping
{
    public interface IBeaconMapper
    {
        Beacon MapToEntity(BeaconModel model);
        BeaconModel MapToModel(Beacon beacon);
    }
}