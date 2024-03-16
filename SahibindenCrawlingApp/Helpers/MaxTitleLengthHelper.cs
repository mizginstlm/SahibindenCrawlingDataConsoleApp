using OpenQA.Selenium;
namespace SahibindenCrawlingApp.Helpers
{
    public static class MaxTitleLengthHelper
    {

        // En uzun başlık uzunluğunu alır
        public static int GetMaxTitleLength(IReadOnlyCollection<IWebElement> items)
        {
            int maxTitleLength = 0;
            foreach (var item in items)
            {
                var titleElement = item.FindElement(By.CssSelector("a.classifiedTitle"));//Get this from website's source code
                string title = titleElement.Text;
                if (title.Length > maxTitleLength)
                    maxTitleLength = title.Length;
            }
            return maxTitleLength;
        }
    }
}
