namespace MyHome.Web.ViewModels.Reviews
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MyHome.Data.Models;
    using MyHome.Services.Mapping;

    public class CreateReviewInputModel : IMapFrom<Home>
    {
        [Required]
        [MinLength(20, ErrorMessage = "Your review is too short!")]
        [MaxLength(500, ErrorMessage = "Your review is too long")]
        public string Content { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }
    }
}
