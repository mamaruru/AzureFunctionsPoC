using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Sql;

namespace AzureFunctionsPoc
{
    public static class SqlTriggerExample
    {
        [FunctionName("SqlTriggerExample")]
        public static void Run(
            [SqlTrigger("dbo.Products", "SqlConnectionString")] IReadOnlyList<SqlChange<Product>> changes,
            ILogger log)
        {
            foreach (SqlChange<Product> change in changes)
            {
                Product product = change.Item;
                log.LogInformation($"Change operation: {change.Operation}");
                log.LogInformation($"Id: {product.Id}, Name: {product.Name}");
            }
        }
    }

    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }
}

namespace FunctionApp1
{
    public static class Function1
    {
        [FunctionName("QueueTriggerCSharp")]
        public static void Run([QueueTrigger("myqueue-items", 
            Connection = "AzureWebJobsStorage")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}