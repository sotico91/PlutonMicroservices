using System;
using System.Text;
using System.Threading.Tasks;
using MSRecipes.Application.Interfaces;
using MSRecipes.Application.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MSRecipes.Infrastructure.Messaging
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly RecipeService _recipeService;

        public RabbitMQService(string hostname, string queueName, RecipeService recipeService)
        {
            _hostname = hostname;
            _queueName = queueName;
            _recipeService = recipeService;
        }

        public void StartListening()
        {

            Console.WriteLine("Esperando mensajes...");

            var factory = new ConnectionFactory() { HostName = _hostname };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    Console.WriteLine($"Mensaje recibido: {message}");

                    try
                    {
                        // Simulación de procesamiento
                        await Task.Delay(1000);

                        // Aquí deberías llamar a tu método de negocio
                        await HandleMessageAsync(message);

                        // Confirmar el mensaje para que RabbitMQ lo elimine de la cola
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error procesando mensaje: {ex.Message}");
                    }
                };

                channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);

                Console.WriteLine("Consumidor iniciado, esperando mensajes...");

                // Mantener la aplicación en ejecución
                while (true) { }
            }
        }

    private async Task HandleMessageAsync(string message)
        {
            Console.WriteLine($"Procesando mensaje: {message}");
            // Aquí debes implementar la lógica para actualizar la base de datos
            await Task.Delay(500); // Simulación de trabajo
        }

        Task IRabbitMQService.HandleMessageAsync(string message)
        {
            return HandleMessageAsync(message);
        }
    } 
  
}