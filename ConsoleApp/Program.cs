using ConsoleApp.Operations;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp
{
   internal class Program
    {
        static IOperation[] Operations;

        static async Task Main(string[] args)
        {
            using var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(ResolveServerBaseUrl())
            };

            var apiClient = new ApiClient(httpClient);

            Operations = new IOperation[] {
                new ListCategoriesOperation(apiClient),
                new ListProductsOperation(apiClient),
                new GetCategoryByIdOperation(apiClient),
                new GetProductByIdOperation(apiClient),
                new CreateCategoryOperation(apiClient),
                new UpdateCategoryOperation(apiClient),
                new DeleteCategoryByIdOperation(apiClient),
            };


            await Run();
        }

        static async Task Run()
        {
            while (true)
            {
                PrintMenu(Operations);

                var operationIndex = ConsoleUtils.PromptNumericInput("Enter Operation Index: ");

                if (operationIndex == null || operationIndex < 0 || operationIndex > Operations.Length )
                {
                    Console.WriteLine("Invalid input \n\n");
                    continue;
                }

                var selectedOperation = Operations[(int) operationIndex];

                try
                {
                    await selectedOperation.Execute();
                } catch (Exception e)
                {
                    Console.WriteLine($"Operation failed: {e}");
                }
            }
        }

        private static void PrintMenu(IOperation[] operations)
        {
            Console.WriteLine("\n##########################################");

            for (var i = 0; i < operations.Length; i++)
            {
                Console.WriteLine($"[{i}] {operations[i].GetName()}");
            }

            Console.WriteLine("##########################################\n");
        }

        private static string ResolveServerBaseUrl()
        {
            var folder = Directory.GetCurrentDirectory();

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            return config.GetSection("ServerBaseUrl").Value;
        }
    }
}
