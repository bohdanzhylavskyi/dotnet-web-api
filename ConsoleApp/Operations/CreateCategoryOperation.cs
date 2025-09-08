using ConsoleApp.DTOs;
using System.Text.Json;

namespace ConsoleApp.Operations
{
    public class CreateCategoryOperation: IOperation
    {
        private ApiClient apiClient;

        public CreateCategoryOperation(ApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public string GetName()
        {
            return "Create Category";
        }

        public async Task Execute()
        {
            Console.WriteLine("Enter Category Creation DTO:");

            string? input = Console.ReadLine();

            if (input == null)
            {
                Console.WriteLine("\nInvalid input");
                return;
            }

            var dto = JsonSerializer.Deserialize<CreateCategoryDTO>(input);

            var createdCategory = await this.apiClient.CreateCategoryAsync(dto);

            Console.WriteLine($"\nResult: {JsonSerializer.Serialize(createdCategory)}");
        }

        
    }
}
