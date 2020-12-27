namespace MyHome.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using MyHome.Common;
    using MyHome.Services.Data;
    using MyHome.Web.ViewModels.Categories;

    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IWebHostEnvironment environment;

        public CategoriesController(ICategoryService categoryService,
                                    IWebHostEnvironment environment)
        {
            this.categoryService = categoryService;
            this.environment = environment;
        }
        
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Add()
        {
            var viewModel = new CreateCategoryInputModel();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Add(CreateCategoryInputModel inputModel)
        {
            await this.categoryService.CreateAsync(inputModel, $"{this.environment.WebRootPath}/localImages");

            return this.RedirectToAction("Index", "Home");
        }
    }
}
