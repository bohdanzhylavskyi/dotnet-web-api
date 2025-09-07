using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Entities;

namespace ConsoleApp.Operations
{
    public class ListCategoriesOperation: IOperation
    {
        private HttpClient httpClient;

        public ListCategoriesOperation(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public string GetName()
        {
            return "List Categories";
        }

        public async Task Execute()
        {
            var categories = await GetCategoriesList();

            Console.WriteLine("\nResult:");
            Console.WriteLine(
                JsonSerializer.Serialize(categories, new JsonSerializerOptions() { WriteIndented = true})
            );
        }

        private async Task<List<Category>> GetCategoriesList()
        {
            return await this.httpClient.GetFromJsonAsync<List<Category>>($"categories");
        }

    }
}
