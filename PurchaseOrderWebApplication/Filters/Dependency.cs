namespace PurchaseOrderWebApplication.Filters
{
    public class Dependency : IDependency
    {
        public string WriteMessage(string message)
        {
            Console.WriteLine($"writing this message : {message}");
            return message;
        }
    }
}
