namespace project.Entities;

public class UserRole
{
    public String  Id          { get; set; } = null!;
    public String  Description { get; set; } = null!;
    public Boolean CanCreate   { get; set; }
    public Boolean CanRead     { get; set; }
    public Boolean CanUpdate   { get; set; }
    public Boolean CanDelete   { get; set; }
    
    public List<UserAccess> UserAccesses { get; set; } = [];
}