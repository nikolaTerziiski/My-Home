namespace MyHome.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyHome.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Homes = new HashSet<Home>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Home> Homes { get; set; }
    }
}
