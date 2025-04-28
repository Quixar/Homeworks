namespace project.Entities;

public class UserAccess
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string RoleId { get; set; } = null!;
    
    public User User { get; set; } = null!;
    public UserRole UserRole { get; set; } = null!;
}