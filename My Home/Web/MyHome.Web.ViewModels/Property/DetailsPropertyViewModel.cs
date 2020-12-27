namespace MyHome.Web.ViewModels.Property
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using MyHome.Data.Models;
    using MyHome.Services.Mapping;
    using MyHome.Web.ViewModels.Reviews;

    public class DetailsPropertyViewModel : IMapFrom<Home>
    {
        public DetailsPropertyViewModel()
        {
            this.Images = new HashSet<Image>();
            this.UploadDate = DateTime.UtcNow;
            this.ImageURLs = new List<string>();
            this.Reviews = new HashSet<PropertyDetailsReviewViewModel>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public int YearOfProduction { get; set; }

        public string Adress { get; set; }

        public DateTime UploadDate { get; set; }

        public int Rooms { get; set; }

        public int Sqauring { get; set; }

        public Town Town { get; set; }

        public int Likes { get; set; }

        public int Views { get; set; }

        public bool IsItFavourite { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public virtual Category Category { get; set; }

        public ICollection<Image> Images { get; set; }

        public ICollection<string> ImageURLs { get; set; }

        public ICollection<PropertyDetailsReviewViewModel> Reviews { get; set; }

    }
}
