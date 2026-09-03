# RabbitMQSender

## Overview

RabbitMQSender is a .NET console application that demonstrates how to connect to a RabbitMQ broker and publish messages to an exchange/queue. It serves as a simple producer example for background services, integration tests, or as a reference implementation for message publishing.

## Description

This application declares a direct exchange (`demo_exchange`), a queue (`demo_queue`), binds the queue with routing key `demo_routing_key`, and publishes 100 demo messages with a 100ms delay between each message.

**Key Features:**
- Connects to RabbitMQ using configurable host, port, username, and password
- Declares and manages exchanges and queues
- Publishes messages in a loop with async/await pattern
- Demonstrates proper resource management with `using` statements

## Pre-requisites

- **.NET 10 SDK** — install from https://dotnet.microsoft.com
- **RabbitMQ Server** — local or remote instance

### Quick Setup with WSL

Run RabbitMQ in Windows Subsystem for Linux (WSL):

1. **Enable mirrored networking mode** in `.wslconfig`:

```ini
[interop]
networkingMode=mirrored
```

2. **Install and start RabbitMQ in WSL**:

```bash
# Install RabbitMQ and Erlang (if not already installed)
sudo apt-get update
sudo apt-get install rabbitmq-server

# Start RabbitMQ service
sudo service rabbitmq-server start

# Enable the management plugin
sudo rabbitmq-plugins enable rabbitmq_management
```

3. **Verify the connection**:

Access the RabbitMQ management UI at http://localhost:15672 with default credentials (`guest`/`guest`).

### Alternative: Quick Setup with Docker

Alternatively, you can run RabbitMQ using Docker:

```bash
docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

### Configuration

Connection settings are hardcoded in `Program.cs`. To use custom values, update:

```csharp
var factory = new ConnectionFactory()
{
    HostName = "localhost",
    Port = 5672,
    UserName = "guest",
    Password = "guest"
};
```

For environment-based configuration, you can set these variables:
- `RABBITMQ_HOST` (default: `localhost`)
- `RABBITMQ_PORT` (default: `5672`)
- `RABBITMQ_USERNAME` (default: `guest`)
- `RABBITMQ_PASSWORD` (default: `guest`)
- `EXCHANGE_NAME` (default: `demo_exchange`)
- `QUEUE_NAME` (default: `demo_queue`)
- `ROUTING_KEY` (default: `demo_routing_key`)

## Build and Run

### 1. Restore and Build

```bash
dotnet restore
dotnet build
```

### 2. Run the Application

```bash
dotnet run --project RabbitMQSender.csproj
```

### 3. Expected Output

```
 [x] Sent Message 1
 [x] Sent Message 2
 ...
 [x] Sent Message 100
 Press [enter] to exit.
```

The application will publish 100 messages to the RabbitMQ broker and wait for user input before exiting.

### Notes

- The sample uses the `RabbitMQ.Client` NuGet package and targets .NET 8
- Exchanges and queues are set to `durable: false` by default — change this if you need message persistence
- For production use, consider implementing publisher confirms, retries, error handling, comprehensive logging, and dead-letter queues
