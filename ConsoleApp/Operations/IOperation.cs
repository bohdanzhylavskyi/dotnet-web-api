namespace ConsoleApp.Operations
{
    public interface IOperation
    {
        public Task Execute();
        public string GetName();
    }
}
