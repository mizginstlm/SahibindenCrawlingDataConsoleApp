namespace SahibindenCrawlingApp.Helpers;
public static class FileHelper
{
    public static void WriteToTextFile(string filePath, List<WebPageModel> webPageModels, int maxTitleLength)
    {
        using (StreamWriter writer = new StreamWriter(filePath, append: true))//when append is true it will append the new data to the end of the file.
        {
            foreach (var model in webPageModels)
            {
                string title = model.Title;
                string price = model.Price;
                writer.WriteLine($"{title}" + new string(' ', maxTitleLength - title.Length + 4) + $"\t{price}");
            }
        }
        Console.WriteLine("Titles and prices have been written to 'AnasayfaVitrini_titles_and_prices.txt' file.");
    }
}
