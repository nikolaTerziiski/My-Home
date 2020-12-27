using System;
using System.Collections.Generic;
using System.Text;

namespace MyHome.Web.ViewModels.Users
{
    public class UsersListViewModel
    {
        public UsersListViewModel()
        {
            this.Users = new HashSet<UserInListViewModel>();
        }

        public ICollection<UserInListViewModel> Users { get; set; }

        public string CurrentUserId { get; set; }
    }
}
