using System;

public class Topping
{
    private string toppingType;
    private int weight;

    public Topping(string toppingType, int weight)
    {
        this.ToppingType = toppingType;
        this.Weight = weight;
    }

    public string ToppingType
    {
        get
        {
            return toppingType;
        }

        private set
        {
            if (value.ToLower() != "meat" && value.ToLower() != "veggies" && value.ToLower() != "cheese" && value.ToLower() != "sauce")
            {
                throw new ArgumentException($"Cannot place {value} on top of your pizza.");
            }
            toppingType = value;
        }
    }

    public int Weight
    {
        get
        {
            return weight;
        }

        private set
        {
            if (value < 1 || value > 50)
            {
                throw new ArgumentException($"{ToppingType} weight should be in the range [1..50].");
            }
            weight = value;
        }
    }

    public decimal TTMultiplier
    {
        get
        {
            switch (this.ToppingType.ToLower())
            {
                case "meat": return 1.2m;
                case "veggies": return 0.8m;
                case "cheese": return 1.1m;
                case "sauce": return 0.9m;
                default: return 1m;
            }
        }
    }

    public decimal Calories
    {
        get
        {
            return 2 * this.Weight * this.TTMultiplier;
        }
    }
}