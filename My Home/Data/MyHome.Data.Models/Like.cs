namespace MyHome.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyHome.Data.Common.Models;

    public class Like : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public int HomeId { get; set; }

        public Home Home { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}
