namespace MyHome.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHome.Data;
    using MyHome.Data.Common.Repositories;
    using MyHome.Data.Models;
    using MyHome.Services.Data;
    using MyHome.Web.ViewModels.Favourite;

    public class FavouriteController : Controller
    {
        private readonly IFavouriteService favouriteService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public FavouriteController(
                        IFavouriteService favouriteService,
                        UserManager<ApplicationUser> userManager,
                        ApplicationDbContext context)
        {
            this.favouriteService = favouriteService;
            this.userManager = userManager;
            this.context = context;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Add(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.favouriteService.AddAsync(id, user.Id);

            return this.RedirectToAction("Details", "Property", new { id = id });
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var list = new FavouriteList { List = this.favouriteService.GetAllById(user.Id) };

            return this.View(list);
        }

        public async Task<IActionResult> Remove(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.favouriteService.RemoveById(id, user.Id);

            return this.RedirectToAction("All", "Favourite");
        }
    }
}
