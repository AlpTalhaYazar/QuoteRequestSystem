using Microsoft.EntityFrameworkCore;
using QuoteRequestSystem.Domain.Entities;

namespace QuoteRequestSystem.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<City> Cities { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Dimension> Dimensions { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Quotes)
            .WithOne(q => q.User)
            .HasForeignKey(q => q.UserId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.RoleId);

        modelBuilder.Entity<Quote>()
            .HasOne(q => q.Country)
            .WithMany()
            .HasForeignKey(q => q.CountryId);

        modelBuilder.Entity<Quote>()
            .HasOne(q => q.City)
            .WithMany()
            .HasForeignKey(q => q.CityId);

        modelBuilder.Entity<Quote>()
            .HasOne(q => q.Offer)
            .WithOne(o => o.Quote)
            .HasForeignKey<Offer>(o => o.QuoteId);
    }
}