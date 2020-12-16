namespace MyHome.Web.ViewModels.Property
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MyHome.Data.Models;
    using MyHome.Services.Mapping;

    public class PropertyInListViewModel : IMapFrom<Home>
    {
        public PropertyInListViewModel()
        {
            this.Images = new HashSet<Image>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<Image> Images { get; set; }

        public string Type { get; set; }

        public float Price { get; set; }

        public string ImageFolderURL { get; set; }

        public DateTime UploadDate { get; set; }
    }
}
