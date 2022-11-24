using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNP_Assignment
{
    public class UploadAndDownloadFiles
    {
        private readonly IConfiguration _configuration;

        public UploadAndDownloadFiles(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public void UploadFiles()
        {
            string BlobConnectionString = _configuration.GetSection("BlobConnectionString").Value;
            string BlobContainerName = _configuration.GetSection("BlobContainerName").Value;
            string FilesDownloadToPath = _configuration.GetSection("FilesDownloadToPath").Value;
            string FilesUploadFromPath = _configuration.GetSection("FilesUploadFromPath").Value;

            BlobServiceClient blobServiceClient = new BlobServiceClient(BlobConnectionString);
            BlobContainerClient blobContainerClient = blobServiceClient.CreateBlobContainer(BlobContainerName);

            var files = Directory.GetFiles(FilesUploadFromPath);
            foreach (var file in files)
            {
                using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(file)))
                {
                    blobContainerClient.UploadBlob(Path.GetFileName(file), ms);
                }
                Console.WriteLine(file + "Uploaded!");
            }


            var containerClient = blobServiceClient.GetBlobContainerClient(BlobContainerName);
            var blobs = containerClient.GetBlobs();
            foreach (var blob in blobs)
            {
                Console.WriteLine(blob.Name);
                BlobClient blobClient = containerClient.GetBlobClient(blob.Name);
                blobClient.DownloadTo(FilesDownloadToPath + blob.Name);
            }



        }
    }
}
