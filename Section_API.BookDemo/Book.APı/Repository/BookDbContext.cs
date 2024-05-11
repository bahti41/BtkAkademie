using Book.APı.Repository.Configur;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Book.APı.Repository
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new bookConfig());
        }
    }
}
