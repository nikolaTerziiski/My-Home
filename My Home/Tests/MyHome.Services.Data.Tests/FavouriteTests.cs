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
    using MyHome.Data.Repositories;
    using MyHome.Services.Mapping;
    using MyHome.Web.ViewModels.Favourite;
    using MyHome.Web.ViewModels.Property;
    using Xunit;

    public class FavouriteTests
    {
        [Fact]
        public async Task CheckAllFavouriteCases()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;
            var dbContext = new ApplicationDbContext(options);
            var favouritesRepository = new EfDeletableEntityRepository<Favourite>(dbContext);
            var homesRepository = new EfDeletableEntityRepository<Home>(dbContext);
            var favouriteHomesRepository = new EfDeletableEntityRepository<FavouriteHome>(dbContext);

            var userId1 = Guid.NewGuid().ToString();
            var userId2 = Guid.NewGuid().ToString();

            var favouriteService = new FavouriteService(favouritesRepository, homesRepository, favouriteHomesRepository);

            dbContext.Homes.Add(new Home { Id = 1, Title = "Test1" });
            dbContext.Homes.Add(new Home { Id = 2, Title = "Test2" });
            dbContext.Homes.Add(new Home { Id = 3, Title = "Test3" });
            dbContext.SaveChanges();

            dbContext.Favourite.Add(new Favourite { Id = 1, UserId = userId1, });
            dbContext.Favourite.Add(new Favourite { Id = 2, UserId = userId2, });
            dbContext.SaveChanges();

            await favouriteService.AddAsync(1, userId1);
            Assert.Equal(true, favouriteHomesRepository.AllAsNoTracking().Any(x => x.Id == 1 && x.HomeId == 1));

            //Check if you can add the same
            Func<Task> act = async () => await favouriteService.AddAsync(1, userId1);
            var invalidOperationException = await Assert.ThrowsAsync<InvalidOperationException>(act);

            Assert.Equal("You already have this in your list!", invalidOperationException.Message);

            //Check if exists
            Assert.Equal(true, favouriteService.DoesContain(1, userId1));
            Assert.Equal(false, favouriteService.DoesContain(1, userId2));
            Assert.Equal(false, favouriteService.DoesContain(1, "dummy"));

            //Taking all favourites for user
            await favouriteService.AddAsync(3, userId1);
            await favouriteService.AddAsync(2, userId1);

            //Remove
            Assert.Equal(3, dbContext.FavouriteHomes.Count());
            await favouriteService.RemoveById(1, userId1);
            Assert.Equal(false, dbContext.FavouriteHomes.Any(x => x.HomeId == 1 && x.FavouriteId == 1));
            Assert.Equal(2, dbContext.FavouriteHomes.Count());

            Func<Task> act2 = async () => await favouriteService.RemoveById(5, userId1);
            var invalidOperationException2 = await Assert.ThrowsAsync<InvalidOperationException>(act2);

            Assert.Equal("User does not have in favourites this property!", invalidOperationException2.Message);

            Func<Task> act3 = async () => await favouriteService.RemoveById(1, "dummy");
            var invalidOperationException3 = await Assert.ThrowsAsync<InvalidOperationException>(act3);

            Assert.Equal("User does not exists!", invalidOperationException3.Message);
        }
    }
}
