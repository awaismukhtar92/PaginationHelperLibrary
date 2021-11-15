using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace PaginationHelper.Pagination
{
    public class PaginationsHelper : IPaginationHelper
    {
        public PagedResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, int totalRecords, string route, HttpRequest accessor, PaginationFilter validFilter)
        {
            var request = accessor.HttpContext.Request;
            double totalPages = 0;
            int roundedTotalPages = 0;
            var _baseUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
            var uriService = new UriService(_baseUri);
            var respose = new PagedResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            totalPages = ((double)totalRecords / (double)(validFilter.PageSize));
            roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
            validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
           ? uriService.GetPageUri( new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize, validFilter.Filter), route)
           : null;
            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri( new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize, validFilter.Filter), route)
                : null;
            respose.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize, validFilter.Filter), route);
            respose.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize, validFilter.Filter), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }
    }
}
