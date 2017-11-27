using System;

public class Book
{
    private string title;
    private string author;
    private decimal price;

    public Book(string author, string title, decimal price)
    {
        this.Title = title;
        this.Author = author;
        this.Price = price;
    }

    public string Title
    {
        get
        {
            return title;
        }

        protected set
        {
            if (value.Length < 3)
            {
                throw new ArgumentException("Title not valid!");
            }
            title = value;
        }
    }

    public string Author
    {
        get
        {
            return author;
        }

        protected set
        {
            int num;
            if (value.Split().Length > 1 && int.TryParse(value.Split()[1][0].ToString(), out num))
            {
                throw new ArgumentException("Author not valid!");
            }
            author = value;
        }
    }

    public decimal Price
    {
        get
        {
            return price;
        }

        protected set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Price not valid!");
            }
            price = value;
        }
    }

    public override string ToString()
    {
        return $"Type: {this.GetType().Name}" + Environment.NewLine +
            $"Title: {this.Title}" + Environment.NewLine +
            $"Author: {this.Author}" + Environment.NewLine +
            $"Price: {this.Price:f2}";
    }

}