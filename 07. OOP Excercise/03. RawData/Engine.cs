public class Engine
{
    private decimal speed;
    private decimal power;

    public Engine(decimal speed, decimal power)
    {
        this.Speed = speed;
        this.Power = power;
    }

    public decimal Speed
    {
        get
        {
            return speed;
        }

        private set
        {
            speed = value;
        }
    }

    public decimal Power
    {
        get
        {
            return power;
        }

        private set
        {
            power = value;
        }
    }
}