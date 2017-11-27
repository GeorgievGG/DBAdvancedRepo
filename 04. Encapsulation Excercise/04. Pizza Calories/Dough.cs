using System;

public class Dough
{
    private string flourType;
    private string bakingTech;
    private int weight;

    public Dough(string flourType, string bakingTech, int weight)
    {
        this.FlourType = flourType;
        this.BakingTech = bakingTech;
        this.Weight = weight;
    }

    public string FlourType
    {
        get
        {
            return flourType;
        }

        private set
        {
            if (value.ToLower() != "white" && value.ToLower() != "wholegrain")
            {
                throw new ArgumentException("Invalid type of dough.");
            }
            flourType = value;
        }
    }

    public string BakingTech
    {
        get
        {
            return bakingTech;
        }

        private set
        {
            if (value.ToLower() != "crispy" && value.ToLower() != "chewy" && value.ToLower() != "homemade")
            {
                throw new ArgumentException("Invalid type of dough.");
            }
            bakingTech = value;
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
            if (value < 1 || value > 200)
            {
                throw new ArgumentException("Dough weight should be in the range [1..200].");
            }
            weight = value;
        }
    }

    public decimal FTMultiplier
    {
        get
        {
            switch (this.FlourType.ToLower())
            {
                case "white": return 1.5m;
                case "wholegrain": return 1m;
                default: return 1m;
            }
        }
    }

    public decimal BTMultiplier
    {
        get
        {
            switch (this.BakingTech.ToLower())
            {
                case "crispy": return 0.9m;
                case "chewy": return 1.1m;
                case "homemade": return 1m;
                default: return 1m;
            }
        }
    }

    public decimal Calories
    {
        get
        {
            return 2 * this.Weight * this.FTMultiplier * this.BTMultiplier;
        }
    }
}