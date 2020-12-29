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
    using Xunit;

    public class LikeTests
    {
        [Fact]
        public async Task ShouldReturnAllTownsAsKeyValuePair()
        {

            var homeList = new List<Home>();
            var mockRepoHome = new Mock<IDeletableEntityRepository<Home>>();
            mockRepoHome.Setup(x => x.All()).Returns(homeList.AsQueryable());
            mockRepoHome.Setup(x => x.HardDelete(It.IsAny<Home>())).Callback((Home home) =>
            homeList.Remove(home));
            mockRepoHome.Setup(x => x.AddAsync(It.IsAny<Home>())).Callback((Home home) => homeList.Add(home));

            var likeList = new List<Like>();
            var mockRepoLike = new Mock<IDeletableEntityRepository<Like>>();
            mockRepoLike.Setup(x => x.AllAsNoTracking()).Returns(likeList.AsQueryable());
            mockRepoLike.Setup(x => x.All()).Returns(likeList.AsQueryable());
            mockRepoLike.Setup(x => x.HardDelete(It.IsAny<Like>())).Callback((Like like) =>
            likeList.Remove(like));
            mockRepoLike.Setup(x => x.AddAsync(It.IsAny<Like>())).Callback((Like like) => likeList.Add(like));

            var serviceLike = new LikesService(mockRepoLike.Object);
            var serviceHome = new PropertyService(mockRepoHome.Object);

            var userId1 = Guid.NewGuid().ToString();
            var userId2 = Guid.NewGuid().ToString();
            var userId3 = Guid.NewGuid().ToString();

            var home = new CreateHomeInputModel() { Title = "Hello"};

            await serviceHome.CreateAsync(home, userId1, "/localImages");


            //Adding likes with differentUser
            homeList[0].Id = 1;
            homeList[0].Likes = new List<Like>();
            await serviceLike.IncrementLikeAsync(1, userId2);
            homeList[0].Likes.Add(new Like() { HomeId = 1, AddedByUserId = userId2 });
            await serviceLike.IncrementLikeAsync(1, userId3);
            homeList[0].Likes.Add(new Like() { HomeId = 1, AddedByUserId = userId3 });

            Assert.Equal(2, homeList[0].Likes.Count());

            //Adding like with same user 2 tmes
            Func<Task> act = async () => await serviceLike.IncrementLikeAsync(1, userId2);
            var invalidOperationException = await Assert.ThrowsAsync<InvalidOperationException>(act);

            Assert.Equal("You cannot like second time!", invalidOperationException.Message);

            //Decremnt like
            await serviceLike.DecrementLikes(1, userId2);
            var like = homeList[0].Likes.Where(x => x.HomeId == 1 && x.AddedByUserId == userId2).FirstOrDefault();
            homeList[0].Likes.Remove(like);
            Assert.Equal(1, homeList[0].Likes.Count());


            //Decrement like if you didnt like it error
            Func<Task> decrement = async () => await serviceLike.DecrementLikes(1, userId2);
            var invalidOperationExceptionDecrement = await Assert.ThrowsAsync<InvalidOperationException>(decrement);
            Assert.Equal("The user didn't liked the post to dislike it!", invalidOperationExceptionDecrement.Message);

        }
    }
}
