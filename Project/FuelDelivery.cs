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
    }
}