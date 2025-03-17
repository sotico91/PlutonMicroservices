using System.Collections.Generic;
using System.Threading.Tasks;
using MSPerson.Application.DTOs;
using MSPerson.Domain;

namespace MSPerson.Application.Interfaces
{
    public interface IPersonService
    {
        Task<List<PersonDto>> GetAllPersons();
        Task CreatePerson(CreatePersonDto createPersonDto);
        Task UpdatePersonAsync(int id, UpdatePersonDto updatePersonDto);
        Task DeletePersonAsync(int id);
        PersonDto ConvertToDto(Person person);
    }
}
