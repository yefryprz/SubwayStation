namespace SubwayStation.Domain.DTOs
{
    public class PageListDTO<T>
    {
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public List<T>? ResultList { get; set; }
    }
}
