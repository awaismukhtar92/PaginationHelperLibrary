using System.Collections.Generic;

namespace PaginationHelper.Pagination
{

    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Dictionary<string, string> Filter { get; set; }

        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PaginationFilter(int pageNumber, int pageSize, Dictionary<string, string> filter =null)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 30 ? 30 : pageSize;
            this.Filter = filter;
        }
    }
}
