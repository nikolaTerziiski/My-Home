using MyHome.Data.Models;
using MyHome.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyHome.Web.ViewModels.Favourite
{
    public class FavouriteInList : IMapFrom<Home>
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public Image Image { get; set; }

        public HomeStatus HomeStatus { get; set; }

        public DateTime UploadDate { get; set; }

        public Category Category { get; set; }

        public float Price { get; set; }
    }
}
