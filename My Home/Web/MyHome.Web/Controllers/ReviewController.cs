namespace MyHome.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHome.Data.Models;
    using MyHome.Services.Data;
    using MyHome.Web.ViewModels.Reviews;

    public class ReviewController : Controller
    {
        private readonly IPropertyService propertyService;
        private readonly IReviewService reviewService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewController(IPropertyService propertyService,
                                IReviewService reviewService,
                                UserManager<ApplicationUser> userManager)
        {
            this.propertyService = propertyService;
            this.reviewService = reviewService;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create(int id)
        {
            if (!this.propertyService.DoesContainProperty(id))
            {
                return this.NotFound();
            }

            var viewModel = this.propertyService.TakeOneById<CreateReviewInputModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateReviewInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                var viewModel = this.propertyService.TakeOneById<CreateReviewInputModel>(inputModel.Id);
                return this.View(viewModel);
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.reviewService.AddReview(userId, inputModel);

            return this.RedirectToAction("Details", "Property", new { id = inputModel.Id });
        }
    }
}
