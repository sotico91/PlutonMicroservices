using System.Threading.Tasks;

namespace MSAuthServ.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(string username, string password);
    }
}