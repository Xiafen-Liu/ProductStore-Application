using static System.Console;
class Program
{
    static void Main(string[] args)
    {

        int productNum;
        double productRevenue;
        const int LOWER_BOUND = 1, UPPER_BOUND = 30;
        //call method InputValue to get value for productNum 
        productNum = InputValue(LOWER_BOUND, UPPER_BOUND);
        Product[] productsArray = new Product[productNum];
        //call method getProductData to fill an array of products
        GetProductData(productsArray);
        DisplayAllProducts(productsArray);
        productRevenue = CalculateRevenue(productsArray);
        WriteLine("The total revenue from selling all products is {0}.", productRevenue.ToString("C"));
        WriteLine();
        GetProductLists(productsArray);

    }

    public static int InputValue(int min, int max)
    {
        int productNumber = 0;
        bool isNumericValue;
        bool isValidNum = false;
        //using an indefinite loop to keep prompting user until a valid input is entered
        while (!isValidNum)
        {
            Write("Please enter the number of products>> ");
            isNumericValue = int.TryParse(ReadLine(), out productNumber);
            if (!isNumericValue || productNumber < min || productNumber > max)
            {
                WriteLine("     Invalid! Input needs to be a number and between 1 and 30 inclusive. Please re-enter.");
            }
            else
            {
                isValidNum = true;
            }
        }
        WriteLine();
        return productNumber;
    }

    private static void GetProductData(Product[] products)
    {
        string pID = "", pName;
        int pQuantity;
        double pPrice;
        //using a "for" loop to get user input for products
        //then create and initialise products and add products to products array
        WriteLine("Please enter product information here.");
        WriteLine("Product Id must contain code for one of the following categories:");
        //calling the DisplayCategories method to display product categories
        DisplayCategories();
        WriteLine();
        for (int x = 0; x < products.Length; x++)
        {

            //using an indefinite loop to keep prompting user until a valid id is entered
            bool isValidID = false;
            while (!isValidID)
            {
                Write("Enter id for product {0} here>> ", x + 1);
                pID = ReadLine();
                //call method CheckString to check id validity
                if (!CheckString(pID))
                {
                    WriteLine("     Invalid input! Please re-enter id for product.");
                    WriteLine();
                }
                else
                {
                    isValidID = true;
                }
            }

            Write("Please enter name for product {0}>> ", x + 1);
            pName = ReadLine();

            Write("Please enter quantity for product {0}>> ", x + 1);
            pQuantity = int.Parse(ReadLine());

            Write("Please enter price for product {0}>> ", x + 1);
            pPrice = double.Parse(ReadLine());
            WriteLine();
            //use the parameterised Product constructor to create and initialise instances of Product and
            // and assign to each element in products array
            products[x] = new Product(pID, pName, pQuantity, pPrice);
        }
    }

    public static bool CheckString(string id)
    {
        bool isValidID = false;
        const int LENGTH = 4;
        const string SECOND_CHAR = "-";
        //using 5 "if" conditions to check if the id length is 4, first letter is lowercase,
        //second is hyphen and last two are digits 
        if (id.Length == LENGTH && char.IsLower(id, 0) && id.Substring(1, 1) == SECOND_CHAR
            && char.IsNumber(id, 2) && char.IsNumber(id, 3))
        {
            isValidID = true;
        }
        return isValidID;
    }

    public static void DisplayAllProducts(Product[] products)
    {
        for (int y = 0; y < products.Length; y++)
        {
            //call the overriden ToString method from Product class to display product information
            WriteLine("Information for product {0}:", y + 1);
            WriteLine(products[y].ToString());
            WriteLine();
        }
    }

    public static double CalculateRevenue(Product[] products)
    {
        double revenue = 0;
        for (int sub = 0; sub < products.Length; sub++)
        {
            revenue = revenue + products[sub].ProductPrice * products[sub].ProductQuantity;
        }
        return revenue;
    }

    private static void GetProductLists(Product[] products)
    {
        string inputCode;
        const string EXIT = "!";
        bool isNotExiting;
        int numberOfProducts = 0;
        //calling the DisplayCategories method to display product categories
        DisplayCategories();
        WriteLine();
        //using a "do..while" loop to keep prompting user for input of category code or "!" to exit
        do
        {
            Write("Please enter a category code from one of the above codes or ! to quit>> ");
            inputCode = ReadLine();
            isNotExiting = inputCode != EXIT;
            if (isNotExiting)
            {
                bool isValidCode = false;
                foreach (string code in Product.categoryCodes)
                {
                    if (inputCode == code)
                    {
                        isValidCode = true;
                        break;
                    }
                }
                if (!isValidCode)
                {
                    WriteLine("     Invalid input. Please re-enter!");
                }
                //get list of products in the input category and calculate their number
                else
                {
                    WriteLine("Here is a list of products in this category:");
                    for (int index = 0; index < products.Length; index++)
                    {
                        if (products[index].ProductID.Substring(0, 2) == inputCode)
                        {
                            WriteLine("     {0}", products[index].ProductName);
                            numberOfProducts++;
                        }
                    }
                    //message to display when there is no product in the input category
                    if (numberOfProducts == 0)
                    {
                        WriteLine("There is no product in this category!");
                    }
                    //display number of products in the input category
                    else
                    {
                        WriteLine("There are {0} product(s) in this category!", numberOfProducts);
                        numberOfProducts = 0;
                    }
                }
            }
        }
        while (isNotExiting);
    }

    //creating a method to display a list of valid product categories 
    public static void DisplayCategories()
    {
        foreach (string category in Product.categoryNames)
        {
            Write("{0, 10}", category);
        }
        WriteLine();
        foreach (string code in Product.categoryCodes)
        {
            Write("{0, 10}", code);
        }
        WriteLine();
    }
}

