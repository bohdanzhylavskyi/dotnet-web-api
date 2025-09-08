namespace ConsoleApp.Operations
{
    public class DeleteCategoryByIdOperation: IOperation
    {
        private ApiClient apiClient;

        public DeleteCategoryByIdOperation(ApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public string GetName()
        {
            return "Delete Category By Id";
        }

        public async Task Execute()
        {
            var categoryId = ConsoleUtils.PromptNumericInput("Enter Category Id:");

            if (categoryId == null)
            {
                Console.WriteLine("\nInvalid input");
                return;
            }

            await this.apiClient.DeleteCategoryByIdAsync((int) categoryId);

            Console.WriteLine($"\nCategory was deleted");
        }
    }
}
