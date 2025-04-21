using static System.Console;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebDriver
{
  public class DriverManager
  {
    // Initialize WebDriver
    static IWebDriver? driver = null;

    /// <summary>
    /// New driver manager instance is created for first time
    /// </summary>
    public static IWebDriver GetWebDriver(string message)
    {
        WriteLine("Message " + message);
        if (driver == null)
        {
            driver = Start();
        }
        return driver;
    }

    /// <summary>
    /// Selenium WebDriver is created.
    /// WebDriver options can be parameterized depending of the type of target platform
    /// Target platform can be Chrome, Edge, Safari and Firefox
    /// </summary>
    public static IWebDriver Start()
    {
        // Set Chrome options
        var options = new ChromeOptions();
        options.AddArguments("--start-maximized");
        options.AddArguments("--disable-infobars");
        // Initialize WebDriver
        driver = new ChromeDriver(options);
        // Open a webpage
        driver.Url = "https://jupiter.cloud.planittesting.com/";
        driver.Navigate();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        return driver;
    }
  }
}