using Beacons.Data;
using Beacons.Models;
using Microsoft.EntityFrameworkCore;

namespace Beacons.Services.Beacons
{
    public class BeaconService : IBeaconService
    {
        private readonly Context _context;

        public BeaconService(Context context)
        {
            _context = context;
        }

        public async Task<Beacon> CreateBeaconAsync(Beacon beacon)
        {
            _context.Beacons.Add(beacon);
            await _context.SaveChangesAsync();

            return beacon;
        }

        public async Task<Beacon?> GetBeaconByIdAsync(Guid id) => await _context.Beacons.Where(x => x.Id == id).FirstOrDefaultAsync();
    }
}
