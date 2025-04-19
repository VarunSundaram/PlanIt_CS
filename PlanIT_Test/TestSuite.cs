using System.ComponentModel;
using WebDriver;
namespace PlanIT_Test;

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
        string email = "test@jupiter.com";
        string telephone = "0491 189 190";
        string message = "Test Message";
        ContactPage.Instance.Navigate();
        ContactPage.Instance.Click_Submit();
        ContactPage.Instance.Verify_Warnings_Visible();
        ContactPage.Instance.Fill_Contact_Details(foreName, surName, email, telephone, message);
        ContactPage.Instance.Verify_Warnings_NotVisible();
    }

    [Test, Repeat(5), Sequential]
    public void Validate_Inquiry_Submit()
    {
        string foreName = "Test Forename";
        string surName = "Test Surname";
        string email = "test@jupiter.com";
        string telephone = "0491 189 190";
        string message = "Test Message";
        ContactPage.Instance.Navigate();
        ContactPage.Instance.Fill_Contact_Details(foreName, surName, email, telephone, message);
        ContactPage.Instance.Click_Submit();
        ContactPage.Instance.VerifySuccessfullSentMessage(foreName);
        ContactPage.Instance.Click_Back();
    }

    [Test]
    public void Validate_Cart_Total()
    {
        ShopPage.Instance.Navigate();
        decimal price1 = ShopPage.Instance.AddItemToCart_ReturnSubTotal(ShopPage.Instance.Stuffed_frog, 2);
        ShopPage.Instance.Verify_Cart_Item_Count(2);
        decimal price2 = ShopPage.Instance.AddItemToCart_ReturnSubTotal(ShopPage.Instance.Fluffy_Bunny, 5);
        ShopPage.Instance.Verify_Cart_Item_Count(7);
        decimal price3 = ShopPage.Instance.AddItemToCart_ReturnSubTotal(ShopPage.Instance.Valentine_Bear, 3);
        ShopPage.Instance.Verify_Cart_Item_Count(10);
        ShopPage.Instance.GotoCart();
        ShopPage.Instance.Verify_Cart_Total(price1 + price2 + price3);
        ShopPage.Instance.Verify_SubTotal_Total(ShopPage.Instance.Stuffed_frog, price1);
        ShopPage.Instance.Verify_SubTotal_Total(ShopPage.Instance.Fluffy_Bunny, price2);
        ShopPage.Instance.Verify_SubTotal_Total(ShopPage.Instance.Valentine_Bear, price3);
    }
}
