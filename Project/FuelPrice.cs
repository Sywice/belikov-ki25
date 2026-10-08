namespace FuelPriceNamespace
{
    public class FuelPrice
    {
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public string FuelType { get; set; }

        public FuelPrice(double price, DateTime date, string fuelType)
        {
            Price = price;
            Date = date;
            FuelType = fuelType;
        }
    }
}