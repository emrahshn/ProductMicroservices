using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Review.API.Aggregators;
using Review.API.Controllers;
using Review.API.Models;

namespace Review.Test
{
    public class ReviewTest
    {
        private readonly ReviewController _controller;
        private readonly Mock<IReviewService> _service;
        private readonly Mock<ILogger<ReviewController>> _logger;
        public ReviewTest()
        {
            _service = new Mock<IReviewService>();
            _logger = new Mock<ILogger<ReviewController>>();
            _controller = new ReviewController(_service.Object, _logger.Object);
        }
        [Fact]
        public void GetReviews_ListOfReview_ReviewExistsInRepo()
        {
            //arrange
            _service.Setup(repo => repo.GetReviews()).ReturnsAsync(GetSampleReviews());

            //act
            var jsonResult = _controller.Get();
            var result = jsonResult.Result as JsonResult;
            var actual = result.Value as IEnumerable<ReviewModel>;

            //assert
            Assert.IsType<JsonResult>(result);
            Assert.Equal(GetSampleReviews().Count(), actual.Count());
        }

        [Fact]
        public void GetReviewById_ReviewObject_ReviewwithSpecificeIdExists()
        {
            //arrange
            var firstReview = GetSampleReviews()[0];
            _service.Setup(x => x.GetReview((int)1).Result).Returns(firstReview);

            //act
            var jsonResult = _controller.GetByProductId((int)1);
            var result = jsonResult.Result as JsonResult;

            //Assert
            Assert.IsType<JsonResult>(result);
            result.Value.Should().BeEquivalentTo(firstReview);
        }

        [Fact]
        public void GetReviewSummaryById_ShouldNotBeEquivalent_ReviewWithId()
        {
            //arrange
            var firstReview = GetSampleReviews()[0];
            _service.Setup(x => x.GetReview((int)1).Result).Returns(firstReview);

            //act
            var jsonResult = _controller.GetSummaryByProductId((int)1);
            var result = jsonResult.Result as JsonResult;

            //Assert
            Assert.IsType<JsonResult>(result);
            result.Value.Should().NotBeEquivalentTo(firstReview);
        }

        #region sample data
        private List<ReviewModel> GetSampleReviews()
        {
            List<ReviewModel> output = new List<ReviewModel>
            {
                new ReviewModel
                {
                    Title = "Test Title 1",
                    Product = new ProductModel(){
                    Id=1,
                    Name="Test Product 1",
                    IsDeleted=false,
                    IsActive=true,
                    CreatedAt=DateTime.Now
                    },
                    Recommendations = new List<RecommendationModel>(){
                        new RecommendationModel() {
                            Id=1,
                            UserId=1,
                            UserName="Test User",
                            RecommendedTo="test@test.com",
                            IsDeleted=false,
                            IsActive=true,
                            CreatedAt=DateTime.Now
                        }
                    },
                    Comments = new List<CommentModel>(){
                        new CommentModel() {
                            Id=1,
                            UserId=1,
                            UserName="Test User",
                            Description="Test Description",
                            IsDeleted=false,
                            IsActive=true,
                            CreatedAt=DateTime.Now
                        }
                    },
                    Votes = new List<VoteModel>(){
                        new VoteModel() {
                            Id=1,
                            UserId=1,
                            UserName="Test User",
                            Value=3,
                            IsDeleted=false,
                            IsActive=true,
                            CreatedAt=DateTime.Now
                        }
                    },
                    ProductName= "Test Product 1",
                    RecommendationPercantage= 12.2,
                    AverageScore= 4.8,
                },
                new ReviewModel
                {
                    Title = "Test Title 2",
                    Product = new ProductModel(){
                    Id=1,
                    Name="Test Product 2",
                    IsDeleted=false,
                    IsActive=true,
                    CreatedAt=DateTime.Now
                    },
                    Recommendations = new List<RecommendationModel>(){
                        new RecommendationModel() {
                            Id=2,
                            UserId=2,
                            UserName="Test User 2",
                            RecommendedTo="test2@test.com",
                            IsDeleted=false,
                            IsActive=true,
                            CreatedAt=DateTime.Now
                        }
                    },
                    Comments = new List<CommentModel>(){
                        new CommentModel() {
                            Id=2,
                            UserId=2,
                            UserName="Test User 2",
                            Description="Test Description 2",
                            IsDeleted=false,
                            IsActive=true,
                            CreatedAt=DateTime.Now
                        }
                    },
                    Votes = new List<VoteModel>(){
                        new VoteModel() {
                            Id=2,
                            UserId=2,
                            UserName="Test User 2",
                            Value=4,
                            IsDeleted=false,
                            IsActive=true,
                            CreatedAt=DateTime.Now
                        }
                    },
                    ProductName= "Test Product 2",
                    RecommendationPercantage= 97.8,
                    AverageScore= 4.4,
                }
            };
            return output;
        }
        #endregion
    }
}