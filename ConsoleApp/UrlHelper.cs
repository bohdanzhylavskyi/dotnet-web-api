using System.Collections.Specialized;
using System.Net;

namespace ConsoleApp
{
    public static class UrlHelper
    {
        public static string AddQueryString(string uri, IDictionary<string, string?> queryParams)
        {
            var builder = new UriBuilder(uri);
            var query = System.Web.HttpUtility.ParseQueryString(builder.Query);

            foreach (var kvp in queryParams)
            {
                query[kvp.Key] = kvp.Value;
            }

            builder.Query = query.ToString();
            return builder.ToString();
        }

        public static string QueryParamsToString(IDictionary<string, string?> queryParams)
        {
            string queryString = string.Join("&",
                queryParams
                    .Where(kvp => !string.IsNullOrEmpty(kvp.Value))
                    .Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value!)}")
            );

            return queryString;
        }
    }
}
