namespace Beacons.Data
{
    public class Beacon
    {
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expiry { get; set; }
        public int TimeToLive { get; set; }
    }
}
