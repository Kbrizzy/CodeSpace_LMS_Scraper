using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace selenium
{
    class Program
    {
        static void Main(string[] args)
        {
            ChromeOptions options = new ChromeOptions(); // optional
            options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            IWebDriver driver = new ChromeDriver("C:/Users/lenovo/Documents/Webdriver", options);

            driver.Navigate().GoToUrl("https://learn.codespace.co.za/login");
            Thread.Sleep(2000);

            driver.FindElement(By.CssSelector("input[type='email']")).SendKeys(Console.ReadLine());
            driver.FindElement(By.CssSelector("input[type='password']")).SendKeys(Console.ReadLine());

            // Find and click the login button
            driver.FindElement(By.ClassName("button")).Click();

            // Wait for the page to load
            Thread.Sleep(2000);

            // Find all links on the page
            IList<IWebElement> links = driver.FindElements(By.TagName("a"));

            // Create HttpClient instance
            HttpClient client = new HttpClient();

            // Iterate through each link and check if it's a broken link
            foreach (IWebElement link in links)
            {
                string url = link.GetAttribute("href");

                // Skip empty or null URLs
                if (string.IsNullOrEmpty(url))
                    continue;

                try
                {
                    HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();

                    // Check if the response is not null and the status is an error (4xx or 5xx)
                    if (response != null && !response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Broken link found: {url}");
                    }
                }
                catch (Exception)
                {
                    // An exception occurred, indicating a broken link
                    Console.WriteLine($"Broken link found: {url}");
                }
            }

            // Quit the driver
            //driver.Quit();
        }
    }
}