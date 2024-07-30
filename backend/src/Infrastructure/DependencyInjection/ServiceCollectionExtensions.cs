using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using QuoteRequestSystem.Application.Services;
using QuoteRequestSystem.Core.Repositories;
using QuoteRequestSystem.Core.Services;
using QuoteRequestSystem.Core.UnitOfWork;
using QuoteRequestSystem.Infrastructure.Data;
using QuoteRequestSystem.Infrastructure.Factories;
using QuoteRequestSystem.Infrastructure.Repositories;

namespace QuoteRequestSystem.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySQL(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IDimensionRepository, DimensionRepository>();
        services.AddScoped<IOfferRepository, OfferRepository>();
        services.AddScoped<IQuoteRepository, QuoteRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<ICityService, CityService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<IDimensionService, DimensionService>();
        services.AddScoped<IOfferService, OfferService>();
        services.AddScoped<IQuoteService, QuoteService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IServiceFactory, ServiceFactory>();

        var jwtSettings = configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("jwt"))
                        {
                            context.Token = context.Request.Cookies["jwt"];
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }
}