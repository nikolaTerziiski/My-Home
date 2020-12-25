namespace MyHome.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyHome.Data.Common.Models;

    public class FavouriteHome : BaseModel<int>
    {
        public int FavouriteId { get; set; }

        public Favourite Favourite { get; set; }

        public int HomeId { get; set; }

        public Home Home { get; set; }
    }
}
