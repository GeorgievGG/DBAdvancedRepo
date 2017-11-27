using System.Collections.Generic;

public class Car
{
    private string model;
    private Engine engine;
    private Cargo cargo;
    private readonly List<Tire> tires;

    public Car(string model, Engine engine, Cargo cargo, Tire[] tires)
    {
        this.Model = model;
        this.Engine = engine;
        this.Cargo = cargo;
        this.tires = new List<Tire>();
        for (int i = 0; i < tires.Length; i++)
        {
            this.tires.Add(tires[i]);
        }
    }

    public string Model
    {
        get
        {
            return model;
        }

        private set
        {
            model = value;
        }
    }

    public Engine Engine
    {
        get
        {
            return engine;
        }

        private set
        {
            engine = value;
        }
    }

    public Cargo Cargo
    {
        get
        {
            return cargo;
        }

        private set
        {
            cargo = value;
        }
    }

    public IReadOnlyList<Tire> Tires
    {
        get
        {
            return tires;
        }
    }
}