using System.Collections.Generic;
using System.Threading.Tasks;
using MSPerson.Application.DTOs;
using MSPerson.Domain;

namespace MSPerson.Application.Interfaces
{
    public interface IPersonService
    {
        Task<int> CreatePerson(CreatePersonDto createPersonDto);
        Task<List<PersonDto>> GetAllPersons();
        Task<PersonDto> GetPersonById(int id);
        Task UpdatePersonAsync(int id, UpdatePersonDto updatePersonDto);
        Task DeletePersonAsync(int id);
    }
}
