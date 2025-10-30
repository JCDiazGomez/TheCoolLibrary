@'
# CoolLibrary - Library Management System

A modern Library Management System built with **ASP.NET Core 9.0** following **Clean Architecture** principles.

## 🏗️ Architecture

This solution follows the Clean Architecture pattern with clear separation of concerns:


### Project Dependencies

- **Domain**: No dependencies (pure business logic)
- **Application**: → Domain
- **Infrastructure**: → Application + Domain
- **API**: → Application + Infrastructure

## 🚀 Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB, Express, or full version)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Installation

1. **Clone the repository**


2. **Restore dependencies**

3. **Update connection string**
- Copy `appsettings.example.json` to `appsettings.json`
- Update the connection string in `appsettings.json`

4. **Apply database migrations**


5. **Run the application**


6. **Access Swagger UI**
- Open browser: `https://localhost:5001` or `http://localhost:5000`

## 📦 Tech Stack

### Frameworks & Libraries
- **ASP.NET Core 9.0** - Web API framework
- **Entity Framework Core 9.0** - ORM for data access
- **AutoMapper** - Object-to-object mapping
- **FluentValidation** - Input validation
- **Swashbuckle** - Swagger/OpenAPI documentation
- **OpenTelemetry** - Observability & metrics
- **Confluent.Kafka** - Event streaming
- **Prometheus** - Metrics collection

### Database
- **SQL Server** - Primary database

## 📚 Domain Model

The system manages the following entities:

- **Books** - Library book catalog
- **Authors** - Book authors
- **Genres** - Book categories
- **Customers** - Library members
- **Loans** - Book checkout records
- **Reservations** - Book reservations
- **Fines** - Late fees and penalties

## 🔧 Development

### Build the solution