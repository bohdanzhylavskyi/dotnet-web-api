using ConsoleApp.DTOs;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Entities;

namespace ConsoleApp.Operations
{
    public class CreateCategoryOperation: IOperation
    {
        private HttpClient httpClient;

        public CreateCategoryOperation(HttpClient httpClient)
        {
            this.httpClient = httpClient;
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

            var createdCategory = await CreateCategory(dto);

            Console.WriteLine($"\nResult: {JsonSerializer.Serialize(createdCategory)}");
        }

        private async Task<Category> CreateCategory(CreateCategoryDTO dto)
        {
            var response = await this.httpClient.PostAsJsonAsync($"categories", dto);
            var createdCategory = await response.Content.ReadFromJsonAsync<Category>();

            return createdCategory;
        }
    }
}
