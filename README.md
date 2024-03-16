### Case: Sahibinden Crawling Application
In this study, a work has been carried out on reading some data from Sahibinden.Com using a Console Application and interpreting the data.


Tried to analize  Cloudflare's JavaScript challenge to understand the algorithm responsible for generating the challenge and validating the response. This way, I could reverse-engineer the script. In my chrome browser when I used developer debug tool XHR breakpoint gets triggered (before the first POST request is sent) And I get some results like these:


![devtool](./SahibindenCrawlingApp/img/devtool.png?raw=true "devtool")


As we see in above photo with slow 3G https://sahibinden.com website is Cloudflare-protected. User will first need to wait a few seconds in the Cloudflare waiting room. During that time, browser solves challenges to prove you're not a robot. If you're labeled as a bot, you'll be given a “Forbidden” or “Access Denied” error. Otherwise, you'll get automatically redirected to the actual web page. 



Cloud-Flare Challenge Platform:
![cloudflare](./SahibindenCrawlingApp/img/cloudflareplatform.png?raw=true "cloudflare")



A brief explanation from https://developers.cloudflare.com/waf/reference/cloudflare-challenges/ :  “Instead of simply replacing CAPTCHA with one alternative, we developed Cloudflare Managed Challenge, a platform to test multiple alternatives. When a visitor faces a Managed Challenge, we start with non-interactive JavaScript challenges to gather signals about the visitor's browser environment, deploying detections and challenges in-browser at the time of the request.”




Then I reexamined the source code using Chrome DevTools.
Example of one of Item in website source code:
![sourcecode](./SahibindenCrawlingApp/img/sourcecode.png?raw=true "sourcecode")





#### Setting Up the Project

 **Create a Dotnet Console App**: 
   ```powershell
   dotnet new console --framework net8.0 --use-program-main
   ```

 **Enter Project Directory**:
   ```powershell
   cd SahibindenCrawlingApp
   ```
   
   
   

#### Running the Application

To run the application, use the following command:
```powershell
dotnet run
```

Note: You better not touch the webpage until all data is extracted. Otherwise, Selenium detection might occur, leading to the application stopping if an exception happens.

While attempting direct access to a website using the HtmlAgilityPack library in C#, you may encounter issues with accessing specific nodes from an HTML document. This could be due to dynamic content loading via JavaScript, which HtmlAgilityPack may not handle effectively.




#### Troubleshooting Approach

When faced with issues, various approaches were explored:

 **HtmlAgilityPack Limitations**: The library may fail to capture dynamically generated content due to its inability to execute JavaScript.
 
 
 **Alternative Solutions**: Considering utilizing headless browsers, APIs provided by the website or other scraping tools.


Then tried to solve with Scrape, FlareSolverr, Capsolver, reverse engineering, headers, proxies, Selenium.



####  Selenium

Selenium was identified as a potential solution due to its capabilities in handling dynamic content. The following steps outline the process of integrating Selenium into the project:

 Research and Documentation Review: Explored Selenium's documentation to understand its features and integration possibilities.
 
 Package Installation: Installed the Selenium WebDriver package for .NET via NuGet Package Manager or .NET CLI.
 
 
 
 **Utilizing WebDriver**:
   - Navigated to the target website's URL using WebDriver.
   - Located and interacted with HTML elements on the webpage.
   
 **Handling Dynamic Content**: Employed Selenium's capabilities to wait for the page to fully render before interacting with elements.
 
 **Error Handling**: Implemented error handling mechanisms to manage exceptions during WebDriver interactions.



When running the app after these steps:
![runningapp](./SahibindenCrawlingApp/img/runningapp.png?raw=true "runningapp")



#### Further Attempts and Challenges
After several tries, I considered making a second attempt right after the first one. Surprisingly, this strategy seemed to solve the crawling problem.


Here are a few potential reasons why I might be able to get data after the second attempt:
1. **Timing**: The website's detection mechanism might be based on timing or frequency. By retrying the navigation after catching an exception, might be giving the website's detection mechanism enough time to "reset," allowing me to successfully navigate on the second attempt.

2. **Randomization**: Some websites implement randomized detection mechanisms. By retrying the navigation,  might end up hitting a window where the detection mechanism is not active, allowing you to navigate successfully.

3. **Session-based Detection**: The detection mechanism might be session-based. By retrying the navigation, might be starting a new session, which does not trigger the detection mechanism immediately.

4. **Browser Configuration**: It's possible that initial browser configuration might trigger the detection mechanism, but the subsequent attempt might have different configurations or settings that bypass the detection.

5. **Network Conditions**: Sometimes, network conditions or latency can affect how the detection mechanism behaves. Retrying the navigation might result in different network conditions that bypass the detection.


After my debug and tests 4 and 5 are seems like the reasons why I can crawle.


It's important to note that while we may currently be able to scrape data after the second attempt, there's no guarantee that this will continue to work reliably in the future. Websites often update their detection mechanisms to prevent scraping, and retrying navigation as a long-term solution may not be sustainable.



We can consider exploring alternative approaches to scraping data, such as using headless browsers, rotating user agents, or using proxies to bypass detection mechanisms more reliably. Additionally, we should ensure that scraping activities comply with the website's terms of service and legal requirements.


Running the entire code will add titles and prices to the existing file, causing duplication. So, it's best to use txt file only once for accurate results. If you want to run the app again you better delete the "AnasayfaVitrini_titles_and_prices.txt" file.


After implementing this solution, I began testing this approach to assess its reliability and to focus on further reasons and consequences. By ensuring necessary logging and error handling based on the outcomes, I achieved a favorable result.



Now that I can access the page and bypass Cloudflare, I retrieved the titles and prices from the source code, neatly displaying them both in a text file and the console. However, I hadn't considered how to access data on other pages. Initially, I thought it would be easy, I tried source html code to click nexpage button but the website recognized me. After numerous attempts and research, I couldn't find a solution other than repeatedly running Selenium WebDriver for each paging offset. Thus, I implemented code with loops to repeatedly run Selenium WebDriver for each page to access the data on those pages.

Txt file:
![txtfile](./SahibindenCrawlingApp/img/txtfile.png?raw=true "txtfile")

Terminal:
![Terminal](./SahibindenCrawlingApp/img/Terminal.png?raw=true "Terminal")

Actual Website apperance(these datas are same as in txt file and console:
![webpage](./SahibindenCrawlingApp/img/webpage.png?raw=true "webpage")

Throughout this process, I continuously reviewed my code and made adjustments, striving to improve it. At this stage, I needed to review my code again to write more efficient and principled code following best practices.

Thank you 🙋🏻‍♀️💻