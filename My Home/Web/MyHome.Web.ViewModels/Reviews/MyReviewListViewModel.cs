namespace MyHome.Web.ViewModels.Reviews
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MyReviewListViewModel
    {
        public MyReviewListViewModel()
        {
            this.Reviews = new HashSet<MyReviewInListViewModel>();
        }

        public ICollection<MyReviewInListViewModel> Reviews { get; set; }
    }
}
