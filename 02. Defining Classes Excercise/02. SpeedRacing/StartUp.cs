using System;
using System.Collections.Generic;
using System.Linq;

public class StartUp
{
    public static void Main()
    {
        var n = int.Parse(Console.ReadLine());
        var cars = new Dictionary<string, Car>();
        for (int i = 0; i < n; i++)
        {
            var inputParams = Console.ReadLine().Split();

            cars.Add(inputParams[0], new Car(inputParams[0], decimal.Parse(inputParams[1]), decimal.Parse(inputParams[2])));
        }
        var input = string.Empty;
        while ((input = Console.ReadLine()) != "End")
        {
            var inputParams = input.Split();

            var model = inputParams[1];
            var km = int.Parse(inputParams[2]);
            var reqCar = cars[model];
            if (reqCar.FuelAmount < km * reqCar.ConsumptionPer1KM)
            {
                Console.WriteLine("Insufficient fuel for the drive");
            }
            else
            {
                reqCar.DistanceTraveled += km;
                reqCar.FuelAmount -= km * reqCar.ConsumptionPer1KM;
            }
        }
        cars.Values.ToList().ForEach(x => Console.WriteLine(x));
    }
}