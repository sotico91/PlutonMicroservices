
namespace MSQuotes.Application.Interfaces
{
         public interface IRabbitMQService
        {
            void SendMessage(string message);
        }
    }
