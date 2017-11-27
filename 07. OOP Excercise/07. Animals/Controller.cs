using System;
using System.Collections.Generic;

namespace _06.Animals
{
    public class Controller
    {
        public static void Main()
        {
            var animals = new List<Animal>();
            var input = string.Empty;
            while ((input = Console.ReadLine()) != "Beast!")
            {
                var inputSplit = Console.ReadLine().Split();
                var animalProps = new string[3];
                for (int i = 0; i < Math.Min(3, inputSplit.Length); i++)
                {
                    animalProps[i] = inputSplit[i];
                }
                var dummy = 0;
                if (!int.TryParse(animalProps[1], out dummy))
                {
                    Console.WriteLine("Invalid input!");
                }
                else
                {
                    try
                    {
                        switch (input.ToLower().Trim())
                        {
                            case "cat":
                                animals.Add(new Cat(animalProps[0], int.Parse(animalProps[1]), animalProps[2]));
                                break;
                            case "frog":
                                animals.Add(new Frog(animalProps[0], int.Parse(animalProps[1]), animalProps[2]));
                                break;
                            case "dog":
                                animals.Add(new Dog(animalProps[0], int.Parse(animalProps[1]), animalProps[2]));
                                break;
                            case "tomcat":
                                animals.Add(new Tomcat(animalProps[0], int.Parse(animalProps[1])));
                                break;
                            case "kitten":
                                animals.Add(new Kitten(animalProps[0], int.Parse(animalProps[1])));
                                break;
                            default:
                                Console.WriteLine("Invalid input!");
                                break;
                        }
                    }
                    catch (ArgumentException ae)
                    {
                        Console.WriteLine(ae.Message);
                    }
                }
            }
            foreach (var animal in animals)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(animal.ProduceSound());
                Console.ResetColor();
            }
        }
    }
}
