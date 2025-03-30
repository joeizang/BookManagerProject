using BookLibraryManagerApi.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLibraryManagerApi.Data.EntityTypeConfigurations;

public class PublisherEntityTypeConfiguration : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder.HasKey(p => p.PublisherId);
        
        builder.Property(p => p.PublisherName)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(p => p.PublisherAddress)
            .HasMaxLength(150)
            .IsRequired(false);
            
    }
}