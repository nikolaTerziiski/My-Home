namespace MyHome.Web.ViewModels.Users
{
    using MyHome.Data.Models;
    using MyHome.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserInListViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int HomesCount { get; set; }

        public string Role { get; set; }
    }
}
