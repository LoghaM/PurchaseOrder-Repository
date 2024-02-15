namespace PurchaseOrderWebApplication.Filters
{
    public class Dependency : IDependency
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine($"writing this message : {message}");
        }
    }
}
