using System;
using System.Collections.Generic;
using System.Text;

namespace MyHome.Web.ViewModels.Shared
{
    public class PaginationViewModel
    {
        public int ItemsPerPage { get; set; }

        public int PageNumber { get; set; }

        public int PreviousPage => this.PageNumber - 1;
        public int NextPage => this.PageNumber + 1;


        public int AllHomesCount { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PagesCount => (int)Math.Ceiling((double)this.AllHomesCount / this.ItemsPerPage);

        public bool HasNextPage => this.PageNumber < this.PagesCount;
    }
}
