namespace Beacons.Services.Dates
{
    public class DefaultDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
