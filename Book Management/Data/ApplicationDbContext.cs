﻿using Book_Management.Models;
<<<<<<< HEAD
=======
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
>>>>>>> origin/main
using Microsoft.EntityFrameworkCore;

namespace Book_Management.Data
{
<<<<<<< HEAD
    public class ApplicationDbContext : DbContext
    {


=======
    public class ApplicationDbContext : IdentityDbContext<User>
    {
>>>>>>> origin/main
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

<<<<<<< HEAD
        //Table create hogi isse

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> authors { get; set; }

        

=======
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
>>>>>>> origin/main
    }
}
