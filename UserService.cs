using project.Data;
using project.Entities;

namespace project;

public class UserService
{
    private readonly DataContex _contex;

    public UserService(DataContex contex)
    {
        _contex = contex;
    }

    public void CreateUser()
    {
        Console.WriteLine("\n--- Create New User ---");

        Console.Write("Enter Name: ");
        string name = Console.ReadLine()!;

        Console.Write("Enter Email: ");
        string email = Console.ReadLine()!;

        Console.Write("Enter Birthdate (yyyy-MM-dd) (optional): ");
        string? birthdateInput = Console.ReadLine();
        DateTime? birthdate = null;
        if (DateTime.TryParse(birthdateInput, out DateTime parsedDate))
        {
            birthdate = parsedDate;
        }

        var roles = _contex.UserRoles.ToList();
        if (roles.Count == 0)
        {
            Console.WriteLine("No roles found. Cannot assign role to user.");
            return;
        }

        Console.WriteLine("\nAvailable Roles:");
        for (int i = 0; i < roles.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {roles[i].Description}");
        }

        Console.Write("Select role number: ");
        if (!int.TryParse(Console.ReadLine(), out int roleIndex) || roleIndex < 1 || roleIndex > roles.Count)
        {
            Console.WriteLine("Invalid role selection.");
            return;
        }

        var selectedRole = roles[roleIndex - 1];

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            Birthdate = birthdate,
            RegisteredAt = DateTime.UtcNow
        };

        _contex.Users.Add(newUser);

        var newAccess = new UserAccess
        {
            UserId = newUser.Id,
            RoleId = selectedRole.Id,
        };

        _contex.UserAccesses.Add(newAccess);

        _contex.SaveChanges();

        Console.WriteLine($"User '{name}' created and assigned role '{selectedRole.Description}'.");
    }

    public void PrintOldestUser()
    {
        var user = _contex.Users
            .Where(u => u.Birthdate.HasValue)
            .OrderBy(u => u.Birthdate)
            .FirstOrDefault();

        if (user != null)
        {
            Console.WriteLine($"\nOldest user: {user.Name}, Birthdate: {user.Birthdate:yyyy-MM-dd}");
        }
        else
        {
            Console.WriteLine("\nNo users with known birthdate.");
        }
    }

    public void PrintLastThreeRegisteredUsers()
    {
        var users = _contex.Users
            .OrderByDescending(u => u.RegisteredAt)
            .Take(3)
            .ToList();

        Console.WriteLine("\nLast 3 registered users:");
        foreach (var user in users)
        {
            Console.WriteLine($"{user.Name} registered at {user.RegisteredAt}");
        }
    }
    
    public void ShowRoleStatistics()
    {
        Console.WriteLine("\n--- User Role Statistics ---");

        var roleStats = _contex.UserAccesses
            .Join(
                _contex.UserRoles,
                ua => ua.RoleId,
                ur => ur.Id,
                (ua, ur) => new { UserAccess = ua, UserRole = ur }
            )
            .GroupBy(x => x.UserRole.Description)
            .Select(g => new
            {
                RoleName = g.Key,
                Count = g.Count()
            })
            .ToList();

        foreach (var role in roleStats)
        {
            Console.WriteLine($"{role.RoleName}: {role.Count} users");
        }
    }
}