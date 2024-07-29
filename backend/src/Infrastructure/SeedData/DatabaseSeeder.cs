using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuoteRequestSystem.Domain.Entities;
using QuoteRequestSystem.Infrastructure.Data;

namespace QuoteRequestSystem.Infrastructure.SeedData;

public class DatabaseSeeder
{
    public static void Seed(IServiceProvider serviceProvider)
    {
        using var context =
            new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
        context.Database.Migrate();

        if (!context.Dimensions.Any())
        {
            context.Dimensions.AddRange(
                new Dimension { Type = "Carton", Width = 12, Length = 12, Height = 12 },
                new Dimension { Type = "Box", Width = 24, Length = 16, Height = 12 },
                new Dimension { Type = "Pallet", Width = 40, Length = 48, Height = 60 }
            );

            context.SaveChanges();
        }

        if (!context.Countries.Any())
        {
            context.Countries.AddRange(
                new Country { Name = "Türkiye" },
                new Country { Name = "United States" },
                new Country { Name = "United Kingdom" },
                new Country { Name = "Germany" },
                new Country { Name = "France" },
                new Country { Name = "Italy" },
                new Country { Name = "Spain" },
                new Country { Name = "Netherlands" },
                new Country { Name = "Belgium" },
                new Country { Name = "Austria" },
                new Country { Name = "Switzerland" },
                new Country { Name = "Poland" },
                new Country { Name = "Czech Republic" },
                new Country { Name = "Sweden" },
                new Country { Name = "Norway" },
                new Country { Name = "Denmark" },
                new Country { Name = "Finland" },
                new Country { Name = "Greece" },
                new Country { Name = "Portugal" },
                new Country { Name = "Ireland" },
                new Country { Name = "Luxembourg" },
                new Country { Name = "Slovakia" },
                new Country { Name = "Hungary" },
                new Country { Name = "Romania" },
                new Country { Name = "Bulgaria" }
            );

            context.SaveChanges();
        }

        if (!context.Cities.Any())
        {
            var turkiye = context.Countries.Single(c => c.Name == "Türkiye");
            var unitedStates = context.Countries.Single(c => c.Name == "United States");
            var unitedKingdom = context.Countries.Single(c => c.Name == "United Kingdom");

            context.Cities.AddRange(
                new City { Name = "İstanbul", CountryId = 1, Country = turkiye },
                new City { Name = "Ankara", CountryId = 1, Country = turkiye },
                new City { Name = "İzmir", CountryId = 1, Country = turkiye },
                new City { Name = "Adana", CountryId = 1, Country = turkiye },
                new City { Name = "Antalya", CountryId = 1, Country = turkiye },
                new City { Name = "Bursa", CountryId = 1, Country = turkiye },
                new City { Name = "Konya", CountryId = 1, Country = turkiye },
                new City { Name = "Mersin", CountryId = 1, Country = turkiye },
                new City { Name = "Samsun", CountryId = 1, Country = turkiye },
                new City { Name = "Trabzon", CountryId = 1, Country = turkiye },
                new City { Name = "New York", CountryId = 2, Country = unitedStates },
                new City { Name = "Los Angeles", CountryId = 2, Country = unitedStates },
                new City { Name = "Chicago", CountryId = 2, Country = unitedStates },
                new City { Name = "Houston", CountryId = 2, Country = unitedStates },
                new City { Name = "Phoenix", CountryId = 2, Country = unitedStates },
                new City { Name = "Philadelphia", CountryId = 2, Country = unitedStates },
                new City { Name = "San Antonio", CountryId = 2, Country = unitedStates },
                new City { Name = "San Diego", CountryId = 2, Country = unitedStates },
                new City { Name = "Dallas", CountryId = 2, Country = unitedStates },
                new City { Name = "San Jose", CountryId = 2, Country = unitedStates },
                new City { Name = "London", CountryId = 3, Country = unitedKingdom },
                new City { Name = "Birmingham", CountryId = 3, Country = unitedKingdom },
                new City { Name = "Leeds", CountryId = 3, Country = unitedKingdom },
                new City { Name = "Glasgow", CountryId = 3, Country = unitedKingdom },
                new City { Name = "Sheffield", CountryId = 3, Country = unitedKingdom },
                new City { Name = "Bradford", CountryId = 3, Country = unitedKingdom },
                new City { Name = "Manchester", CountryId = 3, Country = unitedKingdom },
                new City { Name = "Edinburgh", CountryId = 3, Country = unitedKingdom },
                new City { Name = "Liverpool", CountryId = 3, Country = unitedKingdom },
                new City { Name = "Bristol", CountryId = 3, Country = unitedKingdom }
            );

            context.SaveChanges();
        }

        if (!context.Users.Any())
        {
            // context.Users.AddRange(
            //     new User
            //     {
            //         Email = "admin@admin.com",
            //         PasswordHash =
            //             "4F217F14D6B7BC2D8BB9F1A7C15021D8A3FF481495D6DF7E00AAE47C1B1A7E22", // 123-Development_Password
            //         FirstName = "Admin",
            //         LastName = "User",
            //         RoleId = 1,
            //         CreatedAt = DateTime.UtcNow,
            //         UpdatedAt = DateTime.UtcNow
            //     },
            //     new User
            //     {
            //         Email = "customer@customer.com",
            //         PasswordHash =
            //             "4F217F14D6B7BC2D8BB9F1A7C15021D8A3FF481495D6DF7E00AAE47C1B1A7E22", // 123-Development_Password
            //         FirstName = "Customer",
            //         LastName = "User",
            //         RoleId = 2,
            //         CreatedAt = DateTime.UtcNow,
            //         UpdatedAt = DateTime.UtcNow
            //     }
            // );

            context.SaveChanges();
        }

        if (!context.UserRoles.Any())
        {
            context.UserRoles.AddRange(
                new UserRole
                {
                    Name = "Admin",
                    Description = "Admin",
                    IsAdmin = true,
                    IsCustomer = false,
                    IsSupplier = false,
                    IsEmployee = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new UserRole
                {
                    Name = "Customer",
                    Description = "Customer",
                    IsAdmin = false,
                    IsCustomer = true,
                    IsSupplier = false,
                    IsEmployee = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new UserRole
                {
                    Name = "Supplier",
                    Description = "Supplier",
                    IsAdmin = false,
                    IsCustomer = false,
                    IsSupplier = true,
                    IsEmployee = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new UserRole
                {
                    Name = "Employee",
                    Description = "Employee",
                    IsAdmin = false,
                    IsCustomer = false,
                    IsSupplier = false,
                    IsEmployee = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );

            context.SaveChanges();
        }
    }
}