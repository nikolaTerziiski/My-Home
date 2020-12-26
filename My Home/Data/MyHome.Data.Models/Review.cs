namespace MyHome.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyHome.Data.Common.Models;

    public class Review : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        public int Likes { get; set; }

        public int AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public int HomeId { get; set; }

        public Home Home { get; set; }
    }
}
