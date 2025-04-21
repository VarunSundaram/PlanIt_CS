using System.ComponentModel;
using WebDriver;
namespace PlanITTest;

public class Tests
{
    [OneTimeSetUp]
    public void Setup()
    {
        DriverFactory.Start();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        DriverFactory.Quit();
    }

    [Test, Sequential]
    public void Validate_Error_Message()
    {
        string foreName = "Test Forename";
        string surName = "Test Surname";
        string emailId = "test@jupiter.com";
        string teleNum = "0491 189 190";
        string msgToContact = "Test Message";
        ContactPage.Instance.Navigate();
        ContactPage.Instance.ClickSubmit();
        ContactPage.Instance.VerifyWarningsVisible();
        ContactPage.Instance.FillContactDetail(foreName, surName, emailId, teleNum, msgToContact);
        ContactPage.Instance.VerifyWarningsNotVisible();
    }

    [Test, Repeat(5), Sequential]
    public void Validate_Inquiry_Submit()
    {
        string foreName = "Test Forename";
        string surName = "Test Surname";
        string emailId = "test@jupiter.com";
        string teleNum = "0491 189 190";
        string msgToContact = "Test Message";
        ContactPage.Instance.Navigate();
        ContactPage.Instance.FillContactDetail(foreName, surName, emailId, teleNum, msgToContact);
        ContactPage.Instance.ClickSubmit();
        ContactPage.Instance.VerifySuccessfullSentMessage(foreName);
        ContactPage.Instance.ClickBack();
    }

    [Test]
    public void Validate_Cart_Total()
    {
        ShopPage.Instance.Navigate();
        decimal price1 = ShopPage.Instance.AddItemToCart_ReturnSubTotal(ShopPage.Instance.stuffedFrog, 2);
        ShopPage.Instance.VerifyCartItemCount(2);
        decimal price2 = ShopPage.Instance.AddItemToCart_ReturnSubTotal(ShopPage.Instance.fluffyBunny, 5);
        ShopPage.Instance.VerifyCartItemCount(7);
        decimal price3 = ShopPage.Instance.AddItemToCart_ReturnSubTotal(ShopPage.Instance.valentineBear, 3);
        ShopPage.Instance.VerifyCartItemCount(10);
        ShopPage.Instance.GotoCart();
        ShopPage.Instance.VerifyCartTotal(price1 + price2 + price3);
        ShopPage.Instance.VerifyCartSubTotal(ShopPage.Instance.stuffedFrog, price1);
        ShopPage.Instance.VerifyCartSubTotal(ShopPage.Instance.fluffyBunny, price2);
        ShopPage.Instance.VerifyCartSubTotal(ShopPage.Instance.valentineBear, price3);
    }
}
