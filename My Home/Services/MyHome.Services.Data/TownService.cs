namespace MyHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using FindPlace.Data.Models;
    using MyHome.Data.Common.Repositories;

    public class TownService : ITownService
    {
        private readonly IDeletableEntityRepository<Town> townRepository;

        public TownService(IDeletableEntityRepository<Town> townRepository)
        {
            this.townRepository = townRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllTowns()
        {
            return this.townRepository.AllAsNoTracking().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name)).ToList();
        }
    }
}
