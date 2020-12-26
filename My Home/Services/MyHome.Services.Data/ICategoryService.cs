namespace MyHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyHome.Web.ViewModels.Property;

    public interface ICategoryService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllCategories();

        IEnumerable<SelectCategoryViewModel> GetSelectCategories();

        public bool DoesContainsCategory(string type);
    }
}
