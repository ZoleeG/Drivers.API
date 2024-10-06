using DriverInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DriverInfo.API.DbContexts
{
    public class DriverInfoContext : DbContext
    {
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Win> Wins { get; set; }

        public DriverInfoContext(DbContextOptions<DriverInfoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Driver>()
                .HasData(
                new Driver("Max Verstappen")
                {
                    Id = 1,
                    Description = "The guy who always wins."
                },
                new Driver("Lewis Hamilton")
                {
                    Id = 2,
                    Description = "Won F1-championship 7 times."
                }
                );

            modelBuilder.Entity<Win>()
                .HasData(
                new Win("Spanish Grand Prix")
                {
                    Id = 1,
                    GridPosition = 4,
                    Year = 2016,
                    DriverId = 1 
                },
                new Win("Malaysian Grand Prix")
                {
                    Id = 2,
                    GridPosition = 3,
                    Year = 2017,
                    DriverId = 1
                },
                new Win("Mexican Grand Prix")
                {
                    Id = 3,
                    GridPosition = 2,
                    Year = 2017,
                    DriverId = 1
                },
                new Win("Canadian Grand Prix")
                {
                    Id = 4,
                    GridPosition = 1,
                    Year = 2007,
                    DriverId = 2
                },
                new Win("United States Grand Prix")
                {
                    Id = 5,
                    GridPosition = 1,
                    Year = 2007,
                    DriverId = 2
                },
                new Win("Hungarian Grand Prix")
                {
                    Id = 6,
                    GridPosition = 1,
                    Year = 2007,
                    DriverId = 2
                },
                new Win("Japanese Grand Prix")
                {
                    Id = 7,
                    GridPosition = 1,
                    Year = 2007,
                    DriverId = 2
                }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
