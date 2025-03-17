
using System.Collections.Generic;
using System.Threading.Tasks;
using MSPerson.Domain;

namespace MSPerson.Application.interfaces
{
    public interface IPersonRepository
    {
        Task<Person> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task AddAsync(Person person);
        Task UpdateAsync(Person person);
        Task DeleteAsync(int id);
    }
}
