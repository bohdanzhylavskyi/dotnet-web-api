using ConsoleApp.DTOs;
using System.Text.Json;

namespace ConsoleApp.Operations
{
    public class UpdateCategoryOperation: IOperation
    {
        private ApiClient apiClient;

        public UpdateCategoryOperation(ApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public string GetName()
        {
            return "Update Category";
        }

        public async Task Execute()
        {
            var categoryId = ConsoleUtils.PromptNumericInput("Enter Category Id:");

            if (categoryId == null)
            {
                Console.WriteLine("Invalid input");
                
                return;
            }


            Console.WriteLine("Enter Category Update DTO:");

            string? input = Console.ReadLine();

            if (input == null)
            {
                Console.WriteLine("\nInvalid input");
                return;
            }

            var dto = JsonSerializer.Deserialize<UpdateCategoryDTO>(input);

            await this.apiClient.UpdateCategoryAsync((int) categoryId, dto);

            Console.WriteLine($"\nCategory was updated");
        }
    }
}
