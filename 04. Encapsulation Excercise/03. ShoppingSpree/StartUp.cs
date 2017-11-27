using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    public static void Main()
    {
        try
        {
            var people = new Dictionary<string, Person>();
            var inputParams = Console.ReadLine().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var person in inputParams)
            {
                var parms = person.Split('=');
                people.Add(parms[0], new Person(parms[0], decimal.Parse(parms[1])));
            }
            var products = new Dictionary<string, Product>();
            inputParams = Console.ReadLine().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var prod in inputParams)
            {
                var parms = prod.Split('=');
                products.Add(parms[0], new Product(parms[0], decimal.Parse(parms[1])));
            }

            var input = string.Empty;
            while ((input = Console.ReadLine()) != "END")
            {
                var ip = input.Split();
                var name = ip[0];
                var product = string.Join(" ", ip.Skip(1));
                if (people[name].Money < products[product].Price)
                {
                    Console.WriteLine($"{name} can't afford {product}");
                }
                else
                {
                    Console.WriteLine($"{name} bought {product}");
                    people[name].AddProduct(products[product]);
                    people[name].SubtractMoney(products[product].Price);
                }
            }

            foreach (var person in people.Values)
            {
                Console.Write(person.Name + " - ");
                if (person.BagOfProducts.Count == 0)
                {
                    Console.WriteLine("Nothing bought");
                }
                else
                {
                    Console.WriteLine(string.Join(", ", person.BagOfProducts.Select(x => x.Name)));
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}