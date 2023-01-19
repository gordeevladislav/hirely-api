using Hirely.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hirely.Data
{
  public class HirelyDbContext : DbContext
  {
    public HirelyDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
  }
}