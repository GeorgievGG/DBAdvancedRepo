using System;

public class Kitten : Cat
{
    public Kitten(string name, int age) : base(name, age, "Female")
    {
        this.AnimalType = "Kitten";
        this.Sound = "Meow";
    }
    private string gender;
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
            if (value == "Male")
            {
                throw new ArgumentException("Invalid input!");
            }
            this.gender = value;
        }
    }
}