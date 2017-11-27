using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class StartUp
{
    static void Main()
    {
        //Task 01.

        //Type personType = typeof(Person);
        //PropertyInfo[] properties = personType.GetProperties
        //    (BindingFlags.Public | BindingFlags.Instance);
        //Console.WriteLine(properties.Length);

        //Task 02.

        //Type personType = typeof(Person);
        //ConstructorInfo emptyCtor = personType.GetConstructor(new Type[] { });
        //ConstructorInfo ageCtor = personType.GetConstructor(new[] { typeof(int) });
        //ConstructorInfo nameAgeCtor = personType.GetConstructor(new[] { typeof(string), typeof(int) });
        //bool swapped = false;
        //if (nameAgeCtor == null)
        //{
        //    nameAgeCtor = personType.GetConstructor(new[] { typeof(int), typeof(string) });
        //    swapped = true;
        //}

        //string name = Console.ReadLine();
        //int age = int.Parse(Console.ReadLine());

        //Person basePerson = (Person)emptyCtor.Invoke(new object[] { });
        //Person personWithAge = (Person)ageCtor.Invoke(new object[] { age });
        //Person personWithAgeAndName = swapped ? (Person)nameAgeCtor.Invoke(new object[] { age, name }) : (Person)nameAgeCtor.Invoke(new object[] { name, age });

        //Console.WriteLine("{0} {1}", basePerson.Name, basePerson.Age);
        //Console.WriteLine("{0} {1}", personWithAge.Name, personWithAge.Age);
        //Console.WriteLine("{0} {1}", personWithAgeAndName.Name, personWithAgeAndName.Age);

        //Task 03.
        var n = int.Parse(Console.ReadLine());
        var people = new List<Person>();
        for (int i = 0; i < n; i++)
        {
            var inputParams = Console.ReadLine().Split();

            people.Add(new Person(inputParams[0], int.Parse(inputParams[1])));
        }
        people.Where(x => x.Age > 30).OrderBy(x => x.Name).ToList().ForEach(x => Console.WriteLine(x));
    }
}