using Microsoft.Azure.ServiceBus;
using System.Text;

namespace CommonServices
{
    public class CommunicationService
    {
        private const string SourceFolder = "C:\\Users\\Levani_Vanishvili\\Desktop\\.Net mentoring\\MessageQueues\\MessageQueues\\folder";

        private const string ServiceBusConnectionString = "Endpoint=sb://levanservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=BgG4mffRLpu01iDBIGv6qIKswElYcyWxRYFACeeWIQU=";
        private const string TopicName = "processingtopic";
        private const string SubscriptionName = "processingtopicSub";

        private static ITopicClient _topicClient;
        private static ISubscriptionClient _subscriptionClient;

        public CommunicationService()
        {
            _subscriptionClient = new SubscriptionClient(ServiceBusConnectionString, TopicName, SubscriptionName);
        }

        public async Task UploadComplatedMessageAsync(Guid FileId)
        {
            _topicClient = new TopicClient(ServiceBusConnectionString, TopicName);
            await SendMessagesAsync(FileId);
            Console.ReadKey();
            await _topicClient.CloseAsync();
        }

        static async Task SendMessagesAsync(Guid id)
        {
            try
            {
                var message = new Message(Encoding.UTF8.GetBytes(id.ToString()));
                Console.WriteLine($"Sending message: {message}");
                await _topicClient.SendAsync(message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now} :: Excemtion: {e.Message}");
            }
            
        }

        public async Task ReceiveMessageAsync()
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
            var blobFileService = new BlobFileService();

            Console.WriteLine($"Received message: SequenceNumber: {message.SystemProperties.SequenceNumber} body: {Encoding.UTF8.GetString(message.Body)}");

            var fileId = Guid.Parse(Encoding.UTF8.GetString(message.Body));
            var fileFullName = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), SourceFolder, $"{fileId}{".png"}"));
            var file = File.OpenWrite(fileFullName);

            try
            {
                await using (file)
                {
                    await blobFileService.DownloadFile(fileId, file);
                    Console.WriteLine($"File downloaded. ID: {fileId}; Path: {fileFullName};");

                    await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
            }
        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}");
            return Task.CompletedTask;
        }
    }
}
