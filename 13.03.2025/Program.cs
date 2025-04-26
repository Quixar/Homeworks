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
            Console.WriteLine("Connection established");
            Console.WriteLine($"BD {connection.Database}");
        }
        catch
        {
            Console.WriteLine("Connection Failed");
        }
        finally
        {
            connection.Close();
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}