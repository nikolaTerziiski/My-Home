namespace MyHome.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHome.Common;
    using MyHome.Data;
    using MyHome.Data.Models;
    using MyHome.Services.Data;
    using MyHome.Web.ViewModels.Users;

    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPropertyService propertyService;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly ApplicationDbContext dbContext;

        public UsersController(
                               UserManager<ApplicationUser> userManager,
                               IPropertyService propertyService,
                               RoleManager<ApplicationRole> roleManager,
                               ApplicationDbContext dbContext)
        {
            this.userManager = userManager;
            this.propertyService = propertyService;
            this.roleManager = roleManager;
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult All()
        {
            var viewModel = new UsersListViewModel();
            viewModel.Users = this.userManager.Users.Select(x => new UserInListViewModel
            {
                Id = x.Id,
                HomesCount = this.propertyService.GetAllByUserCount(x.Id),
                Name = x.UserName,
                Role = this.userManager.GetRolesAsync(x).Result.Last(),
            }).ToList();

            viewModel.CurrentUserId = this.userManager.GetUserId(this.User);

            return this.View(viewModel);
        }
    }
}
