using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServicePattern.Domain.Entities;
using ServicePattern.Infrastructure.Persistence.Constants;

namespace ServicePattern.Infrastructure.Persistence.Configurations;

internal class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable(TableNamesConstants.Genres);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasDefaultValueSql("NEWID()");

        builder
            .Property(x => x.Name)
            .IsRequired();

        builder.HasData(
            new Genre { Id = Guid.NewGuid(), Name = "Action" },
            new Genre { Id = Guid.NewGuid(), Name = "Drama" },
            new Genre { Id = Guid.NewGuid(), Name = "Science Fiction" }
        );
    }
}