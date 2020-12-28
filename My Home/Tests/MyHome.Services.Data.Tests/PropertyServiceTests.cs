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
    using MyHome.Web.ViewModels.Property;
    using Xunit;

    public class PropertyServiceTests
    {

        [Fact]

        public void MustGiveExceptionIfWeSearchWithNegativeId()
        {
            var service = new PropertyService(new FakePropertyReposiory());
            Action act = () => service.TakeOneById<Home>(-5);
            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(act);

            Assert.Equal("Invalid id!", invalidOperationException.Message);
        }

        [Fact]
        public void MustGiveExceptionIfWeSearchWithHigherId()
        {
            var service = new PropertyService(new FakePropertyReposiory());
            Action act = () => service.TakeOneById<Home>(100);
            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(act);

            Assert.Equal("The property with that id does not exists!", invalidOperationException.Message);
        }

        [Fact]
        public async Task MustReturnCorrectProperty()
        {

            var list = new List<Home>();
            var mockRepo = new Mock<IDeletableEntityRepository<Home>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Home>())).Callback((Home home) => list.Add(home));

            var service = new PropertyService(mockRepo.Object);
            var ownerId = Guid.NewGuid().ToString();
            var home = new CreateHomeInputModel()
            {
                UploadDate = DateTime.UtcNow,
                Title = "New",
                Description = "Test",
                Adress = "test",
            };

            await service.CreateAsync(home, ownerId, "/localImages");

            Assert.Equal(1, list.Count());
            Assert.Equal("New", list.First().Title);

        }
    }

    public class FakePropertyReposiory : IDeletableEntityRepository<Home>
    {
        private List<Home> homes = new List<Home>();

        public Task AddAsync(Home entity)
        {
            this.homes.Add(entity);
            return Task.CompletedTask;
        }

        public IQueryable<Home> All()
        {
            return this.homes.AsQueryable();
        }

        public IQueryable<Home> AllAsNoTracking()
        {
            return this.homes.AsQueryable();
        }

        public IQueryable<Home> AllAsNoTrackingWithDeleted()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Home> AllWithDeleted()
        {
            throw new NotImplementedException();
        }

        public void Delete(Home entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<Home> GetByIdWithDeletedAsync(params object[] id)
        {
            throw new NotImplementedException();
        }

        public void HardDelete(Home entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(1);
        }

        public void Undelete(Home entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Home entity)
        {
            throw new NotImplementedException();
        }
    }
}
