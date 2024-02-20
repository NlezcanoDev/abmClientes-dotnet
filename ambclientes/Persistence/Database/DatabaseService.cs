using Amb.Clientes.Domain.Entities;
using Amb.Clientes.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Amb.Clientes.Persistence.Database;

public class DatabaseService: DbContext
{
    public DatabaseService(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Cliente> Cliente { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        new ClienteConfiguration(modelBuilder.Entity<Cliente>());
    }
}