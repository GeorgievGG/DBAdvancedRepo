using System;

public class Tomcat : Cat
{
    public Tomcat(string name, int age) : base(name, age, "Male")
    {
        this.AnimalType = "Tomcat";
        this.Sound = "MEOW";
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
            if (value == "Female")
            {
                throw new ArgumentException("Invalid input!");
            }
            this.gender = value;
        }
    }
}