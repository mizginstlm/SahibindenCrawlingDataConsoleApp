
using System.Globalization;

namespace SahibindenCrawlingApp.Helpers
{
    public static class PriceHelper
    {
        public static decimal ExtractPrice(string priceText)
        {
            decimal price;
            bool isUSD = priceText.Contains("USD");
            bool isEuro = priceText.Contains("€");
            string currencySymbol = "TL";//default currency symbol  is Turkish Lira
            if (isUSD || isEuro)
            {
                decimal exchangeRate = GetExchangeRate(isUSD, isEuro);
                currencySymbol = isUSD ? "USD" : (isEuro ? "€" : "TL");
                priceText = RemoveCurrencySymbol(priceText, currencySymbol);

                if (decimal.TryParse(priceText, NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal foreignPrice))
                {
                    price = ConvertForeignPriceToTL(foreignPrice, exchangeRate);
                    return price;
                }
                else
                {
                    throw new ArgumentException("Invalid price format.");
                }
            }
            price = ExtractRegularPrice(priceText, currencySymbol);
            return price;
        }

        private static decimal GetExchangeRate(bool isUSD, bool isEuro)
        {
            return isUSD ? 32.12m : (isEuro ? 35.14m : 1.0m); // Example exchange rates,we should replace or get exchange rates from API provided by a secure financial service.
        }
        private static string RemoveCurrencySymbol(string priceText, string currencySymbol)
        {
            return priceText.Replace(currencySymbol, "").Trim();
        }

        private static decimal ConvertForeignPriceToTL(decimal foreignPrice, decimal exchangeRate)
        {
            return foreignPrice * exchangeRate;
        }

        private static decimal ExtractRegularPrice(string priceText, string currencySymbol)
        {
            // Regular price extraction
            decimal price;
            decimal.TryParse(priceText
                .Replace(currencySymbol, "")
                .Replace(".", "")
                .Replace(",", ".")
                .Trim(),
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out price);
            return price;
        }
    }
}
