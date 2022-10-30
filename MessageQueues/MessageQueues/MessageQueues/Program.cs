using CommonServices;

class Program
{
    static void Main(string[] args)
    {
        var blobFileService = new BlobFileService();
        var communicationService = new CommunicationService();

        communicationService.ReceiveMessageAsync().GetAwaiter().GetResult();

        Console.ReadLine();
        Console.WriteLine("Finished");
    }
}