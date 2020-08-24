
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;

namespace Training_Triggers
{
    public static class Upload_JSON
    {

        [FunctionName("Upload_JSON")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            
            string contentType;
            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");




            contentType = req.ContentType;

            if (contentType == "application/json")
            {
                string body;

                body = req.Body.ToString();

                if (!string.IsNullOrEmpty(body))
                {
                    string name;

                    name = Guid.NewGuid().ToString("n");

                    BlobContainerClient container = new BlobContainerClient(connectionString, "archivos");
                    container.CreateIfNotExists(PublicAccessType.Blob);
                    container.UploadBlob(name + ".json", req.Body);
                    

                }
            }

            return new OkObjectResult("done");
        }
    }
}
