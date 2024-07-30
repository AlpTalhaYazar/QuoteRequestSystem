# Quote Request System Backend

This is the backend for the Quote Request System, built with .NET 8 using N-tier architecture.

## Project Structure

```
backend/
├── src/
│   ├── Application/
│   ├── Core/
│   ├── Domain/
│   ├── Infrastructure/
│   └── Presentation/
└── QuoteRequestSystem.sln
```

## Prerequisites

- .NET 8 SDK
- MySQL Server

## Setup

1. Clone the repository
2. Navigate to the `backend` directory
3. Update the connection string in `appsettings.json` to point to your MySQL instance
4. Run the following commands:

```
dotnet restore
dotnet ef database update
dotnet run --project src/Presentation/QuoteRequestSystem.API
```

## API Endpoints

- POST /api/auth/register
- POST /api/auth/login
- POST /api/quote
- GET /api/offer

For detailed API documentation, run the project and navigate to the Swagger UI.

## Running Tests

To run the unit tests, use the following command:

```
dotnet test
```