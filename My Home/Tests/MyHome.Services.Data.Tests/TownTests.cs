using Moq;
using MyHome.Data.Common.Repositories;
using MyHome.Data.Models;
using MyHome.Services.Mapping;
using MyHome.Web.ViewModels.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyHome.Services.Data.Tests
{
    public class TownTests
    {
        [Fact]
        public async Task ShouldReturnAllTownsAsKeyValuePair()
        {
            AutoMapperConfig.RegisterMappings(typeof(TownInTableViewModel).Assembly);

            var list = new List<Town>();
            var mockRepo = new Mock<IDeletableEntityRepository<Town>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.HardDelete(It.IsAny<Town>())).Callback((Town home) =>
            list.Remove(home));
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Town>())).Callback((Town home) => list.Add(home));

            var service = new TownService(mockRepo.Object);
            var town1 = new Town() { Name = "New" };
            var town2 = new Town() { Name = "New2" };
            var town3 = new Town() { Name = "New3" };

            await mockRepo.Object.AddAsync(town1);
            await mockRepo.Object.AddAsync(town2);
            await mockRepo.Object.AddAsync(town3);

            list[0].Id = 1;
            list[1].Id = 2;
            list[2].Id = 3;

            var result = service.GetAllTowns();
            Assert.Equal(3, result.Count());
            Assert.Equal("1", result.First().Key);

            town1.Homes.Add(new Home { Title = "new" });
            town1.Homes.Add(new Home { Title = "new" });
            town1.Homes.Add(new Home { Title = "new" });
            town2.Homes.Add(new Home { Title = "new" });
            town2.Homes.Add(new Home { Title = "new" });

            var resultForTable = service.GetAllTownsForTable();

            Assert.Equal(3, resultForTable.First().Homes.Count());

        }
    }
}
