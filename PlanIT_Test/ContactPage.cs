using OpenQA.Selenium;

namespace WebDriver
{
  public class ContactPage : DriverFactory
  {
    private static ContactPage? pageInstance = null;
    
    private ContactPage()
    {
    }

    public static ContactPage Instance
    {
      get
      {
        if (pageInstance == null)
        {
          pageInstance = new ContactPage();
        }
        return pageInstance;
      }
    }

    By Contact = By.XPath("//a[text()='Contact']");
    By Submit = By.XPath("//a[text()='Submit']");
    By forenameWarning = By.XPath("//span[text()='Forename is required']");
    By emailWarning = By.XPath("//span[text()='Email is required']");
    By messageWarning = By.XPath("//span[text()='Message is required']");

    By userForename = By.Id("forename");
    By userSurname = By.Id("surname");
    By userEmail = By.Id("email");
    By userTelephone = By.Id("telephone");
    By userMessage = By.Id("message"); 
    string appreciateNotify = "//*[contains(text(), 'Thanks __NAME__')]";
    By backButton = By.XPath("//a[contains(text(),'Back')]");
    By sendingFeedback = By.XPath("//*[contains(text(), 'Sending Feedback')]");

    /// <summary>
    /// This method navigates application to Contact page
    /// </summary>
    public void Navigate()
    {
        ReturnElement(Contact).Click();
    }

    /// <summary>
    /// This method verifies the warnings that must be visible in the form
    /// </summary>
    public void VerifyWarningsVisible()
    {
        Assert.That(IsElementExists(forenameWarning), "Warning is not visible");
        Assert.That(IsElementExists(emailWarning), "Warning is not visible");
        Assert.That(IsElementExists(messageWarning), "Warning is not visible");
    }

    /// <summary>
    /// This method verifies the warning that must not be visible in the form
    /// </summary>
    public void VerifyWarningsNotVisible()
    {
        Assert.That(!IsElementExists(forenameWarning), "Forename Warning is visible");
        Assert.That(!IsElementExists(emailWarning), "Email Warning is visible");
        Assert.That(!IsElementExists(messageWarning), "Message Warning is visible");
    }

    /// <summary>
    /// This method fill contact details of user
    /// </summary>
    /// <param name="foreName">Forename of user</param>
    /// <param name="surName">Surname of user</param>
    /// <param name="emailId">Email Id of user</param>
    /// <param name="teleNumber">Telephone number of user</param>
    /// <param name="msgToContact">Message from user</param>
    public void FillContactDetail(string foreName, string surName, string emailId, string teleNumber, string msgToContact)
    {
        SetText(userForename, foreName);
        SetText(userSurname, surName);
        SetText(userEmail, emailId);
        SetText(userTelephone, teleNumber);
        SetText(userMessage, msgToContact);
    }

    /// <summary>
    /// Submit the form
    /// </summary>
    public void ClickSubmit()
    {
        ReturnElement(Submit).Click();
    }

    /// <summary>
    /// Navigate back
    /// </summary>
    public void ClickBack()
    {
        ReturnElement(backButton).Click();
    }

    /// <summary>
    /// Verify feedback submit message for given user
    /// </summary>
    public void VerifySuccessfullSentMessage(string foreName)
    {
      By by = By.XPath(appreciateNotify.Replace("__NAME__", foreName));
      WaitForElement(by, 40);
    }
  }
}