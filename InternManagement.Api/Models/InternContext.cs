using Microsoft.EntityFrameworkCore;

namespace InternManagement.Api.Models
{
  public class InternContext : DbContext
  {
    public InternContext(DbContextOptions<InternContext> options) : base(options)
    {

    }

    public DbSet<Intern> Interns { get; set; }
    public DbSet<User> Users { get; set; } 
    public DbSet<Attendance> Attendance { get; internal set; }

    public DbSet<Department> Departments { get; set; }
    public DbSet<Division> Divisions { get; set; }
  }
}