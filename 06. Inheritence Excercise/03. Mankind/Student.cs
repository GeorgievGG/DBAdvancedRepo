using System;

public class Student : Human
{
    private string facultyNumber;

    public Student(string fName, string lName, string fNumber) : base(fName, lName)
    {
        this.FacultyNumber = fNumber;
    }

    public string FacultyNumber
    {
        get
        {
            return facultyNumber;
        }

        protected set
        {
            if (value.Length < 5 || value.Length > 10)
            {
                throw new ArgumentException("Invalid faculty number!");
            }
            facultyNumber = value;
        }
    }

    public override string ToString()
    {
        return base.ToString() + $"{Environment.NewLine}Faculty number: {this.FacultyNumber}";
    }
}