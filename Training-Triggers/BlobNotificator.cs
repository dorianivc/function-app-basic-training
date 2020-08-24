using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Training_Triggers
{
    public static class BlobNotificator
    {
        [FunctionName("BlobNotificator")]
        public static void Run([Queue("outqueue"), StorageAccount("AzureWebJobsStorage")] ICollector<string> msg, [BlobTrigger("archivos/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            msg.Add(String.Format("El archivo " + name + " fue agregado al storage account exitosamente"));
        }
    }
}
