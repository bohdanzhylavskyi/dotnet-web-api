using ConsoleApp.Operations;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp
{
   internal class Program
    {
        static string ServerBaseUrl = ResolveBaseUrl();

        static IOperation[] Operations;

        static async Task Main(string[] args)
        {
            using var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(ServerBaseUrl)
            };

            Operations = new IOperation[] {
                new ListCategoriesOperation(httpClient),
                new ListProductsOperation(httpClient),
                new GetCategoryByIdOperation(httpClient),
                new GetProductByIdOperation(httpClient),
                new CreateCategoryOperation(httpClient),
                new UpdateCategoryOperation(httpClient),
            };


            await Run();
        }

        static async Task Run()
        {
            while (true)
            {
                PrintMenu(Operations);

                string? input = Console.ReadLine();

                int.TryParse(input, out int parsedInput);

                if (parsedInput == null || parsedInput < 0 || parsedInput > Operations.Length )
                {
                    Console.WriteLine("Invalid input \n\n");
                    continue;
                }

                var selectedOperation = Operations[parsedInput];

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

        private static string ResolveBaseUrl()
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
