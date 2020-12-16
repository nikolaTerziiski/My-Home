namespace MyHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using MyHome.Web.ViewModels.Property;

    public interface IPropertyService
    {
        public Task CreateAsync(CreateHomeInputModel inputModel, string userId, string path);

        public T TakeById<T>(int id);

        public Task UpdateAsync(int id,  EditHomeInputModel inputModel);

        public IEnumerable<PropertyInListViewModel> GetAll(int id, int listCount);

        public int GetCount();
    }
}
