using DataGateway.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;


namespace DataGateway.Services

{
    public class RabbitMQService: IRabbitMQService, IDisposable
    {

        private readonly ConnectionFactory _factory;
        private IConnection? _connection;
        private IChannel? _channel;


        public RabbitMQService(IConfiguration configuration)
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "admin123"
            };
        }


        private async Task<IChannel> GetChannelAsync()
        {
            if (_channel != null) return _channel;

            _connection ??= await _factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            return _channel;
        }

        public async Task PublishAsync<T>(T message, string queueName)
        {
            var channel = await GetChannelAsync();

            await channel.QueueDeclareAsync
                (
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false
                );

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            await channel.BasicPublishAsync(exchange: "", routingKey: queueName, body: body);

        }


        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
