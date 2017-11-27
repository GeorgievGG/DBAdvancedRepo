using System;
using System.Reflection;

public class StartUp
{
    public static void Main()
    {
        MethodInfo oldestMemberMethod = typeof(Family).GetMethod("GetOldestMember");
        MethodInfo addMemberMethod = typeof(Family).GetMethod("AddMember");
        if (oldestMemberMethod == null || addMemberMethod == null)
        {
            throw new Exception();
        }

        var n = int.Parse(Console.ReadLine());
        var people = new Family();
        for (int i = 0; i < n; i++)
        {
            var inputParams = Console.ReadLine().Split();
            var name = inputParams[0];
            var age = int.Parse(inputParams[1]);
            people.AddMember(new Person(name, age));
        }
        var oldest = people.GetOldestMember();
        Console.WriteLine($"{oldest.Name} {oldest.Age}");
    }
}