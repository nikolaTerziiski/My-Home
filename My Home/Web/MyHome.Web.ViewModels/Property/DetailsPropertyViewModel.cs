using AutoMapper;
using FindPlace.Data.Models;
using MyHome.Data.Models;
using MyHome.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyHome.Web.ViewModels.Property
{
    public class DetailsPropertyViewModel : IMapFrom<Home>
    {
        
        public DetailsPropertyViewModel()
        {
            this.Images = new HashSet<Image>();
            this.UploadDate = DateTime.UtcNow;
            this.ImageURLs = new List<string>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public int YearOfProduction { get; set; }

        public string Adrress { get; set; }

        public DateTime UploadDate { get; set; }

        public int Rooms { get; set; }

        public int Sqauring { get; set; }

        public Town Town { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public virtual Category Category { get; set; }

        public ICollection<Image> Images { get; set; }

        public ICollection<string> ImageURLs { get; set; }
    }
}
