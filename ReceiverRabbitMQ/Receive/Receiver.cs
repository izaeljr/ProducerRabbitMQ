using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory { HostName = "localhost" };
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
        durable: false,
        autoDelete: false,
        exclusive: false,
        arguments: null);

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received: {message}");
};

channel.BasicConsume(queue: "hello",
        autoAck: true,
        consumer: consumer);

Console.WriteLine(" Press [Enter] to exit.");
Console.ReadLine();
