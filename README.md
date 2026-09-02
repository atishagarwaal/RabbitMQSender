# RabbitMQSender

.NET Console Application using RabbitMQ for sending messages to a queue.

## Overview

This small example application demonstrates how to publish messages to a RabbitMQ exchange/queue using the RabbitMQ.Client library and .NET 8.

The program declares a direct exchange called `demo_exchange`, a queue called `demo_queue`, binds the queue with routing key `demo_routing_key`, and publishes 100 demo messages with a short delay between each.

## Requirements

- .NET 8 SDK (net8.0)
- A running RabbitMQ server (default expects it on `localhost:5672`)

You can run a local RabbitMQ server with the management plugin using Docker:

```bash
docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

The RabbitMQ management UI will be available at http://localhost:15672 (default user/password: `guest`/`guest`).

## Build and run

1. Restore and build the project:

```bash
dotnet restore
dotnet build
```

2. Run the sender:

```bash
dotnet run --project RabbitMQSender.csproj
```

You should see console output like:

```
 [x] Sent Message 1
 [x] Sent Message 2
 ...
```

Press Enter to exit after messages have been sent.

## Configuration

The connection settings are currently hardcoded in `Program.cs`:

```csharp
var factory = new ConnectionFactory()
{
    HostName = "localhost",
    Port = 5672,
    UserName = "guest",
    Password = "guest"
};
```

For production use, consider reading these values from environment variables or a configuration file.

## Notes

- The example sets `durable: false` and `autoDelete: false` for the queue; change these values if you need persistence.
- This project uses package `RabbitMQ.Client` (see `RabbitMQSender.csproj`).

## License

MIT
