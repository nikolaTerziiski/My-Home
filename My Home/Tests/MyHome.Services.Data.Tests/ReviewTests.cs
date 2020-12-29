namespace MyHome.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Moq;
    using MyHome.Data.Common.Repositories;
    using MyHome.Data.Models;
    using MyHome.Services.Mapping;
    using MyHome.Web.ViewModels.Property;
    using MyHome.Web.ViewModels.Reviews;
    using Xunit;

    public class ReviewTests
    {
        [Fact]
        public async Task AllTestsForReviews()
        {
            AutoMapperConfig.RegisterMappings(typeof(PropertyDetailsReviewViewModel).Assembly);

            var homeList = new List<Home>();
            var mockRepoHome = new Mock<IDeletableEntityRepository<Home>>();
            mockRepoHome.Setup(x => x.All()).Returns(homeList.AsQueryable());
            mockRepoHome.Setup(x => x.HardDelete(It.IsAny<Home>())).Callback((Home home) =>
            homeList.Remove(home));
            mockRepoHome.Setup(x => x.AddAsync(It.IsAny<Home>())).Callback((Home home) => homeList.Add(home));

            var reviewList = new List<Review>();
            var mockRepoReview = new Mock<IDeletableEntityRepository<Review>>();
            mockRepoReview.Setup(x => x.AllAsNoTracking()).Returns(reviewList.AsQueryable());
            mockRepoReview.Setup(x => x.All()).Returns(reviewList.AsQueryable());
            mockRepoReview.Setup(x => x.HardDelete(It.IsAny<Review>())).Callback((Review review) =>
            reviewList.Remove(review));
            mockRepoReview.Setup(x => x.AddAsync(It.IsAny<Review>())).Callback((Review review) => reviewList.Add(review));

            var serviceReview = new ReviewService(mockRepoReview.Object, mockRepoHome.Object);
            var serviceHome = new PropertyService(mockRepoHome.Object);

            var userId1 = Guid.NewGuid().ToString();

            var home = new CreateHomeInputModel() { Title = "Hello" };
            await serviceHome.CreateAsync(home, userId1, "/localImages");


            //Adding review to home
            homeList[0].Id = 1;
            homeList[0].Reviews = new List<Review>();

            var createReview = new CreateReviewInputModel()
            {
                Id = 1,
                Content = "Nope",
            };
            await serviceReview.AddReview(userId1, createReview);
            await mockRepoReview.Object.AddAsync(new Review()
            {
                HomeId = 1,
                Content = "Nope",
                AddedByUserId = userId1,
            });

            Assert.Equal(1, homeList[0].Reviews.Count());

            var createReview2 = new CreateReviewInputModel()
            {
                Id = 1,
                Content = "Text",
            };
            await serviceReview.AddReview(userId1, createReview2);
            await mockRepoReview.Object.AddAsync(new Review()
            {
                HomeId = 1,
                Content = "Text",
                AddedByUserId = userId1,
            });

            Assert.Equal(2, homeList[0].Reviews.Count());

            //Take all by propertyId
            var result = serviceReview.TakeById<PropertyDetailsReviewViewModel>(1).Count();
            Assert.Equal(2, result);

            var zeroResult = serviceReview.TakeById<PropertyDetailsReviewViewModel>(100).Count();
            Assert.Equal(0, zeroResult);

            //Test for taking all created by user
            Assert.Equal(2, serviceReview.TakeForUser(userId1).Count());
            Assert.Equal(0, serviceReview.TakeForUser("dummyTest").Count());
        }
    }
}
