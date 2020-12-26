namespace MyHome.Web.ViewModels.Favourite
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FavouriteList
    { 
        public FavouriteList()
        {
            this.List = new HashSet<FavouriteInList>();
        }
        public ICollection<FavouriteInList> List { get; set; }
    }
}
