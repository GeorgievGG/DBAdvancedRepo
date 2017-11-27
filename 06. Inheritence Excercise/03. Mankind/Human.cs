using System;

public abstract class Human
{
    private string firstName;
    private string lastName;

    public Human(string fName, string lName)
    {
        this.FirstName = fName;
        this.LastName = lName;
    }

    public string FirstName
    {
        get
        {
            return firstName;
        }

        protected set
        {
            if (value[0] < 65 || value[0] > 90)
            {
                throw new ArgumentException("Expected upper case letter! Argument: firstName");
            }
            if (value.Length <= 3)
            {
                throw new ArgumentException("Expected length at least 4 symbols! Argument: firstName");
            }
            firstName = value;
        }
    }

    public string LastName
    {
        get
        {
            return lastName;
        }

        protected set
        {
            if (value[0] < 65 || value[0] > 90)
            {
                throw new ArgumentException("Expected upper case letter! Argument: lastName");
            }
            if (value.Length <= 2)
            {
                throw new ArgumentException("Expected length at least 3 symbols! Argument: lastName");
            }
            lastName = value;
        }
    }
    public override string ToString()
    {
        return $"First Name: {this.FirstName}{Environment.NewLine}Last Name: {this.LastName}";
    }
}