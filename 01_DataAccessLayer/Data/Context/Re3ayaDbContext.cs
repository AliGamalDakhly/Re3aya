using System.Reflection;
using _01_DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace _01_DataAccessLayer.Data.Context
{
    internal class Re3ayaDbContext: DbContext
    {
        DbSet<Model> Models { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=db23185.public.databaseasp.net; Database=db23185; User Id=db23185; Password=Ali123456_; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;")
                          .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
