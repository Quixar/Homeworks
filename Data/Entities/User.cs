namespace project.Entities;

public class User
{
    public Guid      Id           { get; set; }
    public String    Name         { get; set; } = null!;
    public String    Email        { get; set; } = null!;
    public DateTime? Birthdate    { get; set; }
    public DateTime  RegisteredAt { get; set; }
    public DateTime? DeletedAt    { get; set; }
    public List<UserAccess> UserAccesses { get; set; } = [];
}