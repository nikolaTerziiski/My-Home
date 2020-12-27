namespace MyHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MyHome.Web.ViewModels.Favourite;

    public interface IFavouriteService
    {
        public Task AddAsync(int id, string userId);

        public bool DoesContain(int id, string userId);

        public ICollection<FavouriteInList> GetAllById(string id);

        public Task RemoveById(int id, string userId);
    }
}
