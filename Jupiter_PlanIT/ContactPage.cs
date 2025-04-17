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
    By Forename_warning = By.XPath("//span[text()='Forename is required']");
    By Email_warning = By.XPath("//span[text()='Email is required']");
    By Message_warning = By.XPath("//span[text()='Message is required']");

    By txtForename = By.Id("forename");
    By txtSurname = By.Id("surname");
    By txtEmail = By.Id("email");
    By txtTelephone = By.Id("telephone");
    By txtMessage = By.Id("message"); 
    string txtAppreciate = "//*[contains(text(), 'Thanks __NAME__')]";
    By btnBack = By.XPath("//a[contains(text(),'Back')]");
    By txtSending = By.XPath("//*[contains(text(), 'Sending Feedback')]");

    public void Navigate()
    {
        ReturnElement(Contact).Click();
    }

    public void Verify_Warnings_Visible()
    {
        Assert.IsTrue(IsElementExists(Forename_warning), "Warning is not visible");
        Assert.IsTrue(IsElementExists(Email_warning), "Warning is not visible");
        Assert.IsTrue(IsElementExists(Message_warning), "Warning is not visible");
    }

    public void Verify_Warnings_NotVisible()
    {
        Assert.IsFalse(IsElementExists(Forename_warning), "Forename Warning is visible");
        Assert.IsFalse(IsElementExists(Email_warning), "Email Warning is visible");
        Assert.IsFalse(IsElementExists(Message_warning), "Message Warning is visible");
    }

    public void Fill_Contact_Details(string foreName, string surName, string email, string telephone, string message)
    {
        SetText(txtForename, foreName);
        SetText(txtSurname, surName);
        SetText(txtEmail, email);
        SetText(txtTelephone, telephone);
        SetText(txtMessage, message);
    }

    public void Click_Submit()
    {
        ReturnElement(Submit).Click();
    }
    public void Click_Back()
    {
        ReturnElement(btnBack).Click();
    }
    public void VerifySuccessfullSentMessage(string foreName)
    {
      By by = By.XPath(txtAppreciate.Replace("__NAME__", foreName));
      WaitForElement(by, 30);
    }
  }
}