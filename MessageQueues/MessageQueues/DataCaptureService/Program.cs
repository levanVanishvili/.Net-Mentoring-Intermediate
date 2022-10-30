using CommonServices;

class Program
{
    private const string SourceFolder = "C:\\Users\\Levani_Vanishvili\\Desktop\\.Net mentoring\\MessageQueues\\MessageQueues\\folder";

    static async Task Main(string[] args)
    {
        _ = UploadFilesToBlob();

        await Task.Delay(5000);
        Console.WriteLine("Finished");
    }

    static async Task UploadFilesToBlob()
    {
        var blobFileService = new BlobFileService();
        var communicationService = new CommunicationService();

        var localFolder = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), SourceFolder));
        var files = localFolder.GetFiles();
        foreach (var fileInfo in files)
        {
            var file = File.OpenRead(fileInfo.FullName);

            await using (file)
            {
                var fileId = Guid.NewGuid();

                await blobFileService.UploadFileAsync(fileId, file);
                Console.WriteLine($"File uploaded. ID: {fileId}; Path: {fileInfo.FullName};");

                await communicationService.UploadComplatedMessageAsync(fileId);
            }
        }
    }
}
