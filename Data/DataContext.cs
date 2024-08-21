using Microsoft.EntityFrameworkCore;
using DxDyIntegrationTest.Models;

namespace DxDyIntegrationTest.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Planet> Planets { get; set; }
        public DbSet<Ship> Ships { get; set; }
    }
}
