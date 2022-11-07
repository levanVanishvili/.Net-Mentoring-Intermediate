using CommonServices;
using Microsoft.Azure.ServiceBus;
using System.Text;

class Program
{
    private const string SourceFolder = "C:\\Users\\Levani_Vanishvili\\Desktop\\.Net mentoring\\MessageQueues\\MessageQueues\\folder\\download";

    private const string ServiceBusConnectionString = "Endpoint=sb://levanservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=BgG4mffRLpu01iDBIGv6qIKswElYcyWxRYFACeeWIQU=";
    private const string TopicName = "processingtopic";
    private const string SubscriptionName = "processingtopicSub";
    static string currentFile = string.Empty;
    static string currentFilePath = string.Empty;
    static int bytesCount = 0;
    private static ISubscriptionClient _subscriptionClient;
    static void Main(string[] args)
    {
        ReceiveMessageAsync().GetAwaiter().GetResult();
    }

    static async Task ReceiveMessageAsync()
    {
        _subscriptionClient = new SubscriptionClient(ServiceBusConnectionString, TopicName, SubscriptionName);
        RegisterOnMessageHandlerAndReceiveMessages();
        Console.ReadKey();
        await _subscriptionClient.CloseAsync();
    }

    static void RegisterOnMessageHandlerAndReceiveMessages()
    {
        var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
        {
            MaxConcurrentCalls = 1,
            AutoComplete = false
        };
        _subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
    }

    static async Task ProcessMessagesAsync(Message message, CancellationToken token)
    {
        string fileName = message.Label;

        if (currentFile != fileName)
        {
            Console.WriteLine("Retrieving new file {0}", fileName);
            currentFilePath = Path.Combine(SourceFolder, fileName);
            currentFile = fileName;
        }

        var position = Convert.ToInt64(message.UserProperties["position"]);
        var size = Convert.ToInt32(message.UserProperties["size"]);
        var fileSize = Convert.ToInt64(message.UserProperties["fileSize"]);

        using (var fileStream = new FileStream(currentFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
        {
            fileStream.Position = position;
            fileStream.Write(message.Body, 0, size);
            fileStream.Flush();
        }

        bytesCount += size;
        Console.WriteLine("Current size: {0}", bytesCount);

        if (bytesCount == fileSize)
        {
            Console.WriteLine($"Received file {fileName} and saved it");
            bytesCount = 0;
        }

        await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
    }

    static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
    {
        Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}");
        return Task.CompletedTask;
    }
}