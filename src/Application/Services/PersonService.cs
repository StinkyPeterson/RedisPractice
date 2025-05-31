using Application.Contracts.Contracts;
using Application.Infrastructure.Contracts.Contracts;
using RedisPractice.Domain.Entity;

namespace Application.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly ICacheService _cacheService;

    public PersonService(IPersonRepository personRepository, ICacheService cacheService)
    {
        _personRepository = personRepository;
        _cacheService = cacheService;
    }
    
    public async Task CreatePerson(Person person)
    {
        await _personRepository.CreatePerson(person);
    }

    public async Task<IReadOnlyCollection<Person>> GetPersons(int skip, int take)
    {
        var persons = await _personRepository.GetPersons(skip, take);
        
        return persons;
    }

    public async Task<Person> GetPersonById(Guid id)
    {
        var cached = await _cacheService.GetAsync<Person>(id.ToString());
        if (cached != null)
        {
            return cached;
        }

        var person = await _personRepository.GetPersonById(id);
        await _cacheService.SetAsync(id.ToString(), person, TimeSpan.FromMinutes(5));

        return person;
    }
}