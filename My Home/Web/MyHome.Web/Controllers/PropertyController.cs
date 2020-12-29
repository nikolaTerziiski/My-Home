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
    using MyHome.Common;
    using MyHome.Data;
    using MyHome.Data.Models;
    using MyHome.Services.Data;
    using MyHome.Web.ViewModels.Property;
    using MyHome.Web.ViewModels.Reviews;

    public class PropertyController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly ITownService townService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPropertyService propertyService;
        private readonly IWebHostEnvironment environment;
        private readonly IFavouriteService favouriteService;
        private readonly IReviewService reviewService;
        private readonly ILikesService likesService;
        private readonly string[] roles = { "User", "Administrator" };

        public PropertyController(ICategoryService categoryService,
                                  ITownService townService,
                                  UserManager<ApplicationUser> userManager,
                                  IPropertyService propertyService,
                                  IWebHostEnvironment environment,
                                  IFavouriteService favouriteService,
                                  IReviewService reviewService,
                                  ILikesService likesService)
        {
            this.categoryService = categoryService;
            this.townService = townService;
            this.userManager = userManager;
            this.propertyService = propertyService;
            this.environment = environment;
            this.favouriteService = favouriteService;
            this.reviewService = reviewService;
            this.likesService = likesService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        [Authorize]
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

            //Validation for SQL injection
            inputModel.Description = HtmlUtilities.EncodeThisPropertyForMe(inputModel.Description);
            inputModel.Adress = HtmlUtilities.EncodeThisPropertyForMe(inputModel.Adress);
            inputModel.Title = HtmlUtilities.EncodeThisPropertyForMe(inputModel.Title);


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

            return this.View(viewModel);
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

            var viewModel = this.propertyService.TakeOneById<DeleteHomeViewModel>(id);
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

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var baseProperty = this.propertyService.TakeOneById<DetailsPropertyViewModel>(id);
            foreach (var image in baseProperty.Images)
            {
                var txt = $"/localImages/homes/{image.Id}.{image.Extension}";
                baseProperty.ImageURLs.Add(txt);
            }

            // Checking if this user created the home, so it can edit it
            var user = await this.userManager.GetUserAsync(this.User);

            bool isHisPost = false;
            // Checking if the user is logged in, so he cannot have chance to see EditPost
            if (user != null)
            {
              baseProperty.IsItLiked = this.likesService.DoesContainUser(id, user.Id);
              isHisPost = user.Id == baseProperty.AddedByUser.Id;
              baseProperty.IsItFavourite = this.favouriteService.DoesContain(id, user.Id);
            }

            this.ViewData["flag"] = isHisPost;

            baseProperty.Reviews = this.reviewService.TakeById<PropertyDetailsReviewViewModel>(id);

            //Increment like after taking the info, so the current doesnt count
            await this.propertyService.IncrementView(id);
            return this.View(baseProperty);
        }

        [HttpGet]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int id)
        {
            var viewModel = this.propertyService.TakeOneById<EditHomeInputModel>(id);
            viewModel.Categories = this.categoryService.GetAllCategories();
            viewModel.Towns = this.townService.GetAllTowns();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditHomeInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                var viewModel = this.propertyService.TakeOneById<EditHomeInputModel>(id);
                viewModel.Categories = this.categoryService.GetAllCategories();
                viewModel.Towns = this.townService.GetAllTowns();

                return this.View(viewModel);
            }

            //Validation for SQL injection
            inputModel.Description = HtmlUtilities.EncodeThisPropertyForMe(inputModel.Description);
            inputModel.Adress = HtmlUtilities.EncodeThisPropertyForMe(inputModel.Adress);
            inputModel.Title = HtmlUtilities.EncodeThisPropertyForMe(inputModel.Title);

            var user = await this.userManager.GetUserAsync(this.User);
            await this.propertyService.UpdateAsync(id, inputModel);
            return this.RedirectToAction("Details", "Property", new { id = id });
        }

        [HttpGet]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult My()
        {

            var userId = this.userManager.GetUserId(this.User);
            var viewModel = new MyPropertiesViewModel() 
            {
                Properties = this.propertyService.TakeAllByUserId<MyPropertyViewModel>(userId),
            };

            return this.View(viewModel);
        }
    }
}
