namespace Beacons.Services.Dates
{
    public class DefaultDateTime : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
