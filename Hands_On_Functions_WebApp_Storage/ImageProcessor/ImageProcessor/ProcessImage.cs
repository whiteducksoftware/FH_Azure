using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Filters;
using SixLabors.ImageSharp.Processing.Transforms;


namespace ImageProcessor
{
    public static class ProcessImage
    {
        private static readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=araimgupload;AccountKey=ZhD9rzKSJNFJCR2lA7GjOXjIQrwskWnbmAaDaoXw6zGJmbaaEf4RYdi4CgkbO+y0Aq8bF9qVHCJ01LwjI5sLTA==;EndpointSuffix=core.windows.net";

        [FunctionName("ProcessImage")]
        public static async Task Run([BlobTrigger("originalfile/{name}", Connection = "FileBlob")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            //Process Image
            MemoryStream output = new MemoryStream();
            using (Image<Rgba32> image = Image.Load(myBlob))
            {
                image.Mutate(x => x
                     .Resize(image.Width / 4, image.Height / 4)
                     .Grayscale());
                image.SaveAsJpeg(output); 
            }
            output.Position = 0;

            if (CloudStorageAccount.TryParse(connectionString, out CloudStorageAccount storageAccount))
            {
                //Create client and create BlobContainer
                var client = storageAccount.CreateCloudBlobClient();
                var container = client.GetContainerReference("mutatedfile");
                await container.CreateIfNotExistsAsync();

                //Creates a Blob and uploads file into Blob
                var blob = container.GetBlockBlobReference(name);
                await blob.UploadFromStreamAsync(output);
                log.LogInformation($"Image processed!");
            }

        }
    }
}
