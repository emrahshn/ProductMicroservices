namespace Review.Recommendations.Entities
{
    public class Recommendation : BaseEntity
    {
        public string RecommendedTo { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}