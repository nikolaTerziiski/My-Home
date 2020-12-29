namespace MyHome.Web.ViewModels.Reviews
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyHome.Data.Models;
    using MyHome.Services.Mapping;

    public class PropertyDetailsReviewViewModel : IMapFrom<Review>
    {
        public ApplicationUser AddedByUser { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }
    }
}
