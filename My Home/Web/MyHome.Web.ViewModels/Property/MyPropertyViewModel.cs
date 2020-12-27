namespace MyHome.Web.ViewModels.Property
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyHome.Data.Models;
    using MyHome.Services.Mapping;

    public class MyPropertyViewModel : IMapFrom<Home>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Likes { get; set; }

        public float Price { get; set; }

        public int Views { get; set; }

        public Category Category { get; set; }
    }
}
