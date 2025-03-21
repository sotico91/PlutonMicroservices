using System.Collections.Generic;
using System.Threading.Tasks;
using MSPerson.Application.Commands;
using MSPerson.Application.DTOs;

namespace MSPerson.Application.Interfaces
{
    public interface IPersonService
    {
        Task<int> CreatePerson(CreatePersonCommand createPersonCommand);
        Task<List<PersonDto>> GetAllPersons();
        Task<PersonDto> GetPersonById(int id);
        Task UpdatePersonAsync(int id, UpdatePersonDto updatePersonDto);
        Task DeletePersonAsync(int id);
    }
}
