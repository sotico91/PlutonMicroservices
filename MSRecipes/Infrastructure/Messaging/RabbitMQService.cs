using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Autofac;
using MSRecipes.Application.Commands;
using MSRecipes.Application.DTOs;
using MSRecipes.Application.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MSRecipes.Infrastructure.Messaging
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly IComponentContext _componentContext;
        private readonly string _queueName;

        public RabbitMQService(IComponentContext componentContext, string queueName)
        {
            _componentContext = componentContext;
            _queueName = queueName;
        }

        public void StartListening()
        {
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "admin", Password = "admin" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            string exchangeName = "custom.direct";

            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct, durable: true, autoDelete: false);
            channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(queue: _queueName, exchange: exchangeName, routingKey: "recipesQueue");

            var consumer = new EventingBasicConsumer(channel);

            System.Diagnostics.Debug.WriteLine("Antes de ... MSRecipes");

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    var createRecipeDto = JsonSerializer.Deserialize<CreateRecipeDto>(message);

                    using (var scope = _componentContext.Resolve<ILifetimeScope>().BeginLifetimeScope())
                    {
                        var recipeService = scope.Resolve<IRecipeService>();

                        var createRecipeCommand = ConvertToDto(createRecipeDto);
                        await recipeService.CreateRecipeAsync(createRecipeCommand);
                    }

                    System.Diagnostics.Debug.WriteLine($"Recibido y procesado en MSRecipes: {message}");


                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error procesando mensaje: {ex.Message}");

                    channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                }
            };


            channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
        }

        async Task IRabbitMQService.HandleMessageAsync(string message)
        {
            Console.WriteLine($"Procesando mensaje: {message}");

            await Task.Delay(500);
        }

    private static CreateRecipeCommand ConvertToDto(CreateRecipeDto createRecipeDto)
        {
            return new CreateRecipeCommand
            {
                Code = createRecipeDto.Code,
                PatientId = createRecipeDto.PatientId,
                Description = createRecipeDto.Description,
                ExpiryDate = createRecipeDto.ExpiryDate
            };
        }
    }
}