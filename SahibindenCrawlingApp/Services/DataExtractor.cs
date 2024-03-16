using System.Text.RegularExpressions;
using OpenQA.Selenium;
using SahibindenCrawlingApp.Helpers;

namespace SahibindenCrawlingApp.Services;

public class DataExtractor : IDataExtractorService
{
    private readonly IWebDriver _driver;

    public DataExtractor(IWebDriver driver)
    {
        _driver = driver ?? throw new ArgumentNullException(nameof(driver));
    }

    public decimal ExtractAndPrintItems()
    {
        var items = _driver.FindElements(By.CssSelector("td.searchResultsGalleryItem"));
        int maxTitleLength = MaxTitleLengthHelper.GetMaxTitleLength(items);

        Console.WriteLine("Title" + new string(' ', Math.Max(0, maxTitleLength - 5)) + "\tPrice");
        decimal total = 0;
        var webPageModels = new List<WebPageModel>();

        foreach (var item in items)
        {
            var titleElement = item.FindElement(By.CssSelector("a.classifiedTitle"));
            var priceElement = item.FindElement(By.CssSelector("span"));

            string title = titleElement.Text; decimal price = PriceHelper.ExtractPrice(priceElement.Text);

            Console.WriteLine($"{title}" + new string(' ', maxTitleLength - title.Length + 4) + $"\t{priceElement.Text}");
            total += price;

            webPageModels.Add(new WebPageModel { Title = title, Price = priceElement.Text });
        }
        decimal average = total / webPageModels.Count;
        Console.WriteLine($"\nAverage of page: {average}");
        FileHelper.WriteToTextFile("AnasayfaVitrini_titles_and_prices.txt", webPageModels, maxTitleLength);
        return average;
    }

    public int FindPageNumber()
    {
        var pageCount = _driver.FindElement(By.CssSelector("p.mbdef"));//sahibinden page count format
        string pageInfoText = pageCount.Text;

        Match match = Regex.Match(pageInfoText, @"\d+");//according to the format of the page count, we extract the number

        if (match.Success)
            return int.Parse(match.Value);
        else
            throw new InvalidOperationException("Page count not found.");
    }
}
