using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Threading.Tasks;

namespace EventBusRabbitMQ
{
    public class RabbitMQConnection : IRabbitMQConnection
    {
        private readonly ILogger<RabbitMQConnection> _logger;
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private bool _disposed;

        public RabbitMQConnection(ILogger<RabbitMQConnection> logger, IConnectionFactory connectionFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

            if (!IsConnected)
            {
                TryConnect();
            }
        }

        public bool IsConnected 
        { 
            get
            {
                return _connection != null && _connection.IsOpen && !_disposed;
            }
        }

        public bool TryConnect()
        {
            try
            {
                _connection = _connectionFactory.CreateConnection();
            }
            catch (BrokerUnreachableException)
            {
                _logger.LogError("Rabbit MQ connection could not be created. Trying again.");

                Task.Delay(2000);
                _connection = _connectionFactory.CreateConnection();
            }

            return IsConnected;
        }

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                _logger.LogError("Rabbit MQ connection could not be established.");
                throw new InvalidOperationException("Rabbit MQ connection could not be established.");
            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            try
            {
                _connection.Dispose();
            }
            catch (Exception e)
            {
                _logger.LogError("Error while trying to dispose Rabbit MQ connection.", e);
                throw;
            }
        }
    }
}