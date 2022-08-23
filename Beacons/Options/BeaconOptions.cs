using System.ComponentModel.DataAnnotations;

namespace Beacons.Options
{
    public class BeaconOptions
    {
        [Range(0, 1440)] // 1440 = 24 * 60
        public int ExpiryInMinutes { get; set; }
    }
}
