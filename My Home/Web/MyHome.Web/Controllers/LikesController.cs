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

    public class LikesController : Controller
    {
        private readonly IPropertyService propertyService;
        private readonly ILikesService likesService;
        private readonly UserManager<ApplicationUser> userManager;

        public LikesController(
                               IPropertyService propertyService,
                               ILikesService likesService,
                               UserManager<ApplicationUser> userManager)
        {
            this.propertyService = propertyService;
            this.likesService = likesService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index(int id)
        {
            if (!this.propertyService.DoesContainProperty(id))
            {
                return this.NotFound();
            }
            var userId = this.userManager.GetUserId(this.User);
            await this.likesService.IncrementLikeAsync(id, userId);
            return this.RedirectToAction("Details", "Property", new { id = id });
        }

        [Authorize]
        public async Task<IActionResult> Decrement(int id)
        {
            if (!this.propertyService.DoesContainProperty(id))
            {
                return this.NotFound();
            }
            var userId = this.userManager.GetUserId(this.User);
            await this.likesService.DecrementLikes(id, userId);
            return this.RedirectToAction("Details", "Property", new { id = id });
        }
    }
}
