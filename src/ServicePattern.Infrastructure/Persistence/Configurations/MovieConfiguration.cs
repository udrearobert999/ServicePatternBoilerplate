using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServicePattern.Domain.Entities;
using ServicePattern.Infrastructure.Persistence.Constants;

namespace ServicePattern.Infrastructure.Persistence.Configurations;

internal class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable(TableNamesConstants.Movies);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasDefaultValueSql("NEWID()");

        builder
            .Property(x => x.Title)
            .IsRequired();

        builder
            .Property(x => x.Description)
            .HasDefaultValue(null);

        builder
            .HasMany(x => x.Genres)
            .WithMany(x => x.Movies);

        builder.HasData(
            new Movie { Id = Guid.NewGuid(), Title = "Inception", Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO." },
            new Movie { Id = Guid.NewGuid(), Title = "The Matrix", Description = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers." }
        );
    }
}