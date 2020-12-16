namespace MyHome.Web.ViewModels.Property
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyHome.Web.ViewModels.Shared;

    public class PropertiesListViewModel : PaginationViewModel
    {
        public PropertiesListViewModel()
        {
            this.Properties = new HashSet<PropertyInListViewModel>();
            this.Towns = new HashSet<TownInTableViewModel>();
        }

        public IEnumerable<PropertyInListViewModel> Properties { get; set; }

        public ICollection<TownInTableViewModel> Towns { get; set; }

        public string Header { get; set; }

    }
}
