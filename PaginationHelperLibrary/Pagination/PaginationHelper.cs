using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace PaginationHelper.Pagination
{
    public class PaginationsHelper : IPaginationHelper
    {
        public PagedResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, int totalRecords, string route, HttpRequest accessor, PaginationFilter validFilter = null)
        {
            var request = accessor.HttpContext.Request;
            PagedResponse<List<T>> respose = null;
            double totalPages = 0;
            int roundedTotalPages = 0;
            PaginationFilter _default = null;
            var _baseuri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
            var uriService = new UriService(_baseuri);
            var url = new Uri(string.Concat(_baseuri, route));
            if (validFilter == null)
            {
                _default = new PaginationFilter();
            }

            
            respose = new PagedResponse<List<T>>(pagedData, validFilter != null ? validFilter.PageNumber : _default.PageNumber, validFilter != null ? validFilter.PageSize : _default.PageSize);
            totalPages = ((double)totalRecords / (double)(validFilter != null ? validFilter.PageSize : _default.PageSize));
            roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
           (validFilter != null ? validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages : _default.PageNumber >= 1 && _default.PageNumber < roundedTotalPages)
           ? uriService.GetPageUri(validFilter != null ? new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize, validFilter.Filter) : _default, route)
           : null;
            respose.PreviousPage =
               (validFilter != null ? validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages : _default.PageNumber - 1 >= 1 && _default.PageNumber <= roundedTotalPages)
                ? uriService.GetPageUri(validFilter != null ? new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize, validFilter.Filter) : _default, route)
                : null;
            respose.FirstPage = uriService.GetPageUri(validFilter != null ? new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize, validFilter.Filter) : _default, route);
            respose.LastPage = uriService.GetPageUri(validFilter != null ? new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize, validFilter.Filter) : _default, route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }
    }
}
