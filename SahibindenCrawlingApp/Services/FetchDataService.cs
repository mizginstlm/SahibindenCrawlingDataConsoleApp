using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SahibindenCrawlingApp.Helpers;
using SahibindenCrawlingApp.Utilities;
namespace SahibindenCrawlingApp.Services;
public class FetchDataService
{
    public static bool FetchData(IWebDriver driver, string url, IDataExtractorService extractorService, ref List<decimal> allAverages)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        ChromeOptions options = ChromeUtility.ConfigureChromeOptions();
        int retryCount = 0;

        while (retryCount < 2) // Maximum 2 retries the reason explained in readme.md
        {
            try
            {
                Console.WriteLine($"\nNavigating to URL: {url}...");
                NavigationHelper.RetryNavigation(driver, options, url);
                Console.WriteLine("Navigation succeeded. Waiting for page to load...");

                decimal average = extractorService.ExtractAndPrintItems();

                allAverages.Add(average);// Calculate and store average
                Console.WriteLine("\nSuccess! Website did not detect Selenium. Extracted and Printed Data!");
                return true;
            }
            catch (Exception ex)
            {
                retryCount++;
                Console.WriteLine($"\nFailed to fetch data on attempt {retryCount}. Retrying...");
                Console.WriteLine($"Exception: {ex.Message}", ex);
            }
        }

        return false;
    }
}