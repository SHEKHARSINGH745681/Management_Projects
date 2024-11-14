using Book_Management.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Book_Management.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet for Books (existing)
        public DbSet<Book> Books { get; set; }

        // DbSet for LoginModel (used for user login, authentication details)
        public DbSet<LoginModel> LoginModels { get; set; }

        // OnModelCreating is used to configure the model (optional, in case you need custom configuration)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the LoginModel entity to have a primary key
            modelBuilder.Entity<LoginModel>()
                .HasKey(x => x.Id); // Specify the primary key for LoginModel

            // Optional: Additional configuration for User and other entities if needed
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)  // Ensure the username is unique
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
