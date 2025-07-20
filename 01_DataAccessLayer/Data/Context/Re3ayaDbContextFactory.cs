using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace _01_DataAccessLayer.Data.Context
{
    public class Re3ayaDbContextFactory : IDesignTimeDbContextFactory<Re3ayaDbContext>
    {
        public Re3ayaDbContext CreateDbContext(string[] args)
        {
            // 👇 This tells EF Core to look for appsettings.json in 03-APILayer
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../03-APILayer");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath) // 👈 Use the path above
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = configuration.GetConnectionString("Re3ayaDbConnectionString");

            var optionsBuilder = new DbContextOptionsBuilder<Re3ayaDbContext>();
            optionsBuilder.UseSqlServer(connectionString, opt =>
                opt.EnableRetryOnFailure());

            return new Re3ayaDbContext(optionsBuilder.Options);
        }
    }
}
