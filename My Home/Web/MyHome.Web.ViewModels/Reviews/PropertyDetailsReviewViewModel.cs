using MyHome.Data.Models;
using MyHome.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyHome.Web.ViewModels.Reviews
{
    public class PropertyDetailsReviewViewModel : IMapFrom<Review>
    {
        public ApplicationUser AddedByUser { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }
    }
}
