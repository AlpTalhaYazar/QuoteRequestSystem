FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["src/Application/QuoteRequestSystem.Application.csproj", "src/Application/"]
COPY ["src/Core/QuoteRequestSystem.Core.csproj", "src/Core/"]
COPY ["src/Domain/QuoteRequestSystem.Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/QuoteRequestSystem.Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/Presentation/QuoteRequestSystem.API/QuoteRequestSystem.API.csproj", "src/Presentation/QuoteRequestSystem.API/"]
RUN dotnet restore "src/Presentation/QuoteRequestSystem.API/QuoteRequestSystem.API.csproj"

# Copy the rest of the code
COPY . .

# Build the application
WORKDIR "/src/src/Presentation/QuoteRequestSystem.API"
RUN dotnet build "QuoteRequestSystem.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
WORKDIR /src/src/Presentation/QuoteRequestSystem.API
RUN dotnet publish "QuoteRequestSystem.API.csproj" -c Release -o /app/publish

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuoteRequestSystem.API.dll"]