using System;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using api.Models;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace api.Services
{
    public class RabbitMQService : IQueueService
    {
        private readonly ILogger<RabbitMQService> _logger;
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly IModel _model;

        public RabbitMQService(ILogger<RabbitMQService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _logger.LogInformation("Connecting to RabbitMQ...");

            var timer = new Stopwatch();
            timer.Start();

            _connectionFactory = new ConnectionFactory()
            {
                HostName = "queue",
                Port = 5672,
                UserName = "demo",
                Password = "demo",
            };

            _connection = _connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
            _model.QueueDeclare(
                queue: "demo-queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            timer.Stop();

            _logger.LogInformation("Finished connecting to RabbitMQ in {ElapsedMilliseconds}ms", timer.ElapsedMilliseconds);
        }


        public bool Enqueue(string name)
        {
            try
            {
                var payload = Encoding.UTF8.GetBytes(
                    JsonSerializer.Serialize(
                        new QueueMessage
                        {
                            TraceId = Activity.Current?.TraceId.ToString(),
                            Name = name
                        }));
                _model.BasicPublish(exchange: "", routingKey: "demo-queue", basicProperties: null, body: payload);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to enqueue message. RabbitMQ may be experiencing issues. Monitor the service in case this is persistent.");
                return false;
            }
        }
    }
}