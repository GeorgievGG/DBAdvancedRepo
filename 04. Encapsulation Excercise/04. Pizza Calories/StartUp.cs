using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    public static void Main()
    {
        var input = string.Empty;
        Pizza pizza = null;
        try
        {
            while ((input = Console.ReadLine()) != "END")
            {
                var inputParams = input.Split();
                var keyword = inputParams[0].ToLower();
                if (keyword == "pizza")
                {
                    var name = inputParams[1];
                    pizza = new Pizza(name);
                }
                if (keyword == "dough")
                {
                    var flourType = inputParams[1];
                    var bakingTech = inputParams[2];
                    var weight = int.Parse(inputParams[3]);
                    var dough = new Dough(flourType, bakingTech, weight);
                    pizza.Dough = dough;
                }
                else if (keyword == "topping")
                {
                    var toppingType = inputParams[1];
                    var weight = int.Parse(inputParams[2]);
                    var topping = new Topping(toppingType, weight);
                    pizza.AddTopping(topping);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Environment.Exit(0);
        }
        Console.WriteLine(pizza);
    }
}