using MyHome.Data.Models;
using MyHome.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyHome.Web.ViewModels.Property
{
    public class DeleteHomeViewModel : IMapFrom<Home>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int YearOfProduction { get; set; }

        public string Adress { get; set; }

        public float Price { get; set; }

        public int Rooms { get; set; }

        public int Sqauring { get; set; }
    }
}
