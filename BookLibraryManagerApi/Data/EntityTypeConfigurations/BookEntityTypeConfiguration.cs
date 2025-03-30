using BookLibraryManagerApi.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLibraryManager.Data.EntityTypeConfigurations;

public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.BookId);

        builder.Property(b => b.Isbn)
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(b => b.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.PublishedDate)
            .IsRequired();

        builder.Property(b => b.BookBlob)
            .HasMaxLength(200);
        
        builder.Property(b => b.BookCoverImage)
            .HasMaxLength(500);
        
        builder.HasIndex(b => b.Isbn)
            .IsUnique();
        
    }
}