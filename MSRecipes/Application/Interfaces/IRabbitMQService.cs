using System.Threading.Tasks;

namespace MSRecipes.Application.Interfaces
{
    public interface IRabbitMQService
    {
        void StartListening();
        Task HandleMessageAsync(string message);
    }
}
