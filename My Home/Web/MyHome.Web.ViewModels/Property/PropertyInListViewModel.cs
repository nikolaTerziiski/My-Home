namespace MyHome.Web.ViewModels.Property
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyHome.Data.Models;
    using MyHome.Services.Mapping;

    public class PropertyInListViewModel : IMapFrom<Home>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Image Image { get; set; }

        public string Type { get; set; }

        public float Price { get; set; }
    }
}
