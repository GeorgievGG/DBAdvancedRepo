using System;

public class Frog : Animal
{
    public Frog(string name, int age, string gender)
    {
        this.Name = name;
        this.Age = age;
        this.Gender = gender;
    }
    private string name;
    private int age;
    private string gender;

    public override string Name
    {
        get
        {
            return this.name;
        }

        set
        {
            if (string.IsNullOrWhiteSpace(value) || value == string.Empty)
            {
                throw new ArgumentException("Invalid input!");
            }
            this.name = value;
        }
    }
    public override int Age
    {
        get
        {
            return this.age;
        }

        set
        {
            if (value < 1)
            {
                throw new ArgumentException("Invalid input!");
            }
            this.age = value;
        }
    }
    public override string Gender
    {
        get
        {
            return this.gender;
        }

        set
        {
            if (string.IsNullOrWhiteSpace(value) || value == string.Empty)
            {
                throw new ArgumentException("Invalid input!");
            }
            this.gender = value;
        }
    }

    public override string ProduceSound()
    {
        return $"Frog{Environment.NewLine}{base.ProduceSound()}{Environment.NewLine}Ribbit";
    }
}