using System;

namespace student;

enum TaxType
{
    None,
    VAT10,
    VAT20
}

struct Product
{
    public string Name;
    public int Quantity;
    public double Price;
    public double Discount;

    public Product(string name, int quantity, double price, double discount)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
        Discount = discount;
    }

    public double ProductPrice()
    {
        return (Price * Quantity) - Discount;
    }
}

struct Receipt
{
    public string StoreName;
    public string Address;
    public DateTime Date;
    public TaxType Tax;
    public List<Product> Products;

    public Receipt(string storeName, string address, TaxType tax)
    {
        StoreName = storeName;
        Address = address;
        Date = DateTime.Now;
        Tax = tax;
        Products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public double TotalPrice()
    {
        double total = 0;
        foreach (Product product in Products)
        {
            total += product.ProductPrice();
        }
        return total;
    }

    public void Print()
    {
        System.Console.WriteLine("Shop: " + StoreName);
        System.Console.WriteLine("Address: " + Address);
        System.Console.WriteLine("Date: " + Date);
        System.Console.WriteLine("------------------------------------");

        foreach(Product product in Products)
        {
            System.Console.WriteLine(product.Name + "        " + product.Quantity + " X " + product.Price);
        }
        System.Console.WriteLine("------------------------------------");
        System.Console.WriteLine("Total: " + TotalPrice());
        System.Console.WriteLine("------------------------------------");
        System.Console.WriteLine(Tax);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Receipt r = new Receipt("Aushan", "address", TaxType.VAT10);
        r.AddProduct(new Product("Apple", 2, 50.00, 10.00));
        r.AddProduct(new Product("Bread", 1, 60.00, 0));
        r.AddProduct(new Product("Egg", 3, 30.00, 5.00));
        r.Print();
    }
}