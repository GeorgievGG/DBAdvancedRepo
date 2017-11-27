using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    public static void Main()
    {
        var n = int.Parse(Console.ReadLine());
        var cars = new List<Car>();
        for (int i = 0; i < n; i++)
        {
            var inputParams = Console.ReadLine().Split();

            var model = inputParams[0];
            var engineSpeed = decimal.Parse(inputParams[1]);
            var enginePower = decimal.Parse(inputParams[2]);
            var engine = new Engine(engineSpeed, enginePower);
            var cargoWeight = decimal.Parse(inputParams[3]);
            var cargoType = inputParams[4];
            var cargo = new Cargo(cargoWeight, cargoType);
            var tires = new Tire[4];
            for (int j = 0; j < 8; j += 2)
            {
                var tirePressure = decimal.Parse(inputParams[5 + j]);
                var tireAge = int.Parse(inputParams[5 + j + 1]);
                tires[j / 2] = new Tire(tirePressure, tireAge);
            }
            var car = new Car(model, engine, cargo, tires);
            cars.Add(car);
        }
        var reqCargoType = Console.ReadLine();
        if (reqCargoType == "fragile")
        {
            foreach (var car in cars.Where(x => x.Tires.Count(z => z.Pressure < 1) > 0))
            {
                Console.WriteLine(car.Model);
            }
        }
        else if (reqCargoType == "flammable")
        {
            foreach (var car in cars.Where(x => x.Engine.Power > 250))
            {
                Console.WriteLine(car.Model);
            }
        }
    }
}