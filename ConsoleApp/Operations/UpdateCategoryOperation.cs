using ConsoleApp.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace ConsoleApp.Operations
{
    public class UpdateCategoryOperation: IOperation
    {
        private HttpClient httpClient;

        public UpdateCategoryOperation(HttpClient httpClient)
        {
            this.httpClient = httpClient;
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

            await UpdateCategory((int) categoryId, dto);

            Console.WriteLine($"\nCategory was updated");
        }

        private async Task UpdateCategory(int categoryId, UpdateCategoryDTO dto)
        {
            await this.httpClient.PutAsJsonAsync($"categories/{categoryId}", dto);
        }
    }
}
