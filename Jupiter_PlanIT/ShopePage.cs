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
    public string Stuffed_frog
    {
        get
        {
            return "Stuffed Frog";
        }
    }
    public string Handmade_Doll
    {
        get
        {
            return "Handmade Doll";
        }
    }
    public string Fluffy_Bunny
    {
        get
        {
            return "Fluffy Bunny";
        }
    }
    public string Smiley_Bear
    {
        get
        {
            return "Smiley Bear";
        }
    }
    public string Valentine_Bear
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

    public void Navigate()
    {
        ReturnElement(Shop).Click();
    }
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
    public void Verify_Cart_Item_Count(int expectedCount)
    {
        int count = int.Parse(ReturnElement(txtItemsCartCount).Text);
        Assert.IsTrue(count==expectedCount, "Expected Item count in the cart is not matching");
    }
    public void GotoCart()
    {
        ReturnElement(txtItemsCartCount).Click();
    }
    public void Verify_Cart_Total(decimal expectedTotal)
    {
        string total = ReturnElement(txtTotalPrice).Text;
        Assert.IsTrue(total.Contains(expectedTotal.ToString("#.#")), "Expected Price in the cart is not matching");
    }
    public void Verify_SubTotal_Total(string itemName, decimal expectedSubTotal)
    {
        By item = By.XPath(txtSubTotal.Replace("__ITEM_NAME__", itemName));
        string subTotal = ReturnElement(item).Text;
        Assert.IsTrue(subTotal.Contains(expectedSubTotal.ToString())
        , string.Format("Expected Sub Total Price for the item {0} is not matching", itemName));
    }
  }
}