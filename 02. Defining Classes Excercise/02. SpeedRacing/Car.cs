public class Car
{
    public Car(string model, decimal fuelAmount, decimal consumptionPer1KM)
    {
        this.Model = model;
        this.FuelAmount = fuelAmount;
        this.ConsumptionPer1KM = consumptionPer1KM;
    }

    public string Model { get; set; }
    public decimal FuelAmount { get; set; }
    public decimal ConsumptionPer1KM { get; set; }
    public int DistanceTraveled { get; set; }

    public override string ToString()
    {
        return $"{this.Model} {this.FuelAmount:f2} {this.DistanceTraveled}";
    }
}