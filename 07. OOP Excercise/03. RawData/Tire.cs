public class Tire
{
    private decimal pressure;
    private int age;

    public Tire(decimal pressure, int age)
    {
        this.Pressure = pressure;
        this.Age = age;
    }

    public decimal Pressure
    {
        get
        {
            return pressure;
        }

        private set
        {
            pressure = value;
        }
    }

    public int Age
    {
        get
        {
            return age;
        }

        private set
        {
            age = value;
        }
    }
}