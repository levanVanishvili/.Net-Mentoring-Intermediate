using CommonServices;
using Microsoft.Azure.ServiceBus;

class Program
{
    private const string SourceFolder = "C:\\Users\\Levani_Vanishvili\\Desktop\\.Net mentoring\\MessageQueues\\MessageQueues\\folder";
    private const string ServiceBusConnectionString = "Endpoint=sb://levanservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=BgG4mffRLpu01iDBIGv6qIKswElYcyWxRYFACeeWIQU=";
    private const string TopicName = "processingtopic";

    private const int chunkSize = 256000;

    private static ITopicClient _topicClient;


    static void Main(string[] args)
    {
        MainMessageAsync().GetAwaiter().GetResult();
    }

    static async Task MainMessageAsync()
    {
        var path = Path.Combine(SourceFolder);
        var fileWatcher = new FileSystemWatcher()
        {
            Path = path,
            EnableRaisingEvents = true
        };

        fileWatcher.Created += FileWatcher_CreatedAsync;
       

        _topicClient = new TopicClient(ServiceBusConnectionString, TopicName);

        Console.WriteLine("Press Enter to stop the service");

        var input = Console.ReadKey();
        if (input.Key == ConsoleKey.Enter)
        {
            await _topicClient.CloseAsync();
        }
    }

    private static async void FileWatcher_CreatedAsync(object sender, FileSystemEventArgs e)
    {
        try
        {
            int count = 0;
            Console.WriteLine($"Created: {e.FullPath}");

            using (FileStream fileStream = File.OpenRead(e.FullPath))
            {
                while (fileStream.Position != fileStream.Length)
                {
                    var buffer = new byte[chunkSize];

                    var message = new Message(buffer)
                    {
                        Label = e.Name
                    };

                    message.UserProperties.Add("position", fileStream.Position);

                    var readBytes = fileStream.Read(buffer, 0, chunkSize);

                    message.UserProperties.Add("size", readBytes);
                    message.UserProperties.Add("fileSize", fileStream.Length);

                    await _topicClient.SendAsync(message);

                    count += readBytes;
                    Console.WriteLine("Sent: {0}", count);
                }
            }

            count = 0;
            Console.WriteLine($"File {e.Name} successfully sent");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
