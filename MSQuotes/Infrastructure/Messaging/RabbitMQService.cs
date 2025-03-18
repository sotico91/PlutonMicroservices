using MSQuotes.Application.Interfaces;
using RabbitMQ.Client;
using System;
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
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "admin", Password = "admin" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                string exchangeName = "custom.direct";
                string routingKey = "recipeRoutingKey";

                channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct, durable: true, autoDelete: false);

                channel.QueueDeclare(queue: "quotesSendRecipe", durable: true, exclusive: false, autoDelete: false, arguments: null);
                channel.QueueDeclare(queue: "recipesQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

                channel.QueueBind(queue: "quotesSendRecipe", exchange: exchangeName, routingKey: routingKey);
                channel.QueueBind(queue: "recipesQueue", exchange: exchangeName, routingKey: routingKey);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: exchangeName,
                                     routingKey: routingKey,
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine($"Mensaje enviado a {exchangeName} con routingKey {routingKey}");
            }
        }
     }
}