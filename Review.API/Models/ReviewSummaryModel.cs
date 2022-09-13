namespace Review.API.Models
{
    public abstract class ReviewSummaryModel
    {
        public string ProductName { get; set; }
        public double RecommendationPercantage { get; set; }
        public double AverageScore { get; set; }
    }
}
