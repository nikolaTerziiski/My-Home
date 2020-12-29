namespace MyHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using MyHome.Data.Common.Repositories;
    using MyHome.Data.Models;
    using MyHome.Web.ViewModels.Favourite;

    public class FavouriteService : IFavouriteService
    {
        private readonly IDeletableEntityRepository<Favourite> favouritesRepository;
        private readonly IDeletableEntityRepository<Home> propertyRepository;
        private readonly IDeletableEntityRepository<FavouriteHome> favouriteHomeRepository;

        public FavouriteService(
                                IDeletableEntityRepository<Favourite> favouritesRepository,
                                IDeletableEntityRepository<Home> propertyRepository,
                                IDeletableEntityRepository<FavouriteHome> favouriteHomeRepository)
        {
            this.favouritesRepository = favouritesRepository;
            this.propertyRepository = propertyRepository;
            this.favouriteHomeRepository = favouriteHomeRepository;
        }

        public async Task AddAsync(int id, string userId)
        {
            //Initialize
            var favourite = this.favouritesRepository.All().Where(x => x.UserId == userId).FirstOrDefault();
            var home = this.propertyRepository.All().Where(x => x.Id == id).FirstOrDefault();

            if (this.favouriteHomeRepository.AllWithDeleted().Any(x => x.IsDeleted == true && x.HomeId == home.Id && x.FavouriteId == favourite.Id))
            {
                var deletedFavourite = this.favouriteHomeRepository.AllWithDeleted().Where(x => x.HomeId == home.Id && x.FavouriteId == favourite.Id).FirstOrDefault();

                deletedFavourite.IsDeleted = false;
                this.favouriteHomeRepository.Update(deletedFavourite);
                await this.favouriteHomeRepository.SaveChangesAsync();
                return;
            }

            if (this.favouriteHomeRepository.AllAsNoTracking().Where(x => x.FavouriteId == favourite.Id).Any(x => x.HomeId == id))
            {
                throw new InvalidOperationException("You already have this in your list!");
            }

            var favourteHome = new FavouriteHome { FavouriteId = favourite.Id, HomeId = id };

            favourite.FavouriteHomes.Add(favourteHome);
            home.FavouriteHomes.Add(favourteHome);

            await this.favouriteHomeRepository.AddAsync(favourteHome);
            await this.favouriteHomeRepository.SaveChangesAsync();
            await this.favouritesRepository.SaveChangesAsync();
            await this.propertyRepository.SaveChangesAsync();
        }

        public bool DoesContain(int id, string userId)
        {
            var favourite = this.favouritesRepository.All().Where(x => x.UserId == userId).FirstOrDefault();

            if (favourite == null)
            {
                return false;
            }

            var favouriteHomesAll = this.favouriteHomeRepository.All().Where(x => x.FavouriteId == favourite.Id);
            if (favouriteHomesAll.Any(x => x.HomeId == id))
            {
                return true;
            }

            return false;
        }

        public ICollection<FavouriteInList> GetAllById(string id)
        {
            var myFavouriteId = this.favouritesRepository.AllAsNoTracking().Where(x => x.UserId == id).FirstOrDefault().Id;

            var myFavourite = this.favouriteHomeRepository.AllAsNoTracking().Where(x => x.FavouriteId == myFavouriteId).Select(x => x.HomeId).ToList();
           
            var homes = this.propertyRepository.AllAsNoTracking().Where(x => myFavourite.Contains(x.Id)).Select(x => new FavouriteInList
            {
                Id = x.Id,
                Category = x.Category,
                HomeStatus = x.Status,
                Image = x.Images.First(),
                Price = x.Price,
                Title = x.Title,
                UploadDate = x.UploadDate,
            }).ToList();

            return homes;
        }

        public async Task RemoveById(int id, string userId)
        {
            var favourite = this.favouritesRepository.AllAsNoTracking().Where(x => x.UserId == userId).FirstOrDefault();

            if (favourite == null)
            {
                throw new InvalidOperationException("User does not exists!");
            }
            var favouriteHome = this.favouriteHomeRepository.All().Where(x => x.FavouriteId == favourite.Id && x.HomeId == id).FirstOrDefault();

            if (favouriteHome == null)
            {
                throw new InvalidOperationException("User does not have in favourites this property!");
            }
            this.favouriteHomeRepository.HardDelete(favouriteHome);

            await this.favouriteHomeRepository.SaveChangesAsync();
        }
    }
}
