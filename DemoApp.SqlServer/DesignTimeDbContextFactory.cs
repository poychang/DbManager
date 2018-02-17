using System.IO;
using DbManager.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DemoApp.SqlServer
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlServer(configuration["DbManagerOptions:ConnectionString"], b => b.MigrationsAssembly("DemoApp.SqlServer"))
                .Options;

            return new MyDbContext(options);
        }
    }
}
