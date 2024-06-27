using System;
using RabbitMQ.Client;
using System.Text;

class Program
{
    static async Task Main(string[] args)
    {
        // Connection parameters
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            Port = 5672, // Default RabbitMQ port, change if necessary
            UserName = "guest", // Default is "guest"
            Password = "guest"  // Default is "guest"
        };

        // Establish a connection
        using (var connection = factory.CreateConnection())
        // Create a channel
        using (var channel = connection.CreateModel())
        {
            // Declare an exchange
            channel.ExchangeDeclare(exchange: "demo_exchange", type: ExchangeType.Direct);

            // Declare a queue
            channel.QueueDeclare(queue: "demo_queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            // Bind the queue to the exchange
            channel.QueueBind(queue: "demo_queue",
                              exchange: "demo_exchange",
                              routingKey: "demo_routing_key");

            for (int i = 0; i < 100; i++)
            {
                // Message to send
                string message = $"Message {i + 1}";
                var body = Encoding.UTF8.GetBytes(message);

                // Publish the message to the exchange
                channel.BasicPublish(exchange: "demo_exchange",
                                     routingKey: "demo_routing_key",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine(" [x] Sent {0}", message);

                // Simulate some async work
                await Task.Delay(100);
            }
        }

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}
