namespace FindPlace.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Town
    {
        public Town()
        {
            this.Homes = new HashSet<Home>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Home> Homes { get; set; }
    }
}
