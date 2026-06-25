using Microsoft.EntityFrameworkCore;
using aspnet05.Models;

namespace aspnet05.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  public DbSet<Weather> Weathers => Set<Weather>();
}