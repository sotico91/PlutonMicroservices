using MSQuotes.Application.Interfaces;
using RabbitMQ.Client;
using System.Text;

namespace MSQuotes.Infrastructure.Messaging
{
	public class RabbitMQService : IRabbitMQService
    {
        private readonly string _hostname;
        private readonly string _queueName;

        public RabbitMQService(string hostname, string queueName)
        {
            _hostname = hostname;
            _queueName = queueName;
        }

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: _queueName,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}