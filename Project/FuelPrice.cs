using System.Globalization;

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

        public static FuelPrice Parse(string input)
        {
            string[] tokens = input.Split(' ');

            double price = 0;
            DateTime date = default;
            string fuelType = "";

            foreach (string token in tokens)
            {
                if (double.TryParse(token, out double parsedPrice))
                {
                    price = parsedPrice;
                    continue;
                }
                if (DateTime.TryParseExact(
                    token,
                    "yyyy.MM.dd",
                    null,
                    DateTimeStyles.None,
                    out DateTime parsedDate))
                {
                    date = parsedDate;
                    continue;
                }
                fuelType = token;
            }

            return new FuelPrice(price, date, fuelType);
        }
    }
}