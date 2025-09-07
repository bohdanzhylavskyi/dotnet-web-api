namespace ConsoleApp
{
    public static class ConsoleUtils
    {
        public static int? PromptNumericInput(string prompt)
        {
            Console.WriteLine(prompt);

            string? input = Console.ReadLine();

            if (int.TryParse(input, out int numericInput))
            {
                return numericInput;
            }

            return null;
        }
    }
}
