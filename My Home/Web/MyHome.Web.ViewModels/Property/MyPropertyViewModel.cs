namespace MyHome.Web.ViewModels.Property
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MyHome.Data.Models;
    using MyHome.Services.Mapping;

    public class MyPropertyViewModel : IMapFrom<Home>
    {
        public MyPropertyViewModel()
        {
            this.Likes = new HashSet<Like>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public int LikesCount => this.Likes.Count();

        public float Price { get; set; }

        public int Views { get; set; }

        public Category Category { get; set; }

        public ICollection<Like> Likes { get; set; }
    }
}
