namespace MyHome.Web.ViewModels.Property
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class CreateHomeInputModel : BasePropertyInputModel
    {
        public CreateHomeInputModel()
        {
            this.Images = new HashSet<IFormFile>();
        }
        [Required]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
