# RabbitMQSender

.NET Console Application using RabbitMQ for sending messages to a queue.

## Overview

This is a small .NET console app that connects to a RabbitMQ broker and publishes messages to a configured exchange/queue. It serves as a simple producer example you can adapt for background services, integration tests, or as a reference implementation for publishing messages.

The example declares a direct exchange (`demo_exchange`), a queue (`demo_queue`), binds the queue with routing key `demo_routing_key`, and publishes demo messages.

## Prerequisites

- .NET 8 SDK (net8.0) — install from https://dotnet.microsoft.com
- A running RabbitMQ server (local or remote)

You can run a local RabbitMQ server with the management plugin using Docker:

```bash
docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

The RabbitMQ management UI will be available at http://localhost:15672 (default user/password: `guest`/`guest`).

## Configuration

The example currently uses connection settings defined in `Program.cs` (see the code for exact values). Example hardcoded snippet:

```csharp
var factory = new ConnectionFactory()
{
    HostName = "localhost",
    Port = 5672,
    UserName = "guest",
    Password = "guest"
};
```

For more flexible configuration, consider reading these values from environment variables or an `appsettings.json`.

Suggested environment variables (if you change the code to read them):

- RABBITMQ_HOST (default: `localhost`)
- RABBITMQ_PORT (default: `5672`)
- RABBITMQ_USERNAME (default: `guest`)
- RABBITMQ_PASSWORD (default: `guest`)
- EXCHANGE_NAME (e.g. `demo_exchange`)
- QUEUE_NAME (e.g. `demo_queue`)
- ROUTING_KEY (e.g. `demo_routing_key`)
- MESSAGE (optional: message body to send)

Example (Linux / macOS):

```bash
export RABBITMQ_HOST=localhost
export RABBITMQ_PORT=5672
export RABBITMQ_USERNAME=guest
export RABBITMQ_PASSWORD=guest
export QUEUE_NAME=demo_queue
export EXCHANGE_NAME=demo_exchange
export ROUTING_KEY=demo_routing_key
export MESSAGE="Hello from RabbitMQSender"
```

Example (Windows PowerShell):

```powershell
$env:RABBITMQ_HOST = "localhost"
$env:RABBITMQ_PORT = "5672"
$env:RABBITMQ_USERNAME = "guest"
$env:RABBITMQ_PASSWORD = "guest"
$env:QUEUE_NAME = "demo_queue"
$env:EXCHANGE_NAME = "demo_exchange"
$env:ROUTING_KEY = "demo_routing_key"
$env:MESSAGE = "Hello from RabbitMQSender"
```

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

You should see console output indicating messages were sent, for example:

```
 [x] Sent Message 1
 [x] Sent Message 2
 ...
```

Press Enter to exit after messages have been sent.

## Usage examples

- Send a batch of demo messages (the example publishes multiple messages with a short delay).
- Provide a message body via environment variable or modify the code to accept command-line arguments (for example: `dotnet run -- "My message"`).
- Integrate into scripts or CI to enqueue work for consumers.

## How it works

- The app creates a connection to RabbitMQ using the supplied host, port, and credentials.
- It declares an exchange and a queue, binds the queue to the exchange with a routing key, and publishes messages to the exchange.
- Messages are encoded (commonly as UTF-8 strings or JSON) and published using the default basic publish API.

## Notes

- The sample uses `RabbitMQ.Client` and targets .NET 8.
- The example sets `durable: false` for queue/exchange by default — change these settings if you need persistence.
- For production use, consider publisher confirms, retries, better error handling, logging, and dead-lettering.
