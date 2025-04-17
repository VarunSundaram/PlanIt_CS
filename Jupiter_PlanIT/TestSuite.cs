using System.Runtime.Versioning;
using WebDriver;
namespace Jupiter_PlanIT;

[TestClass]
public sealed class Jupiter_PlanIT
{
    [ClassInitialize]
    public static void InitializeSuite(TestContext testContext)
    {
        DriverFactory.Start();
    }
    [ClassCleanup]
    public static void CleanupSuite()
    {
        DriverFactory.Quit();
    }
    [TestInitialize]
    public void BeginTest()
    {
        
    }
    [TestCleanup]
    public void EndTest()
    {
    }

    [TestMethod, DoNotParallelize]
    [DataRow ("Test Forename", "Test Surname", "test@jupiter.com", "0491 189 190", "Test Message")]
    public void Validate_Error_Message(string foreName, string surName, string email, string telephone, string message)
    {
        ContactPage.Instance.Navigate();
        ContactPage.Instance.Click_Submit();
        ContactPage.Instance.Verify_Warnings_Visible();
        ContactPage.Instance.Fill_Contact_Details(foreName, surName, email, telephone, message);
        ContactPage.Instance.Verify_Warnings_NotVisible();
    }

    [TestMethod, DoNotParallelize]
    [DataRow ("Test Forename1", "Test Surname", "test1@jupiter.com", "0491 189 190", "Test Message1")]
    [DataRow ("Test Forename2", "", "test2@jupiter.com", "0491 189 190", "Test Message2")]
    [DataRow ("Test Forename3", "Test Surname", "test3@jupiter.com", "", "Test Message3")]
    [DataRow ("Test Forename4", "", "test4@jupiter.com", "", "Test Message4")]
    [DataRow ("Test Forename5", "Test Surname", "test5@jupiter.com", "0491 189 190", "Test Message5")]
    public void Validate_Inquiry_Submit(string foreName, string surName, string email, string telephone, string message)
    {
        ContactPage.Instance.Navigate();
        ContactPage.Instance.Fill_Contact_Details(foreName, surName, email, telephone, message);
        ContactPage.Instance.Click_Submit();
        ContactPage.Instance.VerifySuccessfullSentMessage(foreName);
        ContactPage.Instance.Click_Back();
        Thread.Sleep(1000);
    }

    [TestMethod, DoNotParallelize]
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
