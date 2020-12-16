using MyHome.Data.Models;
using MyHome.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyHome.Web.ViewModels.Property
{
    public class SelectCategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageURL { get; set; }
    }
}
