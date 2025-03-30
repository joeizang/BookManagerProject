using BookLibraryManagerApi.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryManagerApi.Data;

public class BookManagerContext : DbContext
{
    public BookManagerContext(DbContextOptions<BookManagerContext> options)
        : base(options)
    {
        
    }

    public DbSet<Author> Authors { get; set; }
    
    public DbSet<Book> Books { get; set; }
    
    public DbSet<Publisher> Publishers { get; set; }
    
    public DbSet<AuthorsBooks> AuthorsBooks { get; set; }
    
    public DbSet<AuthorsPublishers> AuthorsPublishers { get; set; }

    public DbSet<Rating> Ratings { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
        modelBuilder.Entity<AuthorsBooks>()
            .HasKey(ab => new { ab.AuthorId, ab.BookId });
        modelBuilder.Entity<AuthorsPublishers>()
            .HasKey(ab => new { ab.AuthorId, ab.PublisherId });

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity<AuthorsBooks>();
        
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Publisher)
            .WithMany(p => p.BooksPublished)
            .HasForeignKey(b => b.PublisherId);
        
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Publishers)
            .WithMany(p => p.BookAuthors)
            .UsingEntity<AuthorsPublishers>();
    }
}