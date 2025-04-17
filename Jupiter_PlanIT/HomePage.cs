using static System.Console;
using OpenQA.Selenium;

namespace WebDriver
{
  public class HomePage : DriverFactory
  {
    private static HomePage? pageInstance = null;
    private HomePage()
    {
      WriteLine("Instances created ");
    }

    public static HomePage Instance
    {
      get
      {
        if (pageInstance == null)
        {
          pageInstance = new HomePage();
        }
        return pageInstance;
      }
    }

    By Home = By.XPath("//a[text()='Home']");

    public void Navigate()
    {
        ReturnElement(Home).Click();
    }
  }
}