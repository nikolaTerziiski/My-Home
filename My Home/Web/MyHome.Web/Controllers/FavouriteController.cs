using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyHome.Data.Common.Repositories;
using MyHome.Data.Models;
using MyHome.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHome.Web.Controllers
{
    public class FavouriteController : Controller
    {
        private readonly IFavouriteService favouriteService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<Home> propertyRepository;
        private readonly IDeletableEntityRepository<Favourite> favourtiesRepository;
        private readonly IRepository<FavouriteHome> favouriteHomeRepository;

        public FavouriteController(
                        IFavouriteService favouriteService,
                        UserManager<ApplicationUser> userManager,
                        IDeletableEntityRepository<Home> propertyRepository,
                        IDeletableEntityRepository<Favourite> favourtiesRepository,
                        IRepository<FavouriteHome> favouriteHomeRepository)
        {
            this.favouriteService = favouriteService;
            this.userManager = userManager;
            this.propertyRepository = propertyRepository;
            this.favourtiesRepository = favourtiesRepository;
            this.favouriteHomeRepository = favouriteHomeRepository;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Add(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var favourite = this.favourtiesRepository.All().Where(x => x.UserId == user.Id).FirstOrDefault();
            var home = this.propertyRepository.AllAsNoTracking().Where(x => x.Id == id).FirstOrDefault();

            var te = this.favouriteHomeRepository.All();

            favourite.Homes.Add(home);
            await this.favourtiesRepository.SaveChangesAsync();
            var favourite3 = this.favourtiesRepository.All().Where(x => x.UserId == user.Id).FirstOrDefault();

            return this.RedirectToAction("Property", "Details", new { id = id });
        }
    }
}
