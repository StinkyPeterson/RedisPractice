using RedisPractice.Domain.Entity;

namespace Application.Contracts.Contracts;

public interface IPersonService
{
    Task CreatePerson(Person person);
    Task<IReadOnlyCollection<Person>> GetPersons(int skip, int take);
    Task<Person> GetPersonById(Guid id);
}