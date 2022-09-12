using System.Security.AccessControl;
using Azure.Messaging.ServiceBus;

string connectionString = "";
string queueName = "myqueue";
await using var client = new ServiceBusClient(connectionString);
ServiceBusSender sender = client.CreateSender(queueName);
// since ServiceBusClient implements IAsyncDisposable we create it with "await using"


IList<ServiceBusMessage> messages = new List<ServiceBusMessage>();
for (int i = 0; i < 100; i++)
{
    messages.Add(new ServiceBusMessage("{MessageId: " + i + "}"));
}

// send the messages
await sender.SendMessagesAsync(messages);


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Messages sent");

