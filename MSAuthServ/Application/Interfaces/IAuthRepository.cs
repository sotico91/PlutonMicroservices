using System.Threading.Tasks;
using MSAuthServ.Domain;

namespace MSAuthServ.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> ValidateUserAsync(string username, string password);
        Task<User> GetByIdAsync(int id);
    }
}
