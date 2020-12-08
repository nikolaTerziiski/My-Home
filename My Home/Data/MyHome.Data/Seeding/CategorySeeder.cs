namespace MyHome.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FindPlace.Data.Models;

    public class CategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category { Name = "Apartment" });
            await dbContext.Categories.AddAsync(new Category { Name = "House" });
            await dbContext.Categories.AddAsync(new Category { Name = "Rent" });
        }
    }
}
