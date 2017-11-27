public class Cargo
{
    private decimal weight;
    private string type;

    public Cargo(decimal weight, string type)
    {
        this.Weight = weight;
        this.Type = type;
    }

    public decimal Weight
    {
        get
        {
            return weight;
        }

        private set
        {
            weight = value;
        }
    }

    public string Type
    {
        get
        {
            return type;
        }

        private set
        {
            type = value;
        }
    }
}