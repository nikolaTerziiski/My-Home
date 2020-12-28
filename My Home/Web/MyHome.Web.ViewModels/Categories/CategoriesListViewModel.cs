namespace MyHome.Web.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CategoriesListViewModel
    {
        public CategoriesListViewModel()
        {
            this.Categories = new HashSet<CategoryInListViewModel>();
        }

        public ICollection<CategoryInListViewModel> Categories { get; set; }
    }
}
