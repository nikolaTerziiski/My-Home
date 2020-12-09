namespace MyHome.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyHome.Data;
    using MyHome.Services.Data;
    using MyHome.Web.ViewModels.Property;

    public class PropertyController : Controller
    {
        private readonly ICategoryService categoryService;

        public PropertyController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
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
            return this.View(viewModel);
        }


    }
}
