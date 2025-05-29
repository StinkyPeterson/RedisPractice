using Application.Infrastructure.Contracts.Contracts;
using Infrastructure.Postgres.Context;
using Microsoft.EntityFrameworkCore;
using RedisPractice.Domain.Entity;

namespace Infrastructure.Postgres.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly ApplicationDbContext _context;
    
    public PersonRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task CreatePerson(Person person)
    {
        await _context.AddAsync(person);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<Person>> GetPersons(int skip, int take)
    {
        return await _context.Persons.Skip(skip).Take(take).ToListAsync();
    }

    public async Task<Person> GetPersonById(Guid id)
    {
        return await _context.Persons.FirstOrDefaultAsync(c => c.Id == id);
    }
}