namespace MyHome.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyHome.Data.Common.Models;
    using MyHome.Data.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Extension { get; set; }

        public int PropertyId { get; set; }

        public virtual Home Property { get; set; }

        public string RemoteImageUrl { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}
