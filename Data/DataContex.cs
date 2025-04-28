using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using project.Entities;

namespace project.Data;

public class DataContex : DbContext
{
    public DbSet<Entities.User> Users { get; set; }
    public DbSet<Entities.UserAccess> UserAccesses { get; set; }
    public DbSet<Entities.UserRole> UserRoles { get; set; }

    public DataContex() : base() {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;database=Ads;uid=root;password=INnoVation");
        optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entities.UserAccess>()
            .HasKey(ua => new { ua.UserId, ua.RoleId });
        
        modelBuilder.Entity<Entities.UserAccess>()
            .HasOne(ua => ua.User)
            .WithMany(u => u.UserAccesses)
            .HasForeignKey(ua => ua.UserId);
        
        modelBuilder.Entity<Entities.UserAccess>()
            .HasOne(ua => ua.Role)
            .WithMany()
            .HasForeignKey(ua => ua.RoleId);
    }
}