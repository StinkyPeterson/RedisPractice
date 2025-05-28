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
        var cacheKey = $"persons_{skip}_{take}";
        var cached = await _cacheService.GetAsync<IReadOnlyCollection<Person>>(cacheKey);
        
        if (cached != null) 
            return cached;

        var persons = await _personRepository.GetPersons(skip, take);
        await _cacheService.SetAsync(cacheKey, persons, TimeSpan.FromMinutes(5));
        
        return persons;
    }
}