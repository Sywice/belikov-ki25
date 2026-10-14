using System.Globalization;
using FuelPriceNamespace;
public class Parsers
{
    public static FuelPrice FuelParse(string input)
    {
        string[] tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length < 3)
        {
            throw new ArgumentException("Неверный формат строки");
        }

        double price = 0;
        DateTime date = DateTime.MinValue;
        string fuelType = "";

        foreach(string token in tokens)
        {
            if (double.TryParse(token, out double parsedPrice))
            {
                price = parsedPrice;
                continue;
            }
            else if (DateTime.TryParseExact(token, "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                date = parsedDate;
                continue;
            }
            else
            {
                fuelType = token;
            }
        }

        return new FuelPrice(price, date, fuelType);
    }
    public static GasStation StationParse(string input)
    {
        string[] tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length < 3)
        {
            throw new ArgumentException("Неверный формат строки");
        }

        double? x = null;
        double? y = null;
        string? stationName = null;

        foreach (string token in tokens)
        {
            if (double.TryParse(token, out double parsedValue))
            {
                if (x == null)
                {
                    x = parsedValue;
                }
                else if (y == null)
                {
                    y = parsedValue;
                }
            }
            else
            {
                stationName = token;
            }
        }

        return new GasStation(
            double.Parse(tokens[0]),
            double.Parse(tokens[1]),
            tokens[2]);
    }
    public static FuelDelivery DeliveryParse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Входная строка пуста или содержит только пробелы.");
        }
        string[] tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length != 4)
        {
            throw new ArgumentException($"Неверный формат строки: ожидалось 4 параметра, но получено {tokens.Length}.");
        }
        string supplier = "";
        string fuelType = "";
        DateTime deliveryDate = DateTime.MinValue;
        double volume = 0;

        foreach (string token in tokens)
        {
            if (DateTime.TryParseExact(token, "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                deliveryDate = parsedDate;
                continue;
            }
            else if (double.TryParse(token.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double parsedVolume))
            {
                volume = parsedVolume;
                continue;
            }
            else
            {
                if (supplier == "")
                {
                    supplier = token;
                }
                else
                {
                    fuelType = token;
                }
            }
        }

        return new FuelDelivery(supplier, fuelType, deliveryDate, volume);
    }
}
