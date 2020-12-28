namespace MyHome.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using MyHome.Data;
    using MyHome.Data.Common.Repositories;
    using MyHome.Data.Models;
    using MyHome.Services.Mapping;
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
        public async Task MustCreateProperty_AndDeleteProperty()
        {

            var list = new List<Home>();
            var mockRepo = new Mock<IDeletableEntityRepository<Home>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.HardDelete(It.IsAny<Home>())).Callback((Home home) => 
            list.Remove(home));
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
            var home2 = new CreateHomeInputModel()
            {
                UploadDate = DateTime.UtcNow,
                Title = "New2",
                Description = "Test",
                Adress = "test",
            };
            var home3 = new CreateHomeInputModel()
            {
                UploadDate = DateTime.UtcNow,
                Title = "New3",
                Description = "Test",
                Adress = "test",
            };

            //Creation test
            await service.CreateAsync(home, ownerId, "/localImages");
            list[0].Id = 1;
            Assert.Equal(1, list.Count());
            Assert.Equal("New", list.First().Title);


            await service.CreateAsync(home2, ownerId, "/localImages");
            await service.CreateAsync(home3, ownerId, "/localImages");
            list[1].Id = 2;
            list[2].Id = 3;

            Assert.Equal(3, list.Count());
            await service.DeletePropertyAsync(2);
            Assert.Equal(2, list.Count());
            await service.DeletePropertyAsync(1);
            Assert.Equal(1, list.Count());
            Assert.Equal(3, list[0].Id);


        }

        [Fact]
        public async Task MustReturnAllUsersByGiveId()
        {
            //Creating the mapping
            AutoMapperConfig.RegisterMappings(typeof(MyPropertyViewModel).Assembly);

            //Creating the Entity
            var list = new List<Home>();
            var mockRepo = new Mock<IDeletableEntityRepository<Home>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Home>())).Callback((Home home) => list.Add(home));
            var service = new PropertyService(mockRepo.Object);

            //Creating the home with local userId
            var ownerId = Guid.NewGuid().ToString();
            var home = new CreateHomeInputModel()
            {
                UploadDate = DateTime.UtcNow,
                Title = "New",
                Description = "Test",
                Adress = "test",
            };
            var home2 = new CreateHomeInputModel()
            {
                UploadDate = DateTime.UtcNow,
                Title = "New2",
                Description = "Test2",
                Adress = "test2",
            };

            await service.CreateAsync(home, ownerId, "/localImages");
            await service.CreateAsync(home2, ownerId, "/localImages");
            var ownerHomes = service.TakeAllByUserId<MyPropertyViewModel>(ownerId).Count();
            var randomHomes = service.TakeAllByUserId<MyPropertyViewModel>("random").Count();

            Assert.Equal(2, ownerHomes);
            Assert.Equal(0, randomHomes);
        }

        [Fact]
        public async Task IncrementViewShouldWorkCorrectly()
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
            var home2 = new CreateHomeInputModel()
            {
                UploadDate = DateTime.UtcNow,
                Title = "New2",
                Description = "Test2",
                Adress = "test2",
            };

            await service.CreateAsync(home, ownerId, "/localImages");
            await service.CreateAsync(home2, ownerId, "/localImages");

            //Setting id
            list[0].Id = 1;
            list[1].Id = 2;

            await service.IncrementView(1);
            await service.IncrementView(1);
            await service.IncrementView(1);

            await service.IncrementView(2);
            await service.IncrementView(2);

            Assert.Equal(3, list[0].Views);
            Assert.Equal(2, list[1].Views);
        }

        [Fact]
        public async Task Update_PropertyShouldThrowException_AndCompleteIfInformationIsCorrect()
        {
            var list = new List<Home>();
            var mockRepo = new Mock<IDeletableEntityRepository<Home>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());

            mockRepo.Setup(x => x.AddAsync(It.IsAny<Home>())).Callback((Home home) => list.Add(home));
            var service = new PropertyService(mockRepo.Object);

            //Initialize userid and the home
            var ownerId = Guid.NewGuid().ToString();
            var home = new CreateHomeInputModel()
            {
                UploadDate = DateTime.UtcNow,
                Title = "New",
                Description = "Test",
                Adress = "test",
            };
            await service.CreateAsync(home, ownerId, "/localImages");

            //First test for negative Id
            Func<Task> act = async () => await service.UpdateAsync(-5, null);
            var invalidOperationException = await Assert.ThrowsAsync<InvalidOperationException>(act);

            //Setting id and creating Edit model
            list[0].Id = 1;
            var editModel = new EditHomeInputModel()
            {
                Title = "Edited",
            };
            await service.UpdateAsync(1, editModel);

            Assert.Equal("Invalid id!", invalidOperationException.Message);
            Assert.Equal("Edited", list[0].Title);
        }

        [Fact]
        public async Task GetAllShouldGiveCorrectFirstEight_AndGiveZero()
        {
            AutoMapperConfig.RegisterMappings(typeof(PropertyInListViewModel).Assembly);

            var list = new List<Home>();
            var mockRepo = new Mock<IDeletableEntityRepository<Home>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Home>())).Callback((Home home) => list.Add(home));
            var service = new PropertyService(mockRepo.Object);

            //First test when is empty
            var zero = service.GetAll(1, 8).Count();
            Assert.Equal(0, zero);

            var ownerId = Guid.NewGuid().ToString();
            for (int i = 0; i < 8; i++)
            {
                var home = new CreateHomeInputModel()
                {
                    UploadDate = DateTime.UtcNow,
                    Title = $"New{i}",
                    Description = "Test",
                    Adress = "test",
                };
                await service.CreateAsync(home, ownerId, "/localImages");
            }

            var firstPage = service.GetAll(1, 8).Count();
            Assert.Equal(8, firstPage);

            for (int i = 0; i < 10; i++)
            {
                var home = new CreateHomeInputModel()
                {
                    UploadDate = DateTime.UtcNow,
                    Title = $"New{i}",
                    Description = "Test",
                    Adress = "test",
                };
                await service.CreateAsync(home, ownerId, "/localImages");
            }

            var secondPage = service.GetAll(3, 8).Count();
            Assert.Equal(2, secondPage);
        }

        [Fact]
        public async Task AllByUserIdCountShouldGiveZeroIfDontHave_NumberIfItHas()
        {
            var list = new List<Home>();
            var mockRepo = new Mock<IDeletableEntityRepository<Home>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Home>())).Callback((Home home) => list.Add(home));
            var service = new PropertyService(mockRepo.Object);

            var ownerId = Guid.NewGuid().ToString();

            Assert.Equal(0, service.GetAllByUserCount(ownerId));

            var home = new CreateHomeInputModel()
            {
                UploadDate = DateTime.UtcNow,
                Title = "New",
                Description = "Test",
                Adress = "test",
            };
            var home2 = new CreateHomeInputModel()
            {
                UploadDate = DateTime.UtcNow,
                Title = "New2",
                Description = "Test2",
                Adress = "test2",
            };

            await service.CreateAsync(home, ownerId, "/localimages");
            await service.CreateAsync(home2, ownerId, "/localimages");


            Assert.Equal(2, service.GetAllByUserCount(ownerId));
        }

        [Fact]
        public async Task GetCountShouldGiveZeroIfEmpty_AndNumberIfNot()
        {
            var list = new List<Home>();
            var mockRepo = new Mock<IDeletableEntityRepository<Home>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Home>())).Callback((Home home) => list.Add(home));
            var service = new PropertyService(mockRepo.Object);

            var ownerId = Guid.NewGuid().ToString();

            Assert.Equal(0, service.GetCount());

            var home = new CreateHomeInputModel()
            {
                UploadDate = DateTime.UtcNow,
                Title = "New",
                Description = "Test",
                Adress = "test",
            };
            var home2 = new CreateHomeInputModel()
            {
                UploadDate = DateTime.UtcNow,
                Title = "New2",
                Description = "Test2",
                Adress = "test2",
            };

            await service.CreateAsync(home, ownerId, "/localimages");
            await service.CreateAsync(home2, ownerId, "/localimages");

            Assert.Equal(2, service.GetCount());
        }

        [Fact]
        public async Task DoesContainPropertyShouldGiveZeroIfForEmpty_AndNumberForNot()
        {
            var list = new List<Home>();
            var mockRepo = new Mock<IDeletableEntityRepository<Home>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Home>())).Callback((Home home) => list.Add(home));
            var service = new PropertyService(mockRepo.Object);

            var ownerId = Guid.NewGuid().ToString();

            Assert.Equal(false, service.DoesContainProperty(1));

            var home = new CreateHomeInputModel()
            {
                UploadDate = DateTime.UtcNow,
                Title = "New",
                Description = "Test",
                Adress = "test",
            };
            var home2 = new CreateHomeInputModel()
            {
                UploadDate = DateTime.UtcNow,
                Title = "New2",
                Description = "Test2",
                Adress = "test2",
            };
            await service.CreateAsync(home, ownerId, "/localimages");
            await service.CreateAsync(home2, ownerId, "/localimages");
            list[0].Id = 1;
            list[1].Id = 2;

            Assert.Equal(true, service.DoesContainProperty(1));
            Assert.Equal(true, service.DoesContainProperty(1));

        }

        [Fact]
        public async Task TakingPropertiesWithCategories()
        {
            AutoMapperConfig.RegisterMappings(typeof(PropertyInListViewModel).Assembly);

            var list = new List<Home>();
            var mockRepo = new Mock<IDeletableEntityRepository<Home>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Home>())).Callback((Home home) => list.Add(home));
            var service = new PropertyService(mockRepo.Object);

            var ownerId = Guid.NewGuid().ToString();

            //Tests for taking the category count
            Assert.Equal(0, service.GetAllWithCategoryCount("House"));

            var home = new CreateHomeInputModel()
            {
                UploadDate = DateTime.UtcNow,
                Title = "New",
                Description = "Test",
                Adress = "test",
            };
            var home2 = new CreateHomeInputModel()
            {
                UploadDate = DateTime.UtcNow,
                Title = "New2",
                Description = "Test2",
                Adress = "test2",
            };
            var home3 = new CreateHomeInputModel()
            {
                UploadDate = DateTime.UtcNow,
                Title = "New3",
                Description = "Test2",
                Adress = "test2",
            };
            await service.CreateAsync(home, ownerId, "/localimages");
            await service.CreateAsync(home2, ownerId, "/localimages");
            await service.CreateAsync(home3, ownerId, "/localimages");

            list[0].Category = new Category() { Name = "House" };
            list[1].Category = new Category() { Name = "House" };
            list[2].Category = new Category() { Name = "Apartment" };

            Assert.Equal(2, service.GetAllWithCategoryCount("House"));
            Assert.Equal(1, service.GetAllWithCategoryCount("Apartment"));

            //Tests for taking properties for categories page
            for (int i = 0; i < 15; i++)
            {
                var nestedHome = new CreateHomeInputModel() {
                    Title = $"New{i}",
                };
                await service.CreateAsync(nestedHome, ownerId, "/localimages");
            }

            list[3].Category = new Category() { Name = "House" };
            list[4].Category = new Category() { Name = "House" };
            list[5].Category = new Category() { Name = "Apartment" };
            list[6].Category = new Category() { Name = "House" };
            list[7].Category = new Category() { Name = "Garage" };
            list[8].Category = new Category() { Name = "Apartment" };
            list[9].Category = new Category() { Name = "House" };
            list[10].Category = new Category() { Name = "House" };
            list[11].Category = new Category() { Name = "Apartment" };
            list[12].Category = new Category() { Name = "Garage" };
            list[13].Category = new Category() { Name = "Apartment" };
            list[14].Category = new Category() { Name = "House" };
            list[15].Category = new Category() { Name = "House" };
            list[16].Category = new Category() { Name = "Apartment" };
            list[17].Category = new Category() { Name = "Garage" };


            Assert.Equal(1, service.GetAllWithCategory("House", 2, 8).Count());
            Assert.Equal(6, service.GetAllWithCategory("Apartment", 1, 8).Count());
            Assert.Equal(3, service.GetAllWithCategory("Garage", 1, 8).Count());
            Assert.Equal(0, service.GetAllWithCategory("Random", 1, 8).Count());
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
