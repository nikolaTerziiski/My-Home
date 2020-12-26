namespace MyHome.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyHome.Data.Common.Models;
    using MyHome.Data.Models;

    public class Home : BaseDeletableModel<int>
    {
        public Home()
        {
            this.Images = new HashSet<Image>();
            this.UploadDate = DateTime.UtcNow;
            this.FavouriteHomes = new HashSet<FavouriteHome>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public int YearOfProduction { get; set; }

        public float Price { get; set; }

        public string Adress { get; set; }

        public DateTime UploadDate { get; set; }

        public int Rooms { get; set; }

        public HomeStatus Status { get; set; }

        public int Sqauring { get; set; }

        public int Likes { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public int CategoryId { get; set; }

        public ICollection<FavouriteHome> FavouriteHomes { get; set; }

        public virtual Category Category { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}
