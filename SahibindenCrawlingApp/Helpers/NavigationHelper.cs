using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SahibindenCrawlingApp.Helpers
{
    public static class NavigationHelper
    {
        public static void NavigateToUrl(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public static void RetryNavigation(IWebDriver driver, ChromeOptions options, string url)
        {
            options.AddArgument("--disable-blink-features=AutomationControlled");//This argument disables certain Blink features related to automation control in the Chrome browser.
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            NavigateToUrl(driver, url);
        }
    }
}
