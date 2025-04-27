using MySql.Data.MySqlClient;

internal class Program
{
    public static void Main(string[] args)
    {
        MySqlConnection connection =
            new MySqlConnection("server=localhost;port=3306;database=CoffeeShop;uid=root;password=INnoVation");

        try
        {
            connection.Open();
            Console.WriteLine("Successfully connected to the CoffeeShop database.");
            Console.WriteLine($"Server version: {connection.ServerVersion}");
            Console.WriteLine($"Database: {connection.Database}");

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Show all coffee information");
                Console.WriteLine("2. Show all coffee names");
                Console.WriteLine("3. Show minimum coffee cost price");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option (1-4): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowAllCoffee(connection);
                        break;
                    case "2":
                        ShowAllCoffeeNames(connection);
                        break;
                    case "3":
                        ShowMinimumCostPrice(connection);
                        break;
                    case "4":
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

    static void ShowAllCoffee(MySqlConnection connection)
    {
        string query = "SELECT * FROM Coffee";

        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();

        Console.WriteLine("All Coffee Information:");
        Console.WriteLine("------------------------------------------");

        while (reader.Read())
        {
            Console.WriteLine($"Name: {reader["CoffeeName"]}, Country: {reader["CountryOfOrigin"]}, Type: {reader["CoffeeType"]}, Description: {reader["Description"]}, Weight (g): {reader["WeightGrams"]}, Cost: {reader["CostPrice"]}");
        }

        reader.Close();
    }

    static void ShowAllCoffeeNames(MySqlConnection connection)
    {
        string query = "SELECT CoffeeName FROM Coffee";

        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();

        Console.WriteLine("Coffee Names:");
        Console.WriteLine("-------------");

        while (reader.Read())
        {
            Console.WriteLine(reader["CoffeeName"]);
        }

        reader.Close();
    }

    static void ShowMinimumCostPrice(MySqlConnection connection)
    {
        string query = "SELECT MIN(CostPrice) AS MinPrice FROM Coffee";

        MySqlCommand command = new MySqlCommand(query, connection);
        object result = command.ExecuteScalar();

        if (result != DBNull.Value)
        {
            Console.WriteLine($"Minimum Coffee Cost Price: {result} USD");
        }
        else
        {
            Console.WriteLine("No coffee data found.");
        }
    }
}