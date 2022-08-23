using Beacons.Data;
using Beacons.Models.Results;
using Beacons.Options;
using Beacons.Services.Dates;
using Microsoft.EntityFrameworkCore;

namespace Beacons.Services.Beacons
{
    public class BeaconService : IBeaconService
    {
        private readonly Context _context;
        private readonly BeaconOptions _beaconOptions;
        private readonly IDateTime _dateTime;

        public BeaconService(Context context, BeaconOptions beaconOptions, IDateTime dateTime)
        {
            _context = context;
            _beaconOptions = beaconOptions;
            _dateTime = dateTime;
        }

        public async Task<ServiceResult<Beacon>> CreateBeaconAsync(Beacon beacon)
        {
            var result = new ServiceResult<Beacon>(beacon);

            beacon.Created = _dateTime.Now;
            beacon.Expiry = beacon.Created.AddMinutes(_beaconOptions.ExpiryInMinutes);

            _context.Beacons.Add(beacon);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<Beacon?> GetBeaconByIdAsync(Guid id) => await _context.Beacons.Where(x => x.Id == id).FirstOrDefaultAsync();
    }
}
