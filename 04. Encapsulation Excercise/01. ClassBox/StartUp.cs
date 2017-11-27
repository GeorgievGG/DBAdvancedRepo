using System;
using System.Linq;
using System.Reflection;

public class StartUp
{
    public static void Main()
    {
        Type boxType = typeof(Box);
        FieldInfo[] fields = boxType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        Console.WriteLine(fields.Count());
        try
        {
            var length = decimal.Parse(Console.ReadLine());
            var width = decimal.Parse(Console.ReadLine());
            var height = decimal.Parse(Console.ReadLine());

            var box = new Box(length, width, height);
            Console.WriteLine(box);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}