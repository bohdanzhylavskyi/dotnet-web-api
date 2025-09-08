using System.Text.Json;

namespace ConsoleApp.Operations
{
    public class ListCategoriesOperation: IOperation
    {
        private ApiClient apiClient;

        public ListCategoriesOperation(ApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public string GetName()
        {
            return "List Categories";
        }

        public async Task Execute()
        {
            var categories = await this.apiClient.GetCategoriesListAsync();

            Console.WriteLine("\nResult:");
            Console.WriteLine(
                JsonSerializer.Serialize(categories, new JsonSerializerOptions() { WriteIndented = true})
            );
        }
    }
}
