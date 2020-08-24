using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace Training_Triggers
{
    public static class QueueNotification
    {
        [FunctionName("QueueNotification")]
        public static void Run([QueueTrigger("outqueue", Connection = "AzureWebJobsStorage")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            var client = new RestClient(Environment.GetEnvironmentVariable("email_logic_app_path"));
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter($"application/json", "{\r\n    \"msg\": \""+ myQueueItem +"\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

        }
    }
}
