using System;
using ConsoleApp27;
using StoreApp.Exceptions;

class Program {
    static readonly Market market = new();

    static void Main(string[] args) {
        market.AlcoholPercentLimit = 50;
        string opt;
        do {
            opt = ChooseOperation();

            switch (opt) {
                case "1":
                    AddProduct();
                    break;
                case "2":
                    RemoveProduct();
                    break;
                case "3":
                    ShowProducts();
                    break;
                case "4":
                    EditProduct();
                    break;
                case "5":
                    SellProduct();
                    break;
                case "6":
                    ShowProfit();
                    break;
                case "7":
                    Console.WriteLine(market.AvgAlcoholPercent);
                    break;
                default:
                    break;
            }

        } while (opt != "0");
    }

    private static void ShowProfit() {
        Console.WriteLine("===== Show Profit =====");
        Console.WriteLine("Select the profit information to display:");
        Console.WriteLine("a. All product profit");
        Console.WriteLine("b. Alcohol product profit");
        Console.WriteLine("c. Non-alcohol product profit");
        Console.WriteLine("Choose an option:");

        string? option = Console.ReadLine();

        switch (option) {
            case "a":
                Console.WriteLine($"Profit for all products: ${market.GetAllProfit():0.00}");
                break;
            case "b":
                Console.WriteLine($"Profit for alcohol products: ${market.GetAlcoholProfit():0.00}");
                break;
            case "c":
                Console.WriteLine($"Profit for non-alcohol products: ${market.GetNonAlcoholProfit():0.00}");
                break;
            default:
                Console.WriteLine("Invalid option. Please select a valid option.");
                break;
        }
    }


    private static void SellProduct() {
        Console.WriteLine("===== Sell a Product =====");

        ShowProducts();

        Console.Write("Enter the product number to sell: ");
        if (!int.TryParse(Console.ReadLine(), out int productNo)) {
            Console.WriteLine("Invalid input! Please enter a valid product number.");
            return;
        }

        try {
            market.RemoveProductByNo(productNo);
            Console.WriteLine("Product sold successfully.");
        }
        catch (ProductNotFoundException) {
            Console.WriteLine($"Product with number {productNo} does not exist.");
        }
        catch (Exception ex) {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }


    private static void EditProduct() {
        int productNo;
        do {
            Console.Write("Product no: ");
        } while (!int.TryParse(Console.ReadLine(), out productNo));

        try {
            Product? product = market.FindByNo(productNo);
            if (product != null) {
                // Display the product information
                Console.WriteLine($"Name: {product.Name}");
                Console.WriteLine($"Cost: {product.CostPrice}");
                Console.WriteLine($"Sale: {product.SalePrice}");
                Console.WriteLine($"Expire date: {product.ExpireDate}");

                // For drink products, display additional information
                if (product is DrinkProduct) {
                    DrinkProduct drinkProduct = (DrinkProduct)product;
                    Console.WriteLine($"Alcohol percentage: {drinkProduct.AlcoholPercent}");
                }

                // Modify product information
                Console.WriteLine("Enter new information (press Enter to keep the current value):");

                Console.Write("New name: ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName))
                    product.Name = newName;

                double newCostPrice;
                Console.Write("New cost price: ");
                string newCostPriceStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newCostPriceStr) && double.TryParse(newCostPriceStr, out newCostPrice))
                    product.CostPrice = newCostPrice;

                double newSalePrice;
                Console.Write("New sale price: ");
                string newSalePriceStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newSalePriceStr) && double.TryParse(newSalePriceStr, out newSalePrice))
                    product.SalePrice = newSalePrice;

                DateTime newExpireDate;
                Console.Write("New expire date (MM/DD/YYYY): ");
                string newExpireDateStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newExpireDateStr) && DateTime.TryParse(newExpireDateStr, out newExpireDate))
                    product.ExpireDate = newExpireDate;

                if (product is DrinkProduct) {
                    double newAlcoholPercent;
                    Console.Write("New alcohol percentage: ");
                    string newAlcoholPercentStr = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newAlcoholPercentStr) && double.TryParse(newAlcoholPercentStr, out newAlcoholPercent)) {
                        ((DrinkProduct)product).AlcoholPercent = newAlcoholPercent;
                    }
                }

                Console.WriteLine("Product information updated successfully.");
            }
            else {
                Console.WriteLine($"Product with number {productNo} does not exist.");
            }
        }
        catch (Exception ex) {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }


    static string ChooseOperation() {
        ShowMenu();
        Console.Write("Operation: ");
        return Console.ReadLine();
    }

    static void ShowMenu() {
        Console.WriteLine();
        Console.WriteLine("1. Enter a product");
        Console.WriteLine("2. Remove a product");
        Console.WriteLine("3. See all products");
        Console.WriteLine("4. Edit a product");
        Console.WriteLine("5. Sell a product");
        Console.WriteLine("6. Calculate profit");
        Console.WriteLine("7. Calculate average alcohol percentage");
        Console.WriteLine("0. Exit");
        Console.WriteLine();
    }

    static void AddProduct() {
        Console.Write("Name: ");
        string name = Console.ReadLine();

        double costPrice, salePrice;
        DateTime expireDate;
        while (true) {
            Console.Write("Cost: ");
            if (!double.TryParse(Console.ReadLine(), out costPrice)) {
                Console.WriteLine("Invalid input! Please enter a valid number.");
                continue;
            }
            break;
        }

        while (true) {
            Console.Write("Sale: ");
            if (!double.TryParse(Console.ReadLine(), out salePrice)) {
                Console.WriteLine("Invalid input! Please enter a valid number.");
                continue;
            }
            break;
        }

        while (true) {
            Console.Write("Expire date (MM/DD/YYYY): ");
            if (!DateTime.TryParse(Console.ReadLine(), out expireDate)) {
                Console.WriteLine("Invalid input! Please enter a valid date.");
                continue;
            }
            break;
        }

        bool isDrink;
        while (true) {
            Console.Write("Is it a drink product? (y/n): ");
            string isDrinkStr = Console.ReadLine().ToLower();
            if (isDrinkStr == "y") {
                isDrink = true;
                break;
            }
            else if (isDrinkStr == "n") {
                isDrink = false;
                break;
            }
            else {
                Console.WriteLine("Invalid input! Please enter 'y' or 'n'.");
            }
        }

        double alcoholPercent = 0;
        if (isDrink) {
            while (true) {
                Console.Write("Alcohol percentage: ");
                if (!double.TryParse(Console.ReadLine(), out alcoholPercent)) {
                    Console.WriteLine("Invalid input! Please enter a valid number.");
                    continue;
                }
                break;
            }
        }

        Product product;
        if (isDrink)
            product = new DrinkProduct(name, salePrice, costPrice, expireDate, alcoholPercent);
        else
            product = new Product(name, salePrice, costPrice, expireDate);

        market.AddProduct(product);
    }

    static void ShowProducts() {
        Console.WriteLine("a. All products");
        Console.WriteLine("b. Alcoholic drinks");
        Console.WriteLine("c. Non-alcoholic drinks");
        Console.WriteLine("Select an option:");
        string showOpt = Console.ReadLine();

        switch (showOpt.ToLower()) {
            case "a":
                foreach (var product in market.Products)
                    Console.WriteLine(product);
                break;
            case "b":
                foreach (var product in market.GetAllAlcoholDrinks())
                    Console.WriteLine(product);
                break;
            case "c":
                foreach (var product in market.GetAllNonAlcoholDrinks())
                    Console.WriteLine(product);
                break;
            default:
                Console.WriteLine("Invalid option");
                break;
        }
    }

    static void RemoveProduct() {
        Console.WriteLine("======== Removal operation ========");
        foreach (var product in market.Products)
            Console.WriteLine(product);
        Console.WriteLine("Product number to remove:");
        if (!int.TryParse(Console.ReadLine(), out int no)) {
            Console.WriteLine("Invalid input! Please enter a valid number.");
            return;
        }

        try {
            market.RemoveProductByNo(no);
        }
        catch (ProductNotFoundException) {
            Console.WriteLine($"{no} is not a valid product number");
        }
        catch (ProductExpireDateException) {
            Console.WriteLine("The product's expiration date is more than a year away");
        }
        catch (Exception ex) {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
