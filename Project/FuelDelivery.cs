namespace FuelPriceNamespace
{
    public class FuelDelivery
    {
        public string Supplier { get; set; }
        public string FuelType { get; set; }
        public DateTime DeliveryDate { get; set; }
        public double Volume { get; set; }

        public FuelDelivery(string supplier, string fuelType, DateTime deliveryDate, double volume)
        {
            Supplier = supplier;
            FuelType = fuelType;
            DeliveryDate = deliveryDate;
            Volume = volume;
        }

        public static FuelDelivery Parse(string input)
        {
            string[] tokens = input.Split(' ');
            
            string supplier = tokens[0];
            string fuelType = tokens[1];
            DateTime deliveryDate = DateTime.ParseExact(tokens[2], "yyyy.MM.dd", null);
            double volume = double.Parse(tokens[3]);

            return new FuelDelivery(supplier, fuelType, deliveryDate, volume);
        }
    }
}