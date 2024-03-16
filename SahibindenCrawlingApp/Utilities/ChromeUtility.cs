using OpenQA.Selenium.Chrome;

namespace SahibindenCrawlingApp.Utilities
{
    public static class ChromeUtility
    {
        public static ChromeOptions ConfigureChromeOptions()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--incognito");//Open Chrome in incognito mode
            options.AddArgument("--disable-blink-features=AutomationControlled");
            return options;//
        }
    }
}
