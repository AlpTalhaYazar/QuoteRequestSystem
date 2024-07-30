using Asp.Versioning;
using QuoteRequestSystem.API.Filters;
using QuoteRequestSystem.Infrastructure.DependencyInjection;
using QuoteRequestSystem.Infrastructure.SeedData;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(config =>
{
    config.Filters.Add<CustomResultFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CustomAuthorizationFilter>();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader());
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuoteRequestSystem.API v1"); });
}

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DatabaseSeeder.Seed(services);
}

app.Run();