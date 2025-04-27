using System;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

internal class Program
{
    public static void Main(string[] args)
    {
        string connectionString = "server=localhost;port=3306;database=FruitsAndVegetables;uid=root;password=INnoVation";

        DbProviderFactories.RegisterFactory("MySql.Data.MySqlClient", MySqlClientFactory.Instance);
        DbProviderFactory factory = DbProviderFactories.GetFactory("MySql.Data.MySqlClient");

        using (DbConnection connection = factory.CreateConnection())
        {
            if (connection == null)
            {
                Console.WriteLine("Error creating a connection object.");
                return;
            }

            connection.ConnectionString = connectionString;

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
                    Console.WriteLine("4. Show items with calories lower than a value");
                    Console.WriteLine("5. Exit");
                    Console.Write("Choose an option (1-5): ");
                    string choice = Console.ReadLine();
                    Console.WriteLine();

                    switch (choice)
                    {
                        case "1":
                            ShowAllItems(connection);
                            break;
                        case "2":
                            ShowMinimumCalories(connection);
                            break;
                        case "3":
                            ShowVegetableCount(connection);
                            break;
                        case "4":
                            ShowItemsBelowCalories(connection);
                            break;
                        case "5":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice, try again.");
                            break;
                    }
                }
            }
            catch (DbException ex)
            {
                Console.WriteLine("Error connecting to the database:");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("\nConnection closed.");
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

    static void ShowAllItems(DbConnection connection)
    {
        string query = "SELECT * FROM Products";
        DbCommand command = connection.CreateCommand();
        command.CommandText = query;

        using (DbDataReader reader = command.ExecuteReader())
        {
            Console.WriteLine("All Fruits and Vegetables:");
            Console.WriteLine("---------------------------");

            while (reader.Read())
            {
                Console.WriteLine($"Name: {reader["Name"]}, Type: {reader["Type"]}, Color: {reader["Color"]}, Calories: {reader["Calories"]}");
            }
        }
    }

    static void ShowMinimumCalories(DbConnection connection)
    {
        string query = "SELECT MIN(Calories) AS MinCalories FROM Products";
        DbCommand command = connection.CreateCommand();
        command.CommandText = query;

        object result = command.ExecuteScalar();
        if (result != DBNull.Value)
        {
            Console.WriteLine($"Minimum Calories: {result}");
        }
        else
        {
            Console.WriteLine("No data available.");
        }
    }

    static void ShowVegetableCount(DbConnection connection)
    {
        string query = "SELECT COUNT(*) FROM Products WHERE Type = 'Vegetable'";
        DbCommand command = connection.CreateCommand();
        command.CommandText = query;

        object result = command.ExecuteScalar();
        Console.WriteLine($"Number of Vegetables: {result}");
    }

    static void ShowItemsBelowCalories(DbConnection connection)
    {
        Console.Write("Enter the maximum number of calories: ");
        if (int.TryParse(Console.ReadLine(), out int maxCalories))
        {
            string query = "SELECT * FROM Products WHERE Calories < @calories";
            DbCommand command = connection.CreateCommand();
            command.CommandText = query;

            DbParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@calories";
            parameter.Value = maxCalories;
            command.Parameters.Add(parameter);

            using (DbDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine($"Items with calories less than {maxCalories}:");
                Console.WriteLine("-----------------------------------------------");

                bool found = false;
                while (reader.Read())
                {
                    Console.WriteLine($"Name: {reader["Name"]}, Type: {reader["Type"]}, Color: {reader["Color"]}, Calories: {reader["Calories"]}");
                    found = true;
                }

                if (!found)
                {
                    Console.WriteLine("No items found with calories below specified value.");
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }
}