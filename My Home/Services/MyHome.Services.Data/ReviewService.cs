using MyHome.Data.Common.Repositories;
using MyHome.Data.Models;
using MyHome.Services.Mapping;
using MyHome.Web.ViewModels.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHome.Services.Data
{
    public class ReviewService : IReviewService
    {
        private readonly IDeletableEntityRepository<Review> reviewRepository;
        private readonly IDeletableEntityRepository<Home> homeRepository;

        public ReviewService(IDeletableEntityRepository<Review> reviewRepository,
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

            var reviewsss = this.homeRepository.All().Where(x => x.Id == inputModel.Id).FirstOrDefault().Reviews;

        }

        public ICollection<T> TakeById<T>(int homeId)
        {
            var somth = this.reviewRepository.All().Where(x => x.HomeId == homeId).To<T>().ToList();
            return somth;
        }
    }
}
