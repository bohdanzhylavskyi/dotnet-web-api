using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Entities;

namespace ConsoleApp.Operations
{
    public class GetProductByIdOperation: IOperation
    {
        private HttpClient httpClient;

        public GetProductByIdOperation(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public string GetName()
        {
            return "Get Product By Id";
        }

        public async Task Execute()
        {
            var productId = ConsoleUtils.PromptNumericInput("Enter Product Id:");

            if (productId == null)
            {
                Console.WriteLine("\nInvalid input");
                return;
            }

            var product = await GetProductById((int) productId);

            Console.WriteLine($"\nResult: {JsonSerializer.Serialize(product)}");
        }

        private async Task<Product> GetProductById(int id)
        {
            return await this.httpClient.GetFromJsonAsync<Product>($"products/{id}");
        }
    }
}
