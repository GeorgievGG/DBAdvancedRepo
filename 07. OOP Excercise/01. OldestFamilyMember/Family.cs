using System.Collections.Generic;
using System.Linq;

public class Family
{
    private readonly List<Person> people;

    public Family()
    {
        this.people = new List<Person>();
    }

    public IReadOnlyList<Person> People
    {
        get
        {
            return people;
        }
    }

    public void AddMember(Person psn)
    {
        this.people.Add(psn);
    }

    public Person GetOldestMember()
    {
        return this.people.OrderByDescending(x => x.Age).FirstOrDefault();
    }
}