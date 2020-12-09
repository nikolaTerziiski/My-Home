namespace MyHome.Web.ViewModels.Property
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class CreateHomeInputModel
    {

        [Required]
        [MinLength(4)]
        public string Title { get; set; }

        [Required]
        [MinLength(20)]
        public string Description { get; set; }

        [Range(1900,2020)]
        public int YearOfProduction { get; set; }

        [Required]
        public string Adrress { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        [Required]
        public int Rooms { get; set; }

        [Required]
        public int Sqauring { get; set; }

        [Display(Name = "Please enter the town, where the property is.")]
        public int TownId { get; set; }

        public IEnumerable<KeyValuePair<string,string>> Categories { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }
    }
}
