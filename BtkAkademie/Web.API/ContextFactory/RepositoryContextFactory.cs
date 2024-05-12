using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repositories.EfCore;

namespace Web.API.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<BookDbContext>
    {
        public BookDbContext CreateDbContext(string[] args)
        {
            // configurationBuilder 
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();


            // DbContextOptionsBuilder => 
            var builder = new DbContextOptionsBuilder<BookDbContext>()
                .UseSqlServer(configuration.GetConnectionString("SqlConncetion"),
                prj => prj.MigrationsAssembly("Web.API"));

            return new BookDbContext(builder.Options);
        }
    }
}
