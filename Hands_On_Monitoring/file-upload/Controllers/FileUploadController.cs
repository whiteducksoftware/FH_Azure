using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;

namespace file_upload.Controllers
{
    [Route("api/[controller]")]
    public class FileUploadController : Controller
    {
        //Appsettings Configuration
        private readonly IConfiguration _configuration;
        private TelemetryClient _telemetry;

        public FileUploadController(IConfiguration config, TelemetryClient telemetry)
        {
            _configuration = config;
            _telemetry = telemetry;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileAsync([FromForm]IFormFile file)
        {
            //Parse ConnectionString
            if (CloudStorageAccount.TryParse(_configuration.GetConnectionString("StorageAccount"), out CloudStorageAccount storageAccount))
            {
                //Create client and create BlobContainer
                var client = storageAccount.CreateCloudBlobClient();
                var container = client.GetContainerReference("originalfile");
                await container.CreateIfNotExistsAsync();
                _telemetry.TrackEvent("UploadEvent",
                 new Dictionary<string, string>()
                 {
                     { "Filename", file.FileName },
                     { "FileSize", file.Length.ToString() }
                });

                //Creates a Blob and uploads file into Blob
                var blob = container.GetBlockBlobReference(file.FileName);
                await blob.UploadFromStreamAsync(file.OpenReadStream());

                return Ok(blob.Uri);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
