namespace MyHome.Web.ViewModels.Property
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyHome.Data.Models;
    using MyHome.Services.Mapping;

    public class TownInTableViewModel : IMapFrom<Town>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Home> Homes { get; set; }
    }
}
