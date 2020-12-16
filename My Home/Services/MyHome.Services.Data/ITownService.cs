namespace MyHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyHome.Web.ViewModels.Property;

    public interface ITownService
    {
        public IEnumerable<KeyValuePair<string, string>> GetAllTowns();

        public ICollection<TownInTableViewModel> GetAllTownsForTable();

    }
}
