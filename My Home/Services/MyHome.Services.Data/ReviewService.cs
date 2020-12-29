namespace MyHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyHome.Data.Common.Repositories;
    using MyHome.Data.Models;
    using MyHome.Services.Mapping;
    using MyHome.Web.ViewModels.Reviews;

    public class ReviewService : IReviewService
    {
        private readonly IDeletableEntityRepository<Review> reviewRepository;
        private readonly IDeletableEntityRepository<Home> homeRepository;

        public ReviewService(
                             IDeletableEntityRepository<Review> reviewRepository,
                             IDeletableEntityRepository<Home> homeRepository)
        {
            this.reviewRepository = reviewRepository;
            this.homeRepository = homeRepository;
        }

        public async Task AddReview(string userId, CreateReviewInputModel inputModel)
        {
            var review = new Review
            {
                AddedByUserId = userId,
                Content = inputModel.Content,
                HomeId = inputModel.Id,
                Likes = 0,
                CreatedOn = DateTime.UtcNow,
            };

            var home = this.homeRepository.All().Where(x => x.Id == inputModel.Id).FirstOrDefault();
            home.Reviews.Add(review);

            await this.homeRepository.SaveChangesAsync();
        }

        public async Task Delete(int id, string userId, bool isAdministrator)
        {
            var review = this.reviewRepository.AllAsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            if (review == null)
            {
                throw new InvalidOperationException("Invalid review id");
            }
            if (isAdministrator)
            {
                this.reviewRepository.HardDelete(review);
                await this.reviewRepository.SaveChangesAsync();
                return;
            }
            else if (review.AddedByUserId == userId)
            {
                this.reviewRepository.HardDelete(review);
                await this.reviewRepository.SaveChangesAsync();
                return;
            }

            throw new InvalidOperationException("You can't delete other people reviews!");
        }

        public ICollection<T> TakeById<T>(int homeId)
        {
            var result = this.reviewRepository.All().Where(x => x.HomeId == homeId).To<T>().ToList();
            return result;
        }

        public ICollection<MyReviewInListViewModel> TakeForUser(string userId)
        {
            return this.reviewRepository.AllAsNoTracking().Where(x => x.AddedByUserId == userId).To<MyReviewInListViewModel>().ToList();
        }
    }
}
