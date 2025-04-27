using MySql.Data.MySqlClient;

internal class Program
{
    public static void Main(string[] args)
    {
        MySqlConnection connection =
            new MySqlConnection("server=localhost;port=3306;database=Storage;uid=root;password=INnoVation");
        try
        {
            connection.Open();
            Console.WriteLine("Successfully connected database.");
            Console.WriteLine($"Database: {connection.Database}");

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Show all products");
                Console.WriteLine("2. Show all product types");
                Console.WriteLine("3. Show all suppliers");
                Console.WriteLine("4. Show products by category");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option (1-5): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowAllProducts(connection);
                        break;
                    case "2":
                        ShowAllProductTypes(connection);
                        break;
                    case "3":
                        ShowAllSuppliers(connection);
                        break;
                    case "4":
                        ShowProductsByCategory(connection);
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Error connecting to the database:");
            Console.WriteLine(ex.Message);
        }
        finally
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                Console.WriteLine("\nConnection closed.");
            }
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
}

    static void ShowAllProducts(MySqlConnection connection)
    {
        string query = @"
            SELECT 
                p.ProductName,
                pt.TypeName,
                s.SupplierName,
                p.Quantity,
                p.CostPrice,
                p.DeliveryDate
            FROM Products p
            JOIN ProductTypes pt ON p.TypeId = pt.Id
            JOIN Suppliers s ON p.SupplierId = s.Id";

        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();

        Console.WriteLine("All Products:");
        Console.WriteLine("------------------------------------------------");

        while (reader.Read())
        {
            Console.WriteLine($"Name: {reader["ProductName"]}, Type: {reader["TypeName"]}, Supplier: {reader["SupplierName"]}, Quantity: {reader["Quantity"]}, Cost: {reader["CostPrice"]}, Delivery Date: {Convert.ToDateTime(reader["DeliveryDate"]).ToShortDateString()}");
        }

        reader.Close();
    }

    static void ShowAllProductTypes(MySqlConnection connection)
    {
        string query = "SELECT * FROM ProductTypes";

        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();

        Console.WriteLine("All Product Types:");
        Console.WriteLine("------------------");

        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader["Id"]}, Type: {reader["TypeName"]}");
        }

        reader.Close();
    }

    static void ShowAllSuppliers(MySqlConnection connection)
    {
        string query = "SELECT * FROM Suppliers";

        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();

        Console.WriteLine("All Suppliers:");
        Console.WriteLine("--------------");

        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["SupplierName"]}");
        }

        reader.Close();
    }

    static void ShowProductsByCategory(MySqlConnection connection)
    {
        Console.Write("Enter the product type name: ");
        string typeName = Console.ReadLine();

        string query = @"
            SELECT 
                p.ProductName,
                pt.TypeName,
                s.SupplierName,
                p.Quantity,
                p.CostPrice,
                p.DeliveryDate
            FROM Products p
            JOIN ProductTypes pt ON p.TypeId = pt.Id
            JOIN Suppliers s ON p.SupplierId = s.Id
            WHERE pt.TypeName = @typeName";

        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@typeName", typeName);

        MySqlDataReader reader = command.ExecuteReader();

        Console.WriteLine($"Products in category '{typeName}':");
        Console.WriteLine("--------------------------------");

        bool found = false;
        while (reader.Read())
        {
            Console.WriteLine($"Name: {reader["ProductName"]}, Supplier: {reader["SupplierName"]}, Quantity: {reader["Quantity"]}, Cost: {reader["CostPrice"]}, Delivery Date: {Convert.ToDateTime(reader["DeliveryDate"]).ToShortDateString()}");
            found = true;
        }

        if (!found)
        {
            Console.WriteLine("No products found in this category.");
        }

        reader.Close();
    }
}