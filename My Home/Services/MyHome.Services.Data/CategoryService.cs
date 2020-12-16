namespace MyHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MyHome.Data.Models;
    using MyHome.Data.Common.Repositories;
    using MyHome.Web.ViewModels.Property;
    using MyHome.Services.Mapping;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public bool DoesContainsCategory(string type)
        {
            if (!this.categoryRepository.AllAsNoTracking().Select(x => x.Name).Contains(type))
            {
                return false;
            }
            return true;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllCategories()
        {
            return this.categoryRepository.AllAsNoTracking().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name)).ToList();
        }

        public IEnumerable<SelectCategoryViewModel> GetSelectCategories()
        {
            return this.categoryRepository.AllAsNoTracking().To<SelectCategoryViewModel>();
        }
    }
}
