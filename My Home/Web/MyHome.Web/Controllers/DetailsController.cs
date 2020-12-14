using Microsoft.AspNetCore.Mvc;
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

        public DetailsController(IPropertyService propertyService)
        {
            this.propertyService = propertyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Property(int id)
        {
            var baseProperty = this.propertyService.TakeById<DetailsPropertyViewModel>(id);

            foreach (var image in baseProperty.Images)
            {
                var txt = $"/localImages/homes/{image.Id}.{image.Extension}";
                baseProperty.ImageURLs.Add(txt);
            }
            return this.View(baseProperty);
        }

    }
}
