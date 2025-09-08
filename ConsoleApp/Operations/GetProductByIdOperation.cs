using System.Text.Json;

namespace ConsoleApp.Operations
{
    public class GetProductByIdOperation: IOperation
    {
        private ApiClient apiClient;

        public GetProductByIdOperation(ApiClient apiClient)
        {
            this.apiClient = apiClient;
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

            var product = await this.apiClient.GetProductByIdAsync((int) productId);

            Console.WriteLine($"\nResult: {JsonSerializer.Serialize(product)}");
        }
    }
}
