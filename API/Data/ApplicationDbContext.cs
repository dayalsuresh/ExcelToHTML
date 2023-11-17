using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Data;


public class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
{
    // Additional logic can be added here if needed
}
}