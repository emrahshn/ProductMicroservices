using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Review.API.Aggregators;
using Review.API.Models;

namespace Review.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly ILogger<ReviewController> _logger;
        public ReviewController(IReviewService reviewService,
            ILogger<ReviewController> logger)
        {
            _reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get all reviews of all products
        /// </summary>
        /// <remarks>
        /// Because ordinary HttpClient was used for the requests, It can be a bit slow for the first call.
        /// Queries will be more rapid when cache is activated. When the project is desired to be extended, gRPC or SignalR can be implemented simply.
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _reviewService.GetReviews();
            return Json(response);
        }

        /// <summary>
        /// Get review of a specific product by id
        /// </summary>
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var response = await _reviewService.GetReview(productId);
            return Json(response);
        }

        /// <summary>
        /// Get review summary of a specific product by id
        /// </summary>
        [HttpGet("summary/{productId}")]
        public async Task<IActionResult> GetSummaryByProductId(int productId)
        {
            var response = await _reviewService.GetReviewSummary(productId);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReviewSummaryModel review)
        {
            return Ok(review);
        }
    }
}