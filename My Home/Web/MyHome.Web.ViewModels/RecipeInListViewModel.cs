using MyHome.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyHome.Web.ViewModels
{
    public class RecipeInListViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Image Image { get; set; }

        public string Type { get; set; }

        public float Price { get; set; }
    }
}
