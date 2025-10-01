using Microsoft.EntityFrameworkCore;
using Restaurant_Management.Models;

namespace Restaurant_Management.Data;

public class PostgresContext : DbContext
{
    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Waiter> Waiters { get; set; }
    public DbSet<Plate> Plates { get; set; } 
    public DbSet<Order> Orders { get; set; } 
}