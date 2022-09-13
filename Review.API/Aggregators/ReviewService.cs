using Review.API.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Review.API.Extensions;
using Microsoft.Extensions.Options;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Review.API.Aggregators
{
    public class ReviewService : IReviewService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;
        private static Random rand = new Random(DateTime.Now.Second);
        public List<UserModel> Users { get; set; }
        public ReviewService(HttpClient httpClient,
            IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiSettings = apiSettings.Value ?? throw new ArgumentNullException(nameof(apiSettings.Value));
        }

        /// <summary>
        /// This method gets the review
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<ReviewModel> GetReview(int productId)
        {
            ProductModel productModel = new ProductModel();
            ReviewModel reviewModel = new ReviewModel();
            var productResponse = await _httpClient.GetAsync(String.Format("{0}/api/product/{1}", _apiSettings.ProductUrl, productId));
            if (productResponse.IsSuccessStatusCode)
            {
                productModel = await productResponse.ReadContentAs<ProductModel>();
                //Getting user list from microservice
                var userResponse = await _httpClient.GetAsync(String.Format("{0}/api/user", _apiSettings.UserUrl));
                Users = await userResponse.ReadContentAs<List<UserModel>>();

                //Getting recommendations list from microservice
                var recommendationResponse = await _httpClient.GetAsync(String.Format("{0}/api/recommendation/product/{1}", _apiSettings.RecommendationUrl, productId));
                var recommendations = await recommendationResponse.ReadContentAs<List<RecommendationModel>>();
                recommendations.ForEach(x => x.UserName = Users.Any(p => p.Id == x.UserId) ? Users.First(p => p.Id == x.UserId).Name : String.Empty);

                //Getting comments list from microservice
                var commentResponse = await _httpClient.GetAsync(String.Format("{0}/api/comment/product/{1}", _apiSettings.CommentUrl, productId));
                var comments = await commentResponse.ReadContentAs<List<CommentModel>>();
                comments.ForEach(x => x.UserName = Users.Any(p => p.Id == x.UserId) ? Users.First(p => p.Id == x.UserId).Name : String.Empty);

                //Getting votes list from microservice
                var voteResponse = await _httpClient.GetAsync(String.Format("{0}/api/vote/product/{1}", _apiSettings.VoteUrl, productId));
                var votes = await voteResponse.ReadContentAs<List<VoteModel>>();
                votes.ForEach(x => x.UserName = Users.Any(p => p.Id == x.UserId) ? Users.First(p => p.Id == x.UserId).Name : String.Empty);

                //Setting the required variables
                reviewModel.Product = productModel;
                reviewModel.ProductName = productModel.Name ?? "";
                reviewModel.Title = await GenerateTitle() ?? "";
                reviewModel.Recommendations = recommendations;
                reviewModel.Comments = comments;
                reviewModel.Votes = votes;
                reviewModel.AverageScore = await CalculateAverageScore(votes);
                reviewModel.RecommendationPercantage = await AverageRecommendations(recommendations.Count);
            }
            return reviewModel;
        }

        /// <summary>
        /// This method gets the summary of the review
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<ReviewSummaryModel> GetReviewSummary(int productId)
        {
            ProductModel productModel = new ProductModel();
            ReviewModel reviewModel = new ReviewModel();
            var productResponse = await _httpClient.GetAsync(String.Format("{0}/api/product/{1}", _apiSettings.ProductUrl, productId));
            if (productResponse.IsSuccessStatusCode)
            {
                productModel = await productResponse.ReadContentAs<ProductModel>();

                //Getting recommendations list from microservice
                var recommendationResponse = await _httpClient.GetAsync(String.Format("{0}/api/recommendation/product/{1}", _apiSettings.RecommendationUrl, productId));
                var recommendations = await recommendationResponse.ReadContentAs<List<RecommendationModel>>();

                //Getting votes list from microservice
                var voteResponse = await _httpClient.GetAsync(String.Format("{0}/api/vote/product/{1}", _apiSettings.VoteUrl, productId));
                var votes = await voteResponse.ReadContentAs<List<VoteModel>>();

                //Setting the required variables
                reviewModel.Product = productModel;
                reviewModel.ProductName = productModel.Name ?? "";
                reviewModel.Title = await GenerateTitle() ?? "";
                reviewModel.AverageScore = await CalculateAverageScore(votes);
                reviewModel.RecommendationPercantage = await AverageRecommendations(recommendations.Count);
            }
            return reviewModel;
        }

        /// <summary>
        /// This method gets the reviews for all products
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ReviewModel>> GetReviews()
        {
            List<ProductModel> productModels = new List<ProductModel>();    
            List<ReviewModel> reviewModels = new List<ReviewModel>();
            var productResponse = await _httpClient.GetAsync(String.Format("{0}/api/product", _apiSettings.ProductUrl));
            if (productResponse.IsSuccessStatusCode)
            {
                productModels = await productResponse.ReadContentAs<List<ProductModel>>();
                foreach (var product in productModels)
                {
                    var reviewModel = await GetReview(product.Id);
                    reviewModels.Add(reviewModel);
                }
            }
            return reviewModels;
        }

        /// <summary>
        /// Generates specific title for each review.
        /// </summary>
        /// <returns></returns>
        private async Task<string> GenerateTitle()
        {
            return "review_"+ rand.Next().ToString();
        }

        /// <summary>
        /// Calculates the average vote score from the vote values
        /// </summary>
        /// <returns></returns>
        private async Task<double> CalculateAverageScore(List<VoteModel> votes)
        {
            return Math.Round((double)votes.Sum(x => x.Value) / votes.Count,2); 
        }

        /// <summary>
        /// Returns percentage of recommendations
        /// </summary>
        /// <returns></returns>
        private async Task<double> AverageRecommendations(int numberOfProductRecommendations)
        {
            var recommendationCountResponse = await _httpClient.GetAsync(String.Format("{0}/api/recommendation/count", _apiSettings.RecommendationUrl));
            int totalRecommendations = await recommendationCountResponse.ReadContentAs<int>();
            return Math.Round(((double)numberOfProductRecommendations * 100)/ totalRecommendations,2);
        }
    }
}
