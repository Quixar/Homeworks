using System.Threading.Channels;
using project.Data;
using project.Entities;

namespace project;

class Program
{
    private static DataContex _context = new();
    
    static void Main(string[] args)
    {
        var userService = new UserService(_context);

        while (true)
        {
            Console.WriteLine("\nChoose action:");
            Console.WriteLine("1 - Create new user");
            Console.WriteLine("2 - Show oldest user");
            Console.WriteLine("3 - Show last three registered users");
            Console.WriteLine("4 - Show roles with user counts");
            Console.WriteLine("0 - Exit");
            Console.Write("Choice: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    userService.CreateUser();
                    break;
                case "2":
                    userService.PrintOldestUser();
                    break;
                case "3":
                    userService.PrintLastThreeRegisteredUsers();
                    break;
                case "4":
                    userService.ShowRoleStatistics();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}