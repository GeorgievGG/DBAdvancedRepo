using System;

public class Worker : Human
{
    private decimal salary;
    private decimal workHoursPerDay;

    public Worker(string fName, string lName, decimal salary, decimal whpd) : base(fName, lName)
    {
        this.Salary = salary;
        this.WorkHoursPerDay = whpd;
    }

    public decimal Salary
    {
        get
        {
            return salary;
        }

        protected set
        {
            if (value <= 10)
            {
                throw new ArgumentException("Expected value mismatch! Argument: weekSalary");
            }
            salary = value;
        }
    }

    public decimal WorkHoursPerDay
    {
        get
        {
            return workHoursPerDay;
        }

        protected set
        {
            if (value < 1 || value > 12)
            {
                throw new ArgumentException("Expected value mismatch! Argument: workHoursPerDay");
            }
            workHoursPerDay = value;
        }
    }

    public override string ToString()
    {
        return base.ToString() + $"{Environment.NewLine}Week Salary: {this.Salary:f2}{Environment.NewLine}Hours per day: {this.WorkHoursPerDay:f2}{Environment.NewLine}Salary per hour: {(this.Salary/(5*this.WorkHoursPerDay)):f2}";
    }
}