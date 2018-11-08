using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public FileUploadController(IConfiguration config)
        {
            _configuration = config;
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

                //Creates a Blob and uploads file into Blob
                var blob = container.GetBlockBlobReference(file.FileName);
                await blob.UploadFromStreamAsync(file.OpenReadStream());

                return Ok(blob.Uri);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
