using System.Text.Json;

namespace ConsoleApp.Operations
{
    public class ListProductsOperation: IOperation
    {
        private ApiClient apiClient;

        public ListProductsOperation(ApiClient apiClient)
        {
            this.apiClient = apiClient;
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

            var products = await this.apiClient.GetProductsListAsync(categoryId, pageNumber, pageSize);

            Console.WriteLine("\nResult:");
            Console.WriteLine(
                JsonSerializer.Serialize(products, new JsonSerializerOptions() { WriteIndented = true})
            );
        }

        
    }
}
