namespace MyHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ILikesService
    {
        public Task IncrementLikeAsync(int propertyId, string userId);

        public bool DoesContainUser(int id1, string id2);

        public Task DecrementLikes(int id, string userId);
    }
}
