namespace MyHome.Web.ViewModels.Property
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public abstract class BasePropertyInputModel
    {
        [Required]
        [MinLength(4)]
        public string Title { get; set; }

        [Required]
        [MinLength(20)]
        public string Description { get; set; }

        [Range(1900, 2020)]
        public int YearOfProduction { get; set; }

        [Required]
        [MinLength(5)]
        public string Adress { get; set; }

        public float Price { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Range(1, 5)]
        public int Rooms { get; set; }

        [Required]
        [Range(30, 400)]
        public int Sqauring { get; set; }

        public int TownId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Towns { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Categories { get; set; }
    }
}
