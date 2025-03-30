using BookLibraryManagerApi.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLibraryManagerApi.Data.EntityTypeConfigurations;

public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(x => x.AuthorId);
        
        builder.Property(x => x.FirstName)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(x => x.LastName)
            .HasMaxLength(50)
            .IsRequired();
    }
}