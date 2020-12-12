namespace MyHome.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHome.Data;
    using MyHome.Data.Models;
    using MyHome.Services.Data;
    using MyHome.Web.ViewModels.Property;

    public class PropertyController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly ITownService townService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPropertyService propertyService;
        private readonly IWebHostEnvironment environment;

        public PropertyController(ICategoryService categoryService,
                                  ITownService townService,
                                  UserManager<ApplicationUser> userManager,
                                  IPropertyService propertyService,
                                  IWebHostEnvironment environment)
        {
            this.categoryService = categoryService;
            this.townService = townService;
            this.userManager = userManager;
            this.propertyService = propertyService;
            this.environment = environment;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CreateHomeInputModel();
            viewModel.Categories = this.categoryService.GetAllCategories();
            viewModel.Towns = this.townService.GetAllTowns();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateHomeInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                var viewModel = new CreateHomeInputModel();
                viewModel.Categories = this.categoryService.GetAllCategories();
                viewModel.Towns = this.townService.GetAllTowns();
                return this.View(viewModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.propertyService.CreateAsync(inputModel, user.Id, $"{this.environment.WebRootPath}/localImages");

            return this.Redirect("/");
        }

    }
}
