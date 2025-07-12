using Microsoft.EntityFrameworkCore;
using Railwaybackproject.Models;

namespace Railwaybackproject.Data;

public class DataContext : DbContext
{


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        base.OnModelCreating(modelBuilder);
    }


    public DbSet<User> Users { get; set; }
    public DbSet<Train> Trains { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Railway;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }
}
