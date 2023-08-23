using System;
using System.Collections.Generic;

class Program
{
    static List<ProductType> productTypes = new List<ProductType>
    {
        ProductType.Apparel,
        ProductType.Potions,
        ProductType.EnchantedObjects,
        ProductType.Wands
    };

    static Dictionary<ProductType, List<Product>> productsByType = new Dictionary<ProductType, List<Product>>();

    static void Main(string[] args)
    {
        InitializeProducts();

        while (true)
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine("1. View all products");
            Console.WriteLine("2. Add a product");
            Console.WriteLine("3. Delete a product");
            Console.WriteLine("4. Update a product");
            Console.WriteLine("5. View Products By Type");
            Console.WriteLine("6. Exit");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ViewAllProducts();
                    break;
                case 2:
                    AddProduct();
                    break;
                case 3:
                    DeleteProduct();
                    break;
                case 4:
                    UpdateProduct();
                    break;
                case 5:
                    ViewByType();
                    break;
                case 6:
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please choose a valid option.");
                    break;
            }
        }
    }

    static void InitializeProducts()
    {
        foreach (ProductType type in productTypes)
        {
            productsByType[type] = new List<Product>();
        }

        productsByType[ProductType.Apparel].Add(new Product("Cloak", 29.99m, true, ProductType.Apparel, DateTime.Now.AddDays(-10)));
        productsByType[ProductType.Potions].Add(new Product("Health Potion", 5.99m, true, ProductType.Potions, DateTime.Now.AddDays(-5)));
        productsByType[ProductType.EnchantedObjects].Add(new Product("Crystal Ball", 49.99m, true, ProductType.EnchantedObjects, DateTime.Now.AddDays(-15)));
        productsByType[ProductType.Wands].Add(new Product("Magic Wand", 89.99m, true, ProductType.Wands, DateTime.Now.AddDays(-20)));
    }

    static void ViewByType()
    {
        Console.WriteLine("Product Types: ");
        foreach (ProductType type in productTypes)
        {
            Console.WriteLine($"{type.Id}. {type.Name}");
        }
        Console.Write("Choose product type ID: ");
        int typeId = int.Parse(Console.ReadLine());

        ProductType selectedType = productTypes.Find(type => type.Id == typeId);
        if (productsByType.ContainsKey(selectedType))
        {
            Console.WriteLine($"Products of type '{selectedType.Name}': ");
            foreach (Product product in productsByType[selectedType])
            {
                Console.WriteLine($"Product Name: {product.Name}");
                Console.WriteLine($"Price: {product.Price:C}");
                Console.WriteLine($"Available: {product.IsAvailable}");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine($"No products found for the type '{selectedType.Name}");
        }
    }

    static void ViewAllProducts()
    {
        foreach (List<Product> productList in productsByType.Values)
        {
            foreach (Product product in productList)
            {
                Console.WriteLine($"Product Name: {product.Name}");
                Console.WriteLine($"Price: {product.Price:C}");
                Console.WriteLine($"Available: {product.IsAvailable}");
                Console.WriteLine($"Product Type: {product.ProductType.Name}");
                Console.WriteLine($"Days on Shelf: {product.DaysOnShelf} days ");
                Console.WriteLine();
            }
        }
    }

    static void AddProduct()
    {
        Console.Write("Enter product name: ");
        string name = Console.ReadLine();

        Console.Write("Enter product price: ");
        decimal price = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Product Types:");
        foreach (ProductType type in productTypes)
        {
            Console.WriteLine($"{type.Id}. {type.Name}");
        }
        Console.Write("Choose product type ID: ");
        int typeId = int.Parse(Console.ReadLine());

        ProductType selectedType = productTypes.Find(type => type.Id == typeId);

        productsByType[selectedType].Add(new Product(name, price, true, selectedType, DateTime.Now));
        Console.WriteLine("Product added successfully.");
    }

    static void DeleteProduct()
    {
        Console.Write("Enter the name of the product to delete: ");
        string productName = Console.ReadLine();

        foreach (var productList in productsByType.Values)
        {
            Product productToRemove = productList.Find(product => product.Name == productName);
            if (productToRemove != null)
            {
                productList.Remove(productToRemove);
                Console.WriteLine($"Product '{productName}' has been deleted.");
                return; // Exit the method after deletion
            }
        }

        Console.WriteLine($"Product '{productName}' not found.");
    }

    static void UpdateProduct()
    {
        Console.Write("Enter the name of the product to update: ");
        string productName = Console.ReadLine();

        foreach (var productList in productsByType.Values)
        {
            Product productToUpdate = productList.Find(product => product.Name == productName);
            if (productToUpdate != null)
            {
                Console.Write("Enter new price: ");
                decimal newPrice = decimal.Parse(Console.ReadLine());

                productToUpdate.Price = newPrice;
                Console.WriteLine($"Product '{productName}' has been updated with new price: {newPrice}");
                return; // Exit the method after update
            }
        }

        Console.WriteLine($"Product '{productName}' not found.");
    }


    class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public ProductType ProductType { get; set; }
        public DateTime DateStocked { get; set; }
        public int DaysOnShelf
        {
            get
            {
                TimeSpan timeOnShelf = DateTime.Now - DateStocked;
                return timeOnShelf.Days;
            }
        }
        public Product(string name, decimal price, bool isAvailable, ProductType productType, DateTime dateStocked)
        {
            Name = name;
            Price = price;
            IsAvailable = isAvailable;
            ProductType = productType;
            DateStocked = dateStocked;
        }
    }

    class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static ProductType Apparel = new ProductType { Id = 1, Name = "Apparel" };
        public static ProductType Potions = new ProductType { Id = 2, Name = "Potions" };
        public static ProductType EnchantedObjects = new ProductType { Id = 3, Name = "Enchanted Objects" };
        public static ProductType Wands = new ProductType { Id = 4, Name = "Wands" };
    }
}
