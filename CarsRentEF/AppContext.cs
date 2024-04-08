using CarsRentEF.Objects;
using Microsoft.EntityFrameworkCore;

namespace CarsRentEF

{
    public class VehicleAppContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CarsRent;Trusted_Connection=True");
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
