using System;
using System.Collections.Generic;

public class Person
{
    private string name;
    private decimal money;
    private readonly List<Product> bagOfProducts;

    public Person(string name, decimal money)
    {
        this.Name = name;
        this.Money = money;
        this.bagOfProducts = new List<Product>();
    }

    public string Name
    {
        get
        {
            return name;
        }

        private set
        {
            if (string.IsNullOrWhiteSpace(value) || value == string.Empty)
            {
                throw new ArgumentException("Name cannot be empty");
            }
            name = value;
        }
    }

    public decimal Money
    {
        get
        {
            return money;
        }

        private set
        {
            if (value < 0)
            {
                throw new ArgumentException("Money cannot be negative");
            }
            money = value;
        }
    }

    public IReadOnlyList<Product> BagOfProducts
    {
        get
        {
            return bagOfProducts.AsReadOnly();
        }
    }

    public void AddProduct(Product prod)
    {
        this.bagOfProducts.Add(prod);
    }

    public void SubtractMoney(decimal sum)
    {
        this.Money -= sum;
    }
}