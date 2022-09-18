using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Reflection;

IConfiguration _config = new ConfigurationBuilder()
    .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
    .AddEnvironmentVariables()
    .Build();
//Use user secrets to store the connection string run the following command in the terminal
//dotnet user-secrets init
//dotnet user-secrets set "ConnectionString" "add your connection string here"
string connectionString = _config["ConnectionString"];
string queueName = _config["QueueName"]; //"myqueue";
await using var client = new ServiceBusClient(connectionString);
ServiceBusSender sender = client.CreateSender(queueName);
// since ServiceBusClient implements IAsyncDisposable we create it with "await using"


IList<ServiceBusMessage> messages = new List<ServiceBusMessage>();
for (int i = 0; i < 500; i++)
{
    messages.Add(new ServiceBusMessage("{MessageId: " + i + "}"));
}

// send the messages
await sender.SendMessagesAsync(messages);


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Messages sent");

