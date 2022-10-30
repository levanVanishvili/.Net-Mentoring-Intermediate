using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices
{
    public  class BlobFileService
    {
        private const string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=blobformessagequeuing;AccountKey=HrX9JTP7lzheWFORscrAFnPFbj01Q/1JJZygIA3EgR2s/KdhcSBt5h2r52z+61XQp+1kmjfyiPUG+AStK1tGDA==;EndpointSuffix=core.windows.net";

        private const string BlobContainer = "fileupload";

        private readonly BlobContainerClient _blobContainerClient;

        public BlobFileService()
        {
            _blobContainerClient = new BlobContainerClient(ConnectionString, BlobContainer);
        }
        public async Task UploadFileAsync(Guid fileId, Stream file)
        {
            var blobClient = _blobContainerClient.GetBlobClient(fileId.ToString());
            await blobClient.UploadAsync(file);
        }
        public async Task DownloadFile(Guid fileId, Stream file)
        {
            var blobClient = _blobContainerClient.GetBlobClient(fileId.ToString());
            await blobClient.DownloadToAsync(file);
        }
    }
}
