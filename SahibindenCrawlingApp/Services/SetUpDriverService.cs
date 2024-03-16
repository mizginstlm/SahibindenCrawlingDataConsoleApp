using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SahibindenCrawlingApp.Utilities;
namespace SahibindenCrawlingApp.Services;
public class SetUpDriverService
{
    public static IWebDriver SetupWebDriver()
    {
        ChromeOptions options = ChromeUtility.ConfigureChromeOptions();
        return new ChromeDriver(options);
    }
}