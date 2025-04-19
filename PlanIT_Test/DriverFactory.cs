using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebDriver
{
  public class DriverFactory
  {
    private static DriverFactory? driverInstance = null;
    // Initialize WebDriver
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

    public static void Start()
    {
        if (driverInstance == null)
          driverInstance = new DriverFactory();
    }

    public static void Quit()
    {
        driver.Quit();
    }

    public void WaitForElement(By by, int seconds = 5)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
        wait.Until(ExpectedConditions.ElementIsVisible(by));
    }
    public bool IsElementExists(By by)
    {
        IList < IWebElement > elements = driver.FindElements(by);
        if (elements.Count > 0)
          return true;
        else
          return false;
    }

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

    public void SetText(By by, string text)
    {
      IWebElement element = ReturnElement(by);
      element.Clear();
      element.Click();
      element.SendKeys(text);
    }
  }
}