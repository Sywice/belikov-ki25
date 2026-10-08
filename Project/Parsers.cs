using FuelPriceNamespace;
public class Parsers
{
    public static FuelPrice FuelParse(string input)
    {
        string[] tokens = input.Split(' ');

        double price = double.Parse(tokens[0]);
        DateTime date = DateTime.ParseExact(tokens[1], "yyyy.MM.dd", null);
        string fuelType = tokens[2];

        return new FuelPrice(price, date, fuelType);
    }
    public static GasStation StationParse(string input)
    {
        string[] tokens = input.Split(' ');

        return new GasStation(
            double.Parse(tokens[1]),
            double.Parse(tokens[2]),
            tokens[3]);
    }
    public static FuelDelivery DeliveryParse(string input)
    {
        string[] tokens = input.Split(' ');

        string supplier = tokens[0];
        string fuelType = tokens[1];
        DateTime deliveryDate = DateTime.ParseExact(tokens[2], "yyyy.MM.dd", null);
        double volume = double.Parse(tokens[3]);

        return new FuelDelivery(supplier, fuelType, deliveryDate, volume);
    }
}
