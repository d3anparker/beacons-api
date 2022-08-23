namespace Beacons.Models.Results
{
    public class ServiceResult
    {
        public bool Success =>  !Errors.Any();
        public IReadOnlyList<string> Errors { get; set; } = Array.Empty<string>();
    }

    public class ServiceResult<T> : ServiceResult
    {
        public ServiceResult(T data)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
