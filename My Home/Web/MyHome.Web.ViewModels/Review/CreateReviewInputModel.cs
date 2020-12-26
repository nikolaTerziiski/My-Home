namespace MyHome.Web.ViewModels.Review
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateReviewInputModel
    {
        [Required]
        [MinLength(20, ErrorMessage = "Your review is too short!")]
        [MaxLength(500, ErrorMessage = "Your review is too long")]
        public string Content { get; set; }

        public int MyProperty { get; set; }
    }
}
