namespace MyHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using MyHome.Web.ViewModels.Reviews;

    public interface IReviewService
    {
        public Task AddReview(string userId, CreateReviewInputModel inputModel);

        public ICollection<T> TakeById<T>(int homeId);

        public ICollection<MyReviewInListViewModel> TakeForUser(string userId);
    }
}
