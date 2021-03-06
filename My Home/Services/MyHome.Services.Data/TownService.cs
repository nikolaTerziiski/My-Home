﻿namespace MyHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MyHome.Data.Models;
    using MyHome.Data.Common.Repositories;
    using MyHome.Web.ViewModels.Property;
    using MyHome.Services.Mapping;

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

        public ICollection<TownInTableViewModel> GetAllTownsForTable()
        {
            var test = this.townRepository.All().ToList();
            return this.townRepository.All().OrderByDescending(e => e.Homes.Count()).Take(8).To<TownInTableViewModel>().ToList();
        }
    }
}
