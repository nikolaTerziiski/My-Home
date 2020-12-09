namespace MyHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ICategoryService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllCategories();
    }
}
