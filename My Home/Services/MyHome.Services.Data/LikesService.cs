namespace MyHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyHome.Data.Common.Repositories;
    using MyHome.Data.Models;

    public class LikesService : ILikesService
    {
        private readonly IDeletableEntityRepository<Like> likeRepository;

        public LikesService(IDeletableEntityRepository<Like> likeRepository)
        {
            this.likeRepository = likeRepository;
        }

        public async Task DecrementLikes(int id, string userId)
        {
            if (!this.DoesContainUser(id, userId))
            {
                throw new InvalidOperationException("The user didn't liked the post to dislike it!");
            }

            var likeEntity = this.likeRepository.All().Where(x => x.HomeId == id && x.AddedByUserId == userId).FirstOrDefault();

            this.likeRepository.HardDelete(likeEntity);
            await this.likeRepository.SaveChangesAsync();
        }

        public bool DoesContainUser(int propertyId, string userId)
        {
            var likes = this.likeRepository.All().Where(x => x.HomeId == propertyId).ToList();
            if (this.likeRepository.All().Any(x => x.HomeId == propertyId && x.AddedByUserId == userId))
            {
                return true;
            }

            return false;
        }

        public async Task IncrementLikeAsync(int propertyId, string userId)
        {


            if (this.DoesContainUser(propertyId, userId))
            {
                throw new InvalidOperationException("You cannot like second time!");
            }

            var like = new Like()
            {
                HomeId = propertyId,
                AddedByUserId = userId,
            };

            await this.likeRepository.AddAsync(like);
            await this.likeRepository.SaveChangesAsync();
        }
    }
}
