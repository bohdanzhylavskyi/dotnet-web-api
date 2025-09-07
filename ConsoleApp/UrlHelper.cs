using System.Net;

namespace ConsoleApp
{
    public static class UrlHelper
    {
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
