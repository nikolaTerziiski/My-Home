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
    public class DetailsController : Controller
    {

        private readonly IPropertyService propertyService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;
        private readonly IFavouriteService favouriteService;

        public DetailsController(
            IPropertyService propertyService,
            UserManager<ApplicationUser> userManager,
            IFavouriteService favouriteService)
        {
            this.propertyService = propertyService;
            this.userManager = userManager;
            this.favouriteService = favouriteService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Property(int id)
        {
            var baseProperty = this.propertyService.TakeById<DetailsPropertyViewModel>(id);
            foreach (var image in baseProperty.Images)
            {
                var txt = $"/localImages/homes/{image.Id}.{image.Extension}";
                baseProperty.ImageURLs.Add(txt);
            }

            // Checking if this user created the home, so it can edit it
            var user = await this.userManager.GetUserAsync(this.User);
            bool isHisPost = user.Id == baseProperty.AddedByUser.Id;
            this.ViewData["flag"] = isHisPost;

            return this.View(baseProperty);
        }

        //Adding profile details
    }
}
