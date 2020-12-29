namespace MyHome.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Moq;
    using MyHome.Data.Common.Repositories;
    using MyHome.Data.Models;
    using MyHome.Services.Mapping;
    using MyHome.Web.ViewModels.Categories;
    using MyHome.Web.ViewModels.Property;
    using Xunit;

    public class CategoryTests
    {
        [Fact]
        public async Task CategoryTesting()
        {
            AutoMapperConfig.RegisterMappings(typeof(CategoryInListViewModel).Assembly);
            AutoMapperConfig.RegisterMappings(typeof(SelectCategoryViewModel).Assembly);


            var list = new List<Category>();
            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.HardDelete(It.IsAny<Category>())).Callback((Category category) =>
            list.Remove(category));
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Category>())).Callback((Category category) => list.Add(category));

            var service = new CategoryService(mockRepo.Object);
            var category1 = new Category() { Name = "New" };
            var category2 = new Category() { Name = "New2" };
            var category3 = new Category() { Name = "New3" };

            //Checking for empty list
            Assert.Equal(0, service.GetAllCategories().Count());

            await mockRepo.Object.AddAsync(category1);
            await mockRepo.Object.AddAsync(category2);
            await mockRepo.Object.AddAsync(category3);

            list[0].Id = 1;
            list[1].Id = 2;
            list[2].Id = 3;

            //Tests for taking categories as key-value pairs for the creation of a Home
            var result = service.GetAllCategories();
            Assert.Equal(3, result.Count());
            Assert.Equal("1", result.First().Key);

            //Tests for taking cateogires for the select category page
            var resultSelect = service.GetSelectCategories();
            Assert.Equal(3, resultSelect.Count());
            Assert.Equal(1, resultSelect.First().Id);
            Assert.Equal("New3", resultSelect.Last().Name);

            //Check if category exists:
            Assert.Equal(false, service.DoesContainsCategory("wrong"));
            Assert.Equal(true, service.DoesContainsCategory("New"));

            //Tests for taking categories in list
            var category4 = new Category() { Name = "New4" };
            var category5 = new Category() { Name = "New5" };
            await mockRepo.Object.AddAsync(category4);
            await mockRepo.Object.AddAsync(category5);
            Assert.Equal(5, service.GetCategoriesForList().Count());
            Assert.Equal("New5", service.GetCategoriesForList().Last().Name);

        }
    }
}
