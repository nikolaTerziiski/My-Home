namespace MyHome.Web.ViewModels.Property
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SelectCategoriesViewModel
    {
        public SelectCategoriesViewModel()
        {
            this.Categories = new HashSet<SelectCategoryViewModel>();
        }

        public IEnumerable<SelectCategoryViewModel> Categories { get; set; }
    }
}
