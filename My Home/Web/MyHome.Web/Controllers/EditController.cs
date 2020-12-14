using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyHome.Data.Models;
using MyHome.Services.Data;
using MyHome.Web.ViewModels.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHome.Web.Controllers
{
    public class EditController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly ITownService townService;
        private readonly IPropertyService propertyService;
        private readonly UserManager<ApplicationUser> userManager;

        public EditController(
            ICategoryService categoryService,
            ITownService townService,
            IPropertyService propertyService,
            UserManager<ApplicationUser> userManager)
        {
            this.categoryService = categoryService;
            this.townService = townService;
            this.propertyService = propertyService;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Property(int id)
        {
            var viewModel = this.propertyService.TakeById<EditHomeInputModel>(id);
            viewModel.Categories = this.categoryService.GetAllCategories();
            viewModel.Towns = this.townService.GetAllTowns();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Property(int id, EditHomeInputModel inputModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.propertyService.UpdateAsync(id, inputModel);
            return this.RedirectToAction("Property", "Details", new { id = id });
        }
    }
}
