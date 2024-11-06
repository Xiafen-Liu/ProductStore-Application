class Product
{
    private string productID;
    private string productCategoryName;
    public static string[] categoryCodes = { "e-", "b-", "a-", "t-", "o-" };
    public static string[] categoryNames = { "Electronics", "Books", "Apparel", "Toys", "Others" };

    public Product()
    {
    }
    public Product(string id, string name, int quantity, double price)
    {
        ProductID = id;
        ProductName = name;
        ProductQuantity = quantity;
        ProductPrice = price;
    }

    public string ProductName { get; set; }
    public int ProductQuantity { get; set; }
    public double ProductPrice { get; set; }
    public string ProductID
    {
        get { return productID; }
        set
        {
            //checking if the product id refers to a known category using a for loop
            //assigning value to ProductCategory accordingly
            string productIDPrefix = value.Substring(0, 2);
            bool isKnownCategory = false;
            for (int x = 0; x < categoryCodes.Length; x++)
            {
                if (productIDPrefix == categoryCodes[x])
                {
                    isKnownCategory = true;
                    productID = value;
                    productCategoryName = categoryNames[x];
                    break;
                }
            }
            if (!isKnownCategory)
            {
                productID = categoryCodes[4] + value.Substring(2);
                productCategoryName = categoryNames[4];
            }
        }
    }
    //ProductCategoryName is a readonly property
    public string ProductCategoryName
    {
        get { return productCategoryName; }
    }
    public override string ToString()
    {
        return ("product name: " + ProductName + "  " + "ID: " + ProductID + "  " + "quantity: " +
            ProductQuantity + "  " + "price: " + ProductPrice.ToString("C") + "  " + "category: " + ProductCategoryName);
    }
}