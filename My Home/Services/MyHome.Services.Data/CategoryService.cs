namespace MyHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyHome.Data.Common.Repositories;
    using MyHome.Data.Models;
    using MyHome.Services.Mapping;
    using MyHome.Web.ViewModels.Categories;
    using MyHome.Web.ViewModels.Property;

    public class CategoryService : ICategoryService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task CreateAsync(CreateCategoryInputModel inputModel, string path)
        {
            if (this.categoryRepository.AllAsNoTracking().Any(x => x.Name.ToLower() == inputModel.Name.ToLower()))
            {
                return;
            }

            var newCategory = new Category
            {
                Name = inputModel.Name,
            };

            await this.categoryRepository.AddAsync(newCategory);
            await this.categoryRepository.SaveChangesAsync();
            Directory.CreateDirectory($"{path}/categorySearch");

            var extension = Path.GetExtension(inputModel.Image.FileName).TrimStart('.');

            if (!this.allowedExtensions.Any(x => x == extension))
            {
                throw new Exception("Invalid picture extenstion!");
            }

            var realPath = $"{path}/categorySearch/{newCategory.Id}.{extension}";
            using Stream fileStream = new FileStream(realPath, FileMode.Create);
            await inputModel.Image.CopyToAsync(fileStream);

        }

        public bool DoesContainsCategory(string type)
        {
            if (!this.categoryRepository.AllAsNoTracking().Any(x => x.Name.ToLower() == type.ToLower()))
            {
                return false;
            }
            return true;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllCategories()
        {
            return this.categoryRepository.AllAsNoTracking().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name)).ToList();
        }

        public ICollection<CategoryInListViewModel> GetCategoriesForList()
        {
            var categories = this.categoryRepository.AllAsNoTracking().Select(x => new CategoryInListViewModel()
            {
                Id = x.Id,
                CreatedOn = x.CreatedOn,
                Name = x.Name,
                Count = x.Homes.Count,
            }).ToList();

            return categories;
        }

        public IEnumerable<SelectCategoryViewModel> GetSelectCategories()
        {
            return this.categoryRepository.AllAsNoTracking().To<SelectCategoryViewModel>().ToList();
        }
    }
}
