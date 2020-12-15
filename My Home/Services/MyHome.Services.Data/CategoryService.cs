namespace MyHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MyHome.Data.Models;
    using MyHome.Data.Common.Repositories;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllCategories()
        {
            return this.categoryRepository.AllAsNoTracking().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name)).ToList();
        }
    }
}
