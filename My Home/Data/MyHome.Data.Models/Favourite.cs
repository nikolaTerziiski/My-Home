using MyHome.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyHome.Data.Models
{
    public class Favourite : BaseDeletableModel<int>
    {
        public Favourite()
        {
            this.FavouriteHomes = new HashSet<FavouriteHome>();
        }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<FavouriteHome> FavouriteHomes { get; set; }
    }
}
