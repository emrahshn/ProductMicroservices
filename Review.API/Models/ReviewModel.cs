using System.Collections.Generic;

namespace Review.API.Models
{
    public class ReviewModel:ReviewSummaryModel
    {
        public ReviewModel()
        {
            Product = new();
            Recommendations = new();
            Comments = new();
        }
        public string Title { get; set; }
        public ProductModel Product { get; set; }
        public List<RecommendationModel> Recommendations { get; set; }
        public List<CommentModel> Comments { get; set; }
        public List<VoteModel> Votes { get; set; }
    }
}
