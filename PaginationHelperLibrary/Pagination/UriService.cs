using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaginationHelper.Pagination
{
    public class UriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPageUri(PaginationFilter filter, string route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));
            var _endPointUrl = string.Empty;
            var queryParams = new SortedList<string, string>();
            queryParams.Add("pageNumber", filter.PageNumber.ToString());
            queryParams.Add("pageSize", filter.PageSize.ToString());
            if (filter.Filter != null && filter.Filter.Any())
            {
                foreach (KeyValuePair<string, string> _filter in filter.Filter)
                {
                    queryParams.Add(_filter.Key, _filter.Value != null ? _filter.Value : "");
                }

                _endPointUrl = QueryHelpers.AddQueryString(_enpointUri.ToString(), queryParams);
                return new Uri(_endPointUrl);
            }

            return _enpointUri;

        }
    }
}
