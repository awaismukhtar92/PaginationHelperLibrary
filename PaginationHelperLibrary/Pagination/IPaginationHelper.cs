using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace PaginationHelper.Pagination
{
    public interface IPaginationHelper
    {
        public PagedResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData , int totalRecords, string route, HttpRequest accessor, PaginationFilter validFilter);
    }
}
