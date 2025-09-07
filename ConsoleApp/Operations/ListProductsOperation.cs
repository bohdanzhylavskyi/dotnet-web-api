using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Entities;

namespace ConsoleApp.Operations
{
    public class ListProductsOperation: IOperation
    {
        private HttpClient httpClient;

        public ListProductsOperation(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public string GetName()
        {
            return "List Products";
        }

        public async Task Execute()
        {
            var categoryId = ConsoleUtils.PromptNumericInput("Category Id Filter (Optional):");
            var pageNumber = ConsoleUtils.PromptNumericInput("Page Number (Optional):");
            var pageSize = ConsoleUtils.PromptNumericInput("Page Size (Optional):");

            var products = await GetProductsList(categoryId, pageNumber, pageSize);

            Console.WriteLine("\nResult:");
            Console.WriteLine(
                JsonSerializer.Serialize(products, new JsonSerializerOptions() { WriteIndented = true})
            );
        }

        private async Task<List<Product>> GetProductsList(int? categoryId, int? pageNumber, int? pageSize)
        {
            var query = new Dictionary<string, string>();

            if (categoryId != null)
            {
                query.Add("categoryId", categoryId.ToString());
            }

            if (pageNumber != null)
            {
                query.Add("pageNumber", pageNumber.ToString());
            }

            if (pageSize != null)
            {
                query.Add("pageSize", pageSize.ToString());
            }

            string queryStr = UrlHelper.QueryParamsToString(query);

            Console.WriteLine(queryStr);

            return await this.httpClient.GetFromJsonAsync<List<Product>>($"products?{queryStr}");
        }
    }
}
