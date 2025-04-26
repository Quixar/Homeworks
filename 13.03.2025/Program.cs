using MySql.Data.MySqlClient;

internal class Program
{
    public static void Main(string[] args)
    {
        MySqlConnection connection =
            new MySqlConnection("server=localhost;port=3306;database=FruitsAndVegetables;uid=root;password=INnoVation");
        try
        {
            connection.Open();
            Console.WriteLine("Successfully connected to the database.");
            Console.WriteLine($"Database: {connection.Database}");

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Show all fruits and vegetables");
                Console.WriteLine("2. Show minimum calories");
                Console.WriteLine("3. Show count of vegetables");
                Console.WriteLine("4. Show products with calories less than specified");
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
                        ShowMinimumCalories(connection);
                        break;
                    case "3":
                        ShowVegetablesCount(connection);
                        break;
                    case "4":
                        ShowProductsWithCaloriesLessThan(connection);
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
        string query = "SELECT * FROM products";
        using MySqlCommand command = new MySqlCommand(query, connection);
        using MySqlDataReader reader = command.ExecuteReader();
        
        Console.WriteLine("All Fruits and Vegetables:");
        Console.WriteLine("--------------------------------------------");

        while (reader.Read())
        {
            Console.WriteLine($"Name: {reader["Name"]}, Type: {reader["Type"]}, Color: {reader["Color"]}, Calories: {reader["Calories"]}");
        }

        reader.Close();
    }
    
    static void ShowMinimumCalories(MySqlConnection connection)
    {
        string query = "SELECT MIN(Calories) FROM Products";

        MySqlCommand command = new MySqlCommand(query, connection);
        object result = command.ExecuteScalar();

        Console.WriteLine($"Minimum Calories: {result}");
    }

    static void ShowVegetablesCount(MySqlConnection connection)
    {
        string query = "SELECT COUNT(*) FROM Products WHERE Type = 'Vegetable'";

        MySqlCommand command = new MySqlCommand(query, connection);
        object result = command.ExecuteScalar();

        Console.WriteLine($"Number of Vegetables: {result}");
    }

    static void ShowProductsWithCaloriesLessThan(MySqlConnection connection)
    {
        Console.Write("Enter the maximum calories: ");
        string input = Console.ReadLine();
        if (int.TryParse(input, out int maxCalories))
        {
            string query = "SELECT Name, Type, Color, Calories FROM Products WHERE Calories < @maxCalories";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@maxCalories", maxCalories);

            MySqlDataReader reader = command.ExecuteReader();

            Console.WriteLine($"Products with calories less than {maxCalories}:");
            Console.WriteLine("--------------------------------------------");

            bool found = false;
            while (reader.Read())
            {
                Console.WriteLine($"Name: {reader["Name"]}, Type: {reader["Type"]}, Color: {reader["Color"]}, Calories: {reader["Calories"]}");
                found = true;
            }

            if (!found)
            {
                Console.WriteLine("No products found.");
            }

            reader.Close();
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}