namespace RedisPractice.Domain.Entity;

public class Person : Common.Entity
{
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Patronymic { get; private set; }

    public string FullName => $"{Surname} {Name} {Patronymic}";

    protected Person(Guid id) : base(id)
    {
        
    }

    public Person(Guid id, string name, string surname, string patronymic) : base(id)
    {
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
    }
}