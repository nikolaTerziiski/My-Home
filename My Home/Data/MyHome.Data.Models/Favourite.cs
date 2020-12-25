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
            this.Homes = new HashSet<Home>();
        }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<Home> Homes { get; set; }
    }
}
