using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Models;

namespace LibraryApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<LibraryApp.Models.Book> Book { get; set; }
    public DbSet<LibraryApp.Models.User> User { get; set; }
    public DbSet<LibraryApp.Models.Transaction> Transaction { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Book>()
            .Property(s => s.Name)
            .IsRequired();

        base.OnModelCreating(builder);
    }
}

