using System;
using Xunit;
using FuelPriceNamespace;
public class ParsersTests
    {
        [Fact]
        public void FuelParse_ValidString_ReturnsCorrectFuelPrice()
        {
            string input = "55.5 2026.10.14 АИ-95";

            FuelPrice result = Parsers.FuelParse(input);

            Assert.Equal(55.5, result.Price);
            Assert.Equal(new DateTime(2026, 10, 14), result.Date);
            Assert.Equal("АИ-95", result.FuelType);
        }

        [Fact]
        public void FuelParse_EmptyString_ThrowsArgumentException()
        {
            string badInput = "";

            Assert.Throws<ArgumentException>(() => Parsers.FuelParse(badInput));
        }

        [Theory]
        [InlineData("")]
        [InlineData("50.2   -50.  2")]
        [InlineData("hello   world")]
        public void StationParse_InvalidFormat(string badString)
        {
            Assert.Throws<ArgumentException>(() => Parsers.StationParse(badString));
        }

        [Fact]
        public void StationParse_ValidString_ReturnsCorrect()
        {
            string input = "50.2 30.5 GasStation1";
            GasStation result = Parsers.StationParse(input);

            Assert.Equal(50.2, result.LocationX);
            Assert.Equal(30.5, result.LocationY);
            Assert.Equal("GasStation1", result.Name);
        }

        [Fact]
        public void DeliveryParse_ValidStringCorrect()
        {
            string input = "sup1 50,1 2026.10.14 GasStation1";
            FuelDelivery result = Parsers.DeliveryParse(input);

            Assert.Equal("sup1", result.Supplier);
            Assert.Equal(50.1, result.Volume);
            Assert.Equal(new DateTime(2026, 10, 14), result.DeliveryDate);
            Assert.Equal("GasStation1", result.FuelType);   
        }
        [Theory]
        [InlineData(". ")]
        [InlineData("10 50,2 2026.10.14")]
        [InlineData("sup1 50.1 GasStation1")]
        public void DeliveryParse_InvFormat(string badString)
        {
            Assert.Throws<ArgumentException>(() => Parsers.DeliveryParse(badString));
        }   
    }