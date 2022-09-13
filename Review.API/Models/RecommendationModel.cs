namespace Review.API.Models
{
    public class RecommendationModel:BaseModel
    {
        public string RecommendedTo { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
