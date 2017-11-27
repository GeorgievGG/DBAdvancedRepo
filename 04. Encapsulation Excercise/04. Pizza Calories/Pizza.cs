using System;
using System.Collections.Generic;
using System.Linq;

public class Pizza
{
    private string name;
    private readonly List<Topping> toppings;
    private Dough dough;

    public Pizza(string name)
    {
        this.Name = name;
        this.Dough = dough;
        this.toppings = new List<Topping>();
    }

    public string Name
    {
        get
        {
            return name;
        }

        private set
        {
            if (string.IsNullOrWhiteSpace(value) || value == string.Empty || value.Length > 15)
            {
                throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
            }
            name = value;
        }
    }

    public IReadOnlyCollection<Topping> Toppings
    {
        get
        {
            return toppings;
        }
    }

    public Dough Dough
    {
        get
        {
            return dough;
        }

        set
        {
            dough = value;
        }
    }

    public int NumberOfToppings
    {
        get
        {
            return this.Toppings.Count;
        }
    }

    public decimal TotalCalories
    {
        get
        {
            return this.Dough.Calories + this.Toppings.Select(x => x.Calories).Sum();
        }
    }

    public void AddTopping(Topping topping)
    {
        if (this.NumberOfToppings == 10)
        {
            throw new ArgumentException("Number of toppings should be in range [0..10].");
        }
        this.toppings.Add(topping);
    }
    public override string ToString()
    {
        return $"{this.Name} - {this.TotalCalories:f2} Calories.";
    }
}