namespace Beacons.Models
{
    public class BeaconModel
    {
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Expiry { get; set; }
    }
}
