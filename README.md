# ProductApp

A .NET 8 web application for managing products with integrated unit testing and CI pipeline using GitHub Actions.

## Features

- ASP.NET Core 8.0 Web API
- Product management (CRUD)
- Unit testing with `xUnit`
- CI/CD pipeline using GitHub Actions
- OpenAPI/Swagger support

## Technologies

- .NET 8.0
- ASP.NET Core
- xUnit
- GitHub Actions

## Requirements

- [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Optional: Visual Studio or VS Code

## 🛠️ How to Run Locally

```bash
# Restore dependencies
dotnet restore ProductApp.sln

# Build the solution
dotnet build ProductApp.sln

# Run the Web API
dotnet run --project ProductApp/ProductApp.csproj
