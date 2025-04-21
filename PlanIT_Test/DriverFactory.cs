using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebDriver
{
  public class DriverFactory
  {
    private static DriverFactory? driverInstance = null;
    // Initialize WebDriver

    /// <summary>
    /// Chrome Web Driver is created.
    /// We can add capability for each browser by parameterizing the same
    /// </summary>
    protected static IWebDriver driver = DriverManager.GetWebDriver("chrome");

    public DriverFactory()
    {
    }

    protected static DriverFactory DriverInstance
    {
      get
      {
        if (driverInstance == null)
        {
          driverInstance = new DriverFactory();
        }
        return driverInstance;
      }
    }

    /// <summary>
    /// This method will create the webdriver
    /// </summary>
    public static void Start()
    {
        if (driverInstance == null)
          driverInstance = new DriverFactory();
    }

    /// <summary>
    /// This method will quit the webdriver
    /// </summary>
    public static void Quit()
    {
        driver.Quit();
    }

    /// <summary>
    /// This method will wait for element
    /// </summary>
    /// <param name="foreName">Forename of user</param>
    public void WaitForElement(By by, int seconds = 5)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
        wait.Until(ExpectedConditions.ElementIsVisible(by));
    }

    /// <summary>
    /// This method checks and returns 'true' of the item is visible
    /// </summary>
    /// <param name="foreName">Forename of user</param>
    public bool IsElementExists(By by)
    {
        IList < IWebElement > elements = driver.FindElements(by);
        if (elements.Count > 0)
          return true;
        else
          return false;
    }

    /// <summary>
    /// This method fill return first element of given locator
    /// </summary>
    /// <param name="foreName">Forename of user</param>
    public IWebElement ReturnElement(By by)
    {
      IWebElement webElement;
      try
      {
        WaitForElement(by, 10);
        webElement = driver.FindElement(by);
      }
      catch(NoSuchElementException ex)
      {
        throw new NoSuchElementException(ex.StackTrace);
      }
      catch(Exception ex)
      {
        Console.WriteLine(ex.StackTrace);
        throw new Exception(ex.StackTrace); 
      }
      return webElement;
    }

    /// <summary>
    /// This method fill return all items for given locator
    /// </summary>
    /// <param name="foreName">Forename of user</param>
    public IList<IWebElement> ReturnElements(By by)
    {
      try
      {
        WaitForElement(by);
        return driver.FindElements(by);
      }
      catch(NoSuchElementException ex)
      {
        throw new NoSuchElementException(ex.StackTrace);
      }
      catch(Exception ex)
      {
        Console.WriteLine(ex.StackTrace);
        throw new Exception(ex.StackTrace); 
      }
    }

    /// <summary>
    /// This method set text in given locator
    /// </summary>
    /// <param name="foreName">Forename of user</param>
    public void SetText(By by, string text)
    {
      IWebElement element = ReturnElement(by);
      element.Clear();
      element.Click();
      element.SendKeys(text);
    }
  }
}