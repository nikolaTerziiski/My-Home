using System;
using System.Collections.Generic;
using System.Text;

namespace MyHome.Services.Data
{
    public interface ITownService
    {
        public IEnumerable<KeyValuePair<string, string>> GetAllTowns();
    }
}
