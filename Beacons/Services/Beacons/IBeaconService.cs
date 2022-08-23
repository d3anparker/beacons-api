using Beacons.Data;
using Beacons.Models.Results;

namespace Beacons.Services.Beacons
{
    public interface IBeaconService
    {
        Task<Beacon?> GetBeaconByIdAsync(Guid id);
        Task<ServiceResult<Beacon>> CreateBeaconAsync(Beacon beacon);
    }
}