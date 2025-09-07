using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Entities;

namespace ConsoleApp.Operations
{
    public class GetCategoryByIdOperation: IOperation
    {
        private HttpClient httpClient;

        public GetCategoryByIdOperation(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public string GetName()
        {
            return "Get Category By Id";
        }

        public async Task Execute()
        {
            var categoryId = ConsoleUtils.PromptNumericInput("Enter Category Id:");

            if (categoryId == null)
            {
                Console.WriteLine("\nInvalid input");
                return;
            }

            var category = await GetCategoryById((int) categoryId);

            Console.WriteLine($"\nResult: {JsonSerializer.Serialize(category)}");
        }

        private async Task<Category> GetCategoryById(int id)
        {
            return await this.httpClient.GetFromJsonAsync<Category>($"categories/{id}");
        }
    }
}
