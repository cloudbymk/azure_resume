using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Functions.Worker;

namespace Company.Function
{
    public static class GetResumeCounter
    {
        [FunctionName("GetAndUpdateCounter")]
        public static async Task<HttpResponseMessage> Run(
            [Microsoft.Azure.WebJobs.HttpTrigger(Microsoft.Azure.WebJobs.Extensions.Http.AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "AzureResumeDB",
                containerName: "counter",
                Connection = "AzureResumeConnectionString",
                Id = "1",
                PartitionKey = "1")] Counter counter,
            [CosmosDB(
                databaseName: "AzureResumeDB",
                containerName: "counter",
                Connection = "AzureResumeConnectionString",
                Id = "1",
                PartitionKey = "1")] IAsyncCollector<Counter> updatedCounter,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Increment the count
            counter.Count++;

            // Update the counter in Cosmos DB
            await updatedCounter.AddAsync(counter);

            var jsonToReturn = JsonConvert.SerializeObject(counter);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };
        }
    }
}