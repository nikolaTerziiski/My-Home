namespace MyHome.Services.Data
{
    using MyHome.Web.ViewModels.Property;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ICategoryService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllCategories();

        IEnumerable<SelectCategoryViewModel> GetSelectCategories();

        public bool DoesContainsCategory(string type);
    }
}
