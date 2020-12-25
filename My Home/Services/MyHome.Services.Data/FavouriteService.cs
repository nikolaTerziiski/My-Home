namespace MyHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MyHome.Data;

    using Microsoft.AspNetCore.Identity;
    using MyHome.Data.Common.Repositories;
    using MyHome.Data.Models;

    public class FavouriteService : IFavouriteService
    {
        private readonly IDeletableEntityRepository<Favourite> favouritesRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<Home> propertyRepository;

        public FavouriteService(IDeletableEntityRepository<Favourite> favouritesRepository,
                                UserManager<ApplicationUser> userManager,
                                IDeletableEntityRepository<Home> propertyRepository)
        {
            this.favouritesRepository = favouritesRepository;
            this.userManager = userManager;
            this.propertyRepository = propertyRepository;
        }

        public async Task AddAsync(int id, string userId)
        {
            var favourite = this.favouritesRepository.All().Where(x => x.UserId == userId).FirstOrDefault();
            var home = this.propertyRepository.AllAsNoTracking().Where(x => x.Id == id).FirstOrDefault();

            favourite.Homes.Add(home);
            await this.favouritesRepository.SaveChangesAsync();

            var favourite2 = this.favouritesRepository.All().Where(x => x.UserId == userId).FirstOrDefault();
        }

        public bool DoesContain(int id, string userId)
        {
            var favourite2 = this.favouritesRepository.All().Where(x => x.UserId == userId).FirstOrDefault();

            if (!this.propertyRepository.AllAsNoTracking().Any(x => x.Id == id))
            {
                throw new InvalidOperationException("Invalid id for proeprty!");
            }

            if (this.favouritesRepository.AllAsNoTracking().Where(x => x.UserId == userId).FirstOrDefault().Homes.Any(x => x.Id == id))
            {
                return true;
            }

            var fav = this.favouritesRepository.All().Where(x => x.UserId == userId).FirstOrDefault();
            return false;
        }

    }
}
