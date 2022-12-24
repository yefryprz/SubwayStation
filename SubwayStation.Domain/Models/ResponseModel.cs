namespace SubwayStation.Domain.Models
{
    public class ResponseModel
    {
        public string? Message { get; set; }
    }

    public class ResponseModel<T>
    {
        public string? Message { get; set; }
        public T? Result { get; set; }
    }
}
