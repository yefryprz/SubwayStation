namespace SubwayStation.Domain.Entities
{
    public class EntityBase
    {
        public Guid Id { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid UserId { get; set; }
    }
}
