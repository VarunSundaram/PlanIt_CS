using static System.Console;
using OpenQA.Selenium;

namespace WebDriver
{
  public class ShopPage : DriverFactory
  {
    private static ShopPage? pageInstance = null;
    private ShopPage()
    {
      WriteLine("Instances created ");
    }

    public static ShopPage Instance
    {
      get
      {
        if (pageInstance == null)
        {
          pageInstance = new ShopPage();
        }
        return pageInstance;
      }
    }

    By Shop = By.XPath("//a[text()='Shop']");
    public string stuffedFrog
    {
        get
        {
            return "Stuffed Frog";
        }
    }
    public string handmadeDoll
    {
        get
        {
            return "Handmade Doll";
        }
    }
    public string fluffyBunny
    {
        get
        {
            return "Fluffy Bunny";
        }
    }
    public string smileyBear
    {
        get
        {
            return "Smiley Bear";
        }
    }
    public string valentineBear
    {
        get
        {
            return "Valentine Bear";
        }
    }
    string shopItem = "//h4[text()='__ITEM_NAME__']";
    string btnBuy = "//following-sibling::p/a";
    string txtPrice = "//following-sibling::p/span";
    By txtItemsCartCount = By.XPath("//*[text()='Cart (']//span");
    By txtTotalPrice = By.XPath("//*[contains(text(),'Total: ')]");
    string txtSubTotal = "//td[contains(text(),'__ITEM_NAME__')]//parent::tr//td[4]";

    /// <summary>
    /// This method navigate application to Shop page
    /// </summary>
    public void Navigate()
    {
        ReturnElement(Shop).Click();
    }

    /// <summary>
    /// This method add Item to the card minimum one time. Then returns the sub total price
    /// </summary>
    /// <param name="itemName">item name</param>
    /// <param name="count">N times the item must be added</param>
    public decimal AddItemToCart_ReturnSubTotal(string itemName, int count = 1)
    {
        decimal price = 0;
        By button = By.XPath(shopItem.Replace("__ITEM_NAME__", itemName) + btnBuy);
        By text = By.XPath(shopItem.Replace("__ITEM_NAME__", itemName) + txtPrice);
        IWebElement shopitem = ReturnElement(button);
        IWebElement cost = ReturnElement(text);
        decimal sPrice = decimal.Parse(cost.Text.ToString().Replace("$",""));
        do
        {
            count--;
            shopitem.Click();
            price = Math.Round(price + sPrice, 2);
        }while(count > 0);
        return price;
    }
    /// <summary>
    /// This method verifies count of items in the cart
    /// </summary>
    /// <param name="expectedCount">expected count to be verified</param>
    public void VerifyCartItemCount(int expectedCount)
    {
        int count = int.Parse(ReturnElement(txtItemsCartCount).Text);
        Assert.That(count==expectedCount, "Expected Item count in the cart is not matching");
    }

    /// <summary>
    /// This method navigates to aplication to Cart Page
    /// </summary>
    public void GotoCart()
    {
        ReturnElement(txtItemsCartCount).Click();
    }

    /// <summary>
    /// This method verifies the total price in the cart
    /// </summary>
    /// <param name="expectedTotal">expected total to be verified</param>
    public void VerifyCartTotal(decimal expectedTotal)
    {
        string total = ReturnElement(txtTotalPrice).Text;
        Assert.That(total.Contains(expectedTotal.ToString("#.#")), "Expected Price in the cart is not matching");
    }

    /// <summary>
    /// This method verifies sub total price of given item
    /// </summary>
    /// <param name="itemName">item name to which sub total should be verified</param>
    /// <param name="expectedSubTotal">expected total to be verified</param>
    public void VerifyCartSubTotal(string itemName, decimal expectedSubTotal)
    {
        By item = By.XPath(txtSubTotal.Replace("__ITEM_NAME__", itemName));
        string subTotal = ReturnElement(item).Text;
        Assert.That(subTotal.Contains(expectedSubTotal.ToString())
        , string.Format("Expected Sub Total Price for the item {0} is not matching", itemName));
    }
  }
}