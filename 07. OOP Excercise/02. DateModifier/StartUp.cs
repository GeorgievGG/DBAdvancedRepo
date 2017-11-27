using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    public static void Main()
    {
        var dm = new DateModifier();
        for (int i = 0; i < 2; i++)
        {
            var inputParams = Console.ReadLine().Split();
            var year = int.Parse(inputParams[0]);
            var month = int.Parse(inputParams[1]);
            var day = int.Parse(inputParams[2]);

            dm.AddDate(new DateTime(year, month, day), i);
        }
        Console.WriteLine(dm.GetDiff());
    }
}