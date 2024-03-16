using OpenQA.Selenium;
using SahibindenCrawlingApp.Services;
namespace SahibindenCrawlingApp;
class Program
{
    static void Main()
    {
        List<decimal> allAverages = new List<decimal>(); // List to store averages
        string baseUrl = "https://www.sahibinden.com/anasayfa-vitrin?viewType=Gallery&pagingOffset=";
        int pageCount = 1;
        for (int i = 0; i <= pageCount; i++)
        {
            using (IWebDriver driver = SetUpDriverService.SetupWebDriver())
            {
                IDataExtractorService extractorService = new DataExtractor(driver);
                string url = baseUrl + (i * 20); // sahibinden pagingOffset format
                bool success = FetchDataService.FetchData(driver, url, extractorService, ref allAverages);

                if (i == 0)
                {
                    pageCount = extractorService.FindPageNumber();
                }

                if (!success)
                {
                    Console.WriteLine("\nFailed to fetch data even after retrying. Exiting loop.");
                    break; // Exit the loop if unsuccessful after retries
                }
            }
        }
        CalculateAverage.Calculate(allAverages);
    }
}