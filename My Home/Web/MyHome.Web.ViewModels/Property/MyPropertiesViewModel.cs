namespace MyHome.Web.ViewModels.Property
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MyPropertiesViewModel
    {
        public MyPropertiesViewModel()
        {
            this.Properties = new HashSet<MyPropertyViewModel>();
        }

        public ICollection<MyPropertyViewModel> Properties { get; set; }
    }
}
