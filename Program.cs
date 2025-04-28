using System.Threading.Channels;
using project.Data;
using project.Entities;

namespace project;

class Program
{
    private static DataContex _context = new();
    
    static void Main(string[] args)
    {
        var userCount  = _context.Users.Count();
        
        if (userCount == 0)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test Test",
                Email = "test@test.com",
                Birthdate = new DateTime(2006, 07, 17),
                RegisteredAt = DateTime.Now
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            Console.WriteLine("User was created.");
        }
        else
        {
            var query = _context.Users.Where(u => u.Birthdate <= DateTime.Now);
            Console.WriteLine($"Table has {query.Count()} users");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}