using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using MSPerson.Application.interfaces;
using MSPerson.Domain;
using MSPerson.Infrastructure.Data;

namespace MSPerson.Infrastructure.repositories
{
	public class PersonRepository : IPersonRepository
	{
        private readonly PeopleDbContext _context;
        public PersonRepository(PeopleDbContext context)
        {
            _context = context;
        }
        public async Task<Person> GetByIdAsync(int id)
        {
            return await _context.People.FindAsync(id);
        }
        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.People.AsNoTracking().ToListAsync();
        }
        public async Task AddAsync(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person != null)
            {
                _context.People.Remove(person);
                await _context.SaveChangesAsync();
            }
        }

    }
}