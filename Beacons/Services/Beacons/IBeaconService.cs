using Beacons.Models;

namespace Beacons.Services.Beacons
{
    public interface IBeaconService
    {
        Task<Beacon?> GetBeaconByIdAsync(Guid id);
        Task<Beacon> CreateBeaconAsync(Beacon beacon);
    }
}
