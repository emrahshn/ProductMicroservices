using Review.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Review.API.Aggregators
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewModel>> GetReviews();
        Task<ReviewModel> GetReview(int productId);
        Task<ReviewSummaryModel> GetReviewSummary(int productId);
    }
}
