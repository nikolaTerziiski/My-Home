namespace MyHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FindPlace.Data.Models;
    using MyHome.Data.Common.Repositories;
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
                Adrress = inputModel.Adress,
                Rooms = inputModel.Rooms,
                Sqauring = inputModel.Sqauring,
                YearOfProduction = inputModel.YearOfProduction,
                CategoryId = inputModel.Category,
                TownId = inputModel.Town,
                AddedByUserId = userId,
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
    }
}
