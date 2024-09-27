namespace HydraAPI.Shared
{
    public class ResponseDTO<T>
    {
        public int status { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
