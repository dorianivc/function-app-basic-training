using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace Training_Triggers
{
    public static class TimerFunction5
    {
        [FunctionName("TimerFunction5")]
        public static void Run([TimerTrigger("0 */60 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            for (int a = (10*60); a >= 0; a--)
            {
                log.LogInformation("Tiempo restante {0} segundos ", a);    
                System.Threading.Thread.Sleep(1000);
                if (a == 570)
                {
                    var client = new RestClient(Environment.GetEnvironmentVariable("upload_json_function_path"));
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("application/json", "{\r\n    \"name\": \"dorian\"\r\n}", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                }
                if (a == 300)
                {
                    log.LogInformation("Function dara timeout");
                    for(; ; )
                    {
                        log.LogInformation($"Killing CPU at: {DateTime.Now}");
                    }
                }
            }
        }
    }
}
