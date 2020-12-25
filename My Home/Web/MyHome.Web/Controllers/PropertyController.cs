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
        [Authorize(Roles = "Administrator")]
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

        [HttpGet]
        public IActionResult AllHomes(int id)
        {
            if (id <= 0)
            {
                this.RedirectToAction("NotFound");
            }

            int itemsPerPage = 9;

            var viewModel = new PropertiesListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                Properties = this.propertyService.GetAll(id, itemsPerPage),
                AllHomesCount = this.propertyService.GetCount() / itemsPerPage,
                Towns = this.townService.GetAllTownsForTable(),
                Title = "All Offers",
            };

            foreach (var picture in viewModel.Properties)
            {
                var txt = $"/localImages/homes/{picture.Images.First().Id}.{picture.Images.First().Extension}";

                picture.ImageFolderURL = txt;
            }

            return this.View(viewModel);
        }
    
        [HttpGet]
        public IActionResult Select()
        {
            var viewModel = new SelectCategoriesViewModel
            {
                Categories = this.categoryService.GetSelectCategories(),
            };
            foreach (var category in viewModel.Categories)
            {
                string txt = $"/localImages/categorySearch/{category.Id}.jpg";
                category.ImageURL = txt;
            }

            return this.View();
        }

        [HttpGet]
        public IActionResult Category(int id, string type)
        {
            if (id <= 0 || !this.categoryService.DoesContainsCategory(type))
            {
                this.RedirectToAction("NotFound");
            }

            int itemsPerPage = 9;

            var viewModel = new PropertiesListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                Properties = this.propertyService.GetAllWithCategory(type, id, itemsPerPage),
                AllHomesCount = this.propertyService.GetAllWithCategoryCount(type) / itemsPerPage,
                Title = type,
            };
            foreach (var picture in viewModel.Properties)
            {
                var txt = $"/localImages/homes/{picture.Images.First().Id}.{picture.Images.First().Extension}";

                picture.ImageFolderURL = txt;
            }

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
        {
            if (!this.propertyService.DoesContainProperty(id))
            {
                return this.RedirectToAction("NotFound");
            }

            var viewModel = this.propertyService.TakeById<DeleteHomeViewModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> Delete(int id, DeleteHomeViewModel viewModel)
        {
            if (!this.propertyService.DoesContainProperty(id))
            {
                return this.RedirectToAction("NotFound");
            }

            await this.propertyService.DeletePropertyAsync(id);

            return this.RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Increment(int id)
        {
            await this.propertyService.IncrementLikeAsync(id);

            return this.RedirectToAction("Property", "Details", new { id = id });
        }
    }
}
