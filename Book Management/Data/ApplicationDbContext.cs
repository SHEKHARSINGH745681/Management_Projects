using Book_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Management.Data
{
    public class ApplicationDbContext : DbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //Table create hogi isse

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> authors { get; set; }

        

    }
}
