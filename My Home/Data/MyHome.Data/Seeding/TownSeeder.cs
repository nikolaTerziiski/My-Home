using FindPlace.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHome.Data.Seeding
{
    public class TownSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Towns.Any())
            {
                return;
            }

            await dbContext.Towns.AddAsync(new Town { Name = "Sofia" });
            await dbContext.Towns.AddAsync(new Town { Name = "Plovdiv" });
            await dbContext.Towns.AddAsync(new Town { Name = "Varna" });
            await dbContext.Towns.AddAsync(new Town { Name = "Burgas" });
            await dbContext.Towns.AddAsync(new Town { Name = "Ruse" });
            await dbContext.Towns.AddAsync(new Town { Name = "Stara Zagora" });
            await dbContext.Towns.AddAsync(new Town { Name = "Pleven" });
            await dbContext.Towns.AddAsync(new Town { Name = "Pernik" });
            await dbContext.Towns.AddAsync(new Town { Name = "Montana" });
            await dbContext.Towns.AddAsync(new Town { Name = "Vidin" });
            await dbContext.Towns.AddAsync(new Town { Name = "Vratsa" });
            await dbContext.Towns.AddAsync(new Town { Name = "Silistra" });
            await dbContext.Towns.AddAsync(new Town { Name = "Razgrad" });
            await dbContext.Towns.AddAsync(new Town { Name = "Dobrich" });
            await dbContext.Towns.AddAsync(new Town { Name = "Shumen" });
            await dbContext.Towns.AddAsync(new Town { Name = "Targovishte" });
            await dbContext.Towns.AddAsync(new Town { Name = "Veliko Tarnovo" });
            await dbContext.Towns.AddAsync(new Town { Name = "Stara Zagora" });
            await dbContext.Towns.AddAsync(new Town { Name = "Sliven" });
            await dbContext.Towns.AddAsync(new Town { Name = "Yambol" });
            await dbContext.Towns.AddAsync(new Town { Name = "Haskovo" });
            await dbContext.Towns.AddAsync(new Town { Name = "Kardjali" });
            await dbContext.Towns.AddAsync(new Town { Name = "Smolyan" });
            await dbContext.Towns.AddAsync(new Town { Name = "Pazardzhik" });
            await dbContext.Towns.AddAsync(new Town { Name = "Blagoevgrad" });
            await dbContext.Towns.AddAsync(new Town { Name = "Lovech" });
            await dbContext.Towns.AddAsync(new Town { Name = "Gabrovo" });
            await dbContext.Towns.AddAsync(new Town { Name = "Kyustendil" });
        }
    }
}
