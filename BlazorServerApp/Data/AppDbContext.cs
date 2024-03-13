using DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerApp.Data
{
    public class AppDbContext : DbContext
    {
        string _connectionString = "Server=DESKTOP-PK86BAT;Database=ETR-IS-PGA-TEST;User Id=awit;Password=awit;Trusted_Connection=True;MultipleActiveResultSets=true";

        public DbSet<Employees> Employees { get; set; }
        public DbSet<vwCostUnits> vwCostUnits { get; set; }
       // public DbSet<SampleModel> Warehouses { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        //public IEnumerable<SampleModel> GetWarehouses()
        //{
        //    return Warehouses.FromSqlRaw("EXEC uspWarehouse");
        //}
    }
}
