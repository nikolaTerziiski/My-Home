namespace MyHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyHome.Data.Models;
    using MyHome.Data.Common.Repositories;
    using MyHome.Services.Mapping;
    using MyHome.Web.ViewModels.Property;

    public class PropertyService : IPropertyService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Home> propertyRepository;

        public PropertyService(IDeletableEntityRepository<Home> propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public async Task CreateAsync(CreateHomeInputModel inputModel, string userId, string path)
        {

            var home = new Home
            {
                Title = inputModel.Title,
                Description = inputModel.Description,
                Adress = inputModel.Adress,
                Rooms = inputModel.Rooms,
                Sqauring = inputModel.Sqauring,
                YearOfProduction = inputModel.YearOfProduction,
                CategoryId = inputModel.CategoryId,
                TownId = inputModel.TownId,
                AddedByUserId = userId,
                Status = inputModel.HomeStatus,
                Price = inputModel.Price,
            };

            Directory.CreateDirectory($"{path}/homes");
            foreach (var picture in inputModel.Images)
            {
                var extension = Path.GetExtension(picture.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => x == extension))
                {
                    throw new Exception("Invalid picture extenstion!");
                }

                var dbImage = new Image
                {
                    AddedByUserId = userId,
                    Extension = extension,
                };
                home.Images.Add(dbImage);

                var realPath = $"{path}/homes/{dbImage.Id}.{extension}";
                using Stream fileStream = new FileStream(realPath, FileMode.Create);
                await picture.CopyToAsync(fileStream);
            }


            await this.propertyRepository.AddAsync(home);
            await this.propertyRepository.SaveChangesAsync();
        }

        public async Task DeletePropertyAsync(int id)
        {
            var result = this.propertyRepository.All().Where(x => x.Id == id).FirstOrDefault();
            this.propertyRepository.HardDelete(result);
            await this.propertyRepository.SaveChangesAsync();
        }

        public bool DoesContainProperty(int id)
        {
            if (!this.propertyRepository.AllAsNoTracking().Any(x => x.Id == id))
            {
                return false;
            }

            return true;
        }

        public IEnumerable<PropertyInListViewModel> GetAll(int id, int listCount)
        {
            if (id < 0)
            {
                throw new InvalidOperationException("Page must be positive number!");
            }
            var result = this.propertyRepository.AllAsNoTracking().OrderByDescending(x => x.Id).Skip((id - 1) * listCount).Take(listCount)
                .To<PropertyInListViewModel>().ToList();

            return result;
        }

        public IEnumerable<PropertyInListViewModel> GetAllWithCategory(string type, int id, int listCount)
        {
            if (id < 0)
            {
                throw new InvalidOperationException("Page must be positive number!");
            }
            var result = this.propertyRepository.AllAsNoTracking().Where(x => x.Category.Name.ToLower() == type.ToLower()).OrderByDescending(x => x.Id).Skip((id - 1) * listCount).Take(listCount)
                .To<PropertyInListViewModel>().ToList();

            return result;
        }

        public int GetAllWithCategoryCount(string type)
        {
            return this.propertyRepository.AllAsNoTracking().Where(x => x.Category.Name.ToLower() == type.ToLower()).Count();
        }

        public int GetCount()
        {
            return this.propertyRepository.AllAsNoTracking().Count();
        }

        public T TakeOneById<T>(int id)
        {
            if (id <= 0)
            {
                throw new InvalidOperationException("Invalid id!");
            }

            if (!this.propertyRepository.AllAsNoTracking().Any(x => x.Id == id))
            {
                throw new InvalidOperationException("The property with that id does not exists!");
            }

            var propertyFromData = this.propertyRepository.All()
                .Where(x => x.Id == id).To<T>().FirstOrDefault();

            return propertyFromData;
        }

        public async Task UpdateAsync(int id, EditHomeInputModel inputModel)
        {
            if (!this.DoesContainProperty(id))
            {
                throw new InvalidOperationException("Invalid id!");
            }

            var home = this.propertyRepository.All().First(x => x.Id == id);
            home.Title = inputModel.Title;
            home.Description = inputModel.Description;
            home.YearOfProduction = inputModel.YearOfProduction;
            home.CategoryId = inputModel.CategoryId;
            home.Adress = inputModel.Adress;
            home.Rooms = inputModel.Rooms;
            home.Sqauring = inputModel.Sqauring;
            home.TownId = inputModel.TownId;

            await this.propertyRepository.SaveChangesAsync();
        }

        public int GetAllByUserCount(string userId)
        {
            return this.propertyRepository.AllAsNoTracking().Where(x => x.AddedByUserId == userId).ToList().Count();
        }

        public async Task IncrementView(int id)
        {
            var home = this.propertyRepository.All().Where(x => x.Id == id).FirstOrDefault();
            home.Views += 1;

            await this.propertyRepository.SaveChangesAsync();
        }

        public ICollection<T> TakeAllByUserId<T>(string userId)
        {
            var homes = this.propertyRepository.AllAsNoTracking().Where(x => x.AddedByUserId == userId).To<T>().ToList();

            return homes;
        }
    }
}
