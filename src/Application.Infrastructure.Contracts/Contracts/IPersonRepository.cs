using RedisPractice.Domain.Entity;

namespace Application.Infrastructure.Contracts.Contracts;

public interface IPersonRepository
{
    Task CreatePerson(Person person);
    Task<IReadOnlyCollection<Person>> GetPersons(int skip, int take);
}