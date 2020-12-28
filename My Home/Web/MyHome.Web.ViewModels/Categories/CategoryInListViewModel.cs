namespace MyHome.Web.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyHome.Data.Models;
    using MyHome.Services.Mapping;

    public class CategoryInListViewModel : IMapFrom<Home>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
