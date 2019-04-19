using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Models
{
  public class LoginContext : DbContext
  {
    public LoginContext(DbContextOptions options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Wedding> Weddings { get; set; }
    public DbSet<Association> Associations { get; set; }
    
  }
}