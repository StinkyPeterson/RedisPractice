using Application.Contracts.Contracts;
using Coravel.Invocable;
using RedisPractice.Domain.Entity;

namespace Host.BackgroundService.Job;

public class PersonAutofiller: IInvocable
{
    private readonly IPersonService _personService;
    
    public PersonAutofiller(IPersonService personService)
    {
        _personService = personService;
    }
    
    public async Task Invoke()
    {
        var random = new Random();
        var count = random.Next(10, 50); // 1-5 записей за раз
            
        for (int i = 0; i < count; i++)
        {
            var person = new Person(Guid.NewGuid(), GenerateRandomName(), GenerateRandomSurname(), GenerateRandomPatronymic());
                
            await _personService.CreatePerson(person);
        }
    }
    
    private string GenerateRandomName() => 
        new Bogus.Faker().Name.FirstName();
    
    private string GenerateRandomSurname() => 
        new Bogus.Faker().Name.LastName();
    
    private string GenerateRandomPatronymic() => 
        new Bogus.Faker().Name.FirstName();
}