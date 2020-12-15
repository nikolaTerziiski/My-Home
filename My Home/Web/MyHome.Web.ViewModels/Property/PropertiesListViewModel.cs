namespace MyHome.Web.ViewModels.Property
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PropertiesListViewModel
    {
        public int ItemsPerPage { get; set; }

        public int PageNumber { get; set; }

        public IEnumerable<PropertyInListViewModel> Properties { get; set; }
    }
}
