# BackEnd KCL Hub

> **Enterprise-Grade Microservices Architecture** - A comprehensive backend solution built with .NET 8, featuring Identity Provider (IDP), Asset Management System (AMS), and Human Resource Management System (HRMS).

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=c-sharp)](https://docs.microsoft.com/dotnet/csharp/)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0-512BD4)](https://docs.microsoft.com/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?logo=microsoft-sql-server)](https://www.microsoft.com/sql-server)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

---

## 📋 Table of Contents

- [Architecture Overview](#architecture-overview)
- [Services](#services)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [API Documentation](#api-documentation)
- [Database Migrations](#database-migrations)
- [Development](#development)
- [Security](#security)
- [Deployment](#deployment)
- [Contributing](#contributing)

---

## 🏗️ Architecture Overview

This repository implements a **microservices architecture** with three independent services:

```
BackEnd_KCL_HUB/
├── IDP/          # Identity Provider Service (Authentication & Authorization)
├── AMS/          # Asset Management System
└── HRMS/         # Human Resource Management System
```

### Technology Stack

- **Framework**: .NET 8.0
- **ORM**: Entity Framework Core 8.0
- **Database**: SQL Server
- **Mapping**: AutoMapper
- **Authentication**: JWT (JSON Web Tokens)
- **API Style**: RESTful APIs
- **Architecture Pattern**: Clean Architecture / Repository Pattern

---

## 🔐 Services

### 1. IDP (Identity Provider)

**Port**: `5165`  
**Base URL**: `http://localhost:5165/api`

Authentication and authorization service providing:
- User registration and login
- JWT token generation and validation
- Role-based access control (RBAC)
- User management endpoints

**Key Features**:
- Password hashing with secure algorithms
- JWT token generation with refresh tokens
- Role management (Admin, User, Manager)
- CORS configuration for cross-origin requests

**Endpoints**:
- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User authentication
- `GET /api/auth/users` - Get all users
- `GET /api/auth/users/{id}` - Get user by ID
- `GET /api/roles` - Get all roles
- `GET /api/roles/{id}` - Get role by ID
- `POST /api/roles` - Create new role

### 2. AMS (Asset Management System)

**Port**: `5092` (default)  
**Base URL**: `http://localhost:5092/api`

Comprehensive asset lifecycle management system.

**Key Features**:
- Asset tracking and management
- Maintenance records and scheduling
- Asset ownership tracking
- Status history and audit trail
- Temporary asset usage management
- Supplier and invoice management

**Main Entities**:
- Assets, Asset Types, Asset Status
- Maintenance Records, Maintenance Parts
- Owners, Owner Types
- Maintainers, Maintainer Types
- Temporary Users, Temporary Used Records
- Suppliers, Invoices
- Categories, Locations

### 3. HRMS (Human Resource Management System)

**Port**: `5000` (default)  
**Base URL**: `http://localhost:5000/api`

Human resource management and employee tracking system.

**Key Features**:
- Employee management
- Department organization
- Employee-department relationships
- CRUD operations for employees and departments

**Main Entities**:
- Employees
- Departments

---

## 📦 Prerequisites

Before you begin, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) (2019 or later) or [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/downloads)

### Optional Tools

- [Postman](https://www.postman.com/) or [Insomnia](https://insomnia.rest/) for API testing
- [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms)

---

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/Chhorhout/BackEnd_KCL_HUB.git
cd BackEnd_KCL_HUB
```

### 2. Database Setup

#### For IDP Service

1. Create a SQL Server database named `IDPDb` (or update connection string)
2. Navigate to IDP directory:
   ```bash
   cd IDP/IDP.Api
   ```
3. Apply migrations:
   ```bash
   dotnet ef database update
   ```
   Or create database automatically (configured in `Program.cs`):
   ```bash
   dotnet run
   ```

#### For AMS Service

1. Create a SQL Server database (e.g., `AMS`)
2. Navigate to AMS directory:
   ```bash
   cd AMS/AMS.Api
   ```
3. Apply migrations:
   ```bash
   dotnet ef database update
   ```

#### For HRMS Service

1. Create a SQL Server database (e.g., `HRMS`)
2. Navigate to HRMS directory:
   ```bash
   cd HRMS/HRMS.API
   ```
3. Apply migrations:
   ```bash
   dotnet ef database update
   ```

### 3. Configure Connection Strings

**Important**: Connection strings are stored securely using **.NET User Secrets** (recommended) or `appsettings.Development.json` files, which are **not committed** to the repository for security reasons.

#### Option 1: Using .NET User Secrets (Recommended)

**IDP Service**:
```bash
cd IDP/IDP.Api
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:Default" "Server=YOUR_SERVER_NAME;Database=IDPDb;Trusted_Connection=true;TrustServerCertificate=true"
```

**AMS Service**:
```bash
cd AMS/AMS.Api
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:Default" "Server=YOUR_SERVER_NAME;Database=AMS;Trusted_Connection=true;TrustServerCertificate=true"
```

**HRMS Service**:
```bash
cd HRMS/HRMS.API
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:Default" "Server=YOUR_SERVER_NAME;Database=HRMS;Trusted_Connection=true;TrustServerCertificate=true"
```

#### Option 2: Using appsettings.Development.json

**IDP Service** - Edit `IDP/IDP.Api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "Default": "Server=YOUR_SERVER_NAME;Database=IDPDb;Trusted_Connection=true;TrustServerCertificate=true"
  }
}
```

**AMS Service** - Edit `AMS/AMS.Api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "Default": "Server=YOUR_SERVER_NAME;Database=AMS;Trusted_Connection=true;TrustServerCertificate=true"
  }
}
```

**HRMS Service** - Edit `HRMS/HRMS.API/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "Default": "Server=YOUR_SERVER_NAME;Database=HRMS;Trusted_Connection=true;TrustServerCertificate=true"
  }
}
```

> **Note**: User Secrets are stored outside the project directory and are automatically ignored by Git. This is the recommended approach for local development.

### 4. Run the Services

#### Run IDP Service

```bash
cd IDP/IDP.Api
dotnet restore
dotnet run
```

Service will be available at: `http://localhost:5165`

#### Run AMS Service

```bash
cd AMS/AMS.Api
dotnet restore
dotnet run
```

Service will be available at: `http://localhost:5092` (or configured port)

#### Run HRMS Service

```bash
cd HRMS/HRMS.API
dotnet restore
dotnet run
```

Service will be available at: `http://localhost:5000` (or configured port)

---

## ⚙️ Configuration

### Environment Variables

For production deployments, use environment variables instead of `appsettings.json`:

**Windows**:
```cmd
set ConnectionStrings__Default="Server=...;Database=...;..."
```

**Linux/macOS**:
```bash
export ConnectionStrings__Default="Server=...;Database=...;..."
```

### CORS Configuration

All services are configured with CORS policies allowing:
- `http://localhost:5000`
- `http://192.168.189.1:5000`
- `http://192.168.2.214:5000`

To modify CORS settings, edit the `Program.cs` file in each service.

### Network Access

To allow access from other machines on your network, services are configured to listen on `0.0.0.0`:

- **IDP**: Configured in `Program.cs` to listen on `http://0.0.0.0:5165`
- **AMS/HRMS**: Configure `launchSettings.json` with `applicationUrl: "http://0.0.0.0:{port}"`

---

## 📚 API Documentation

### IDP API Examples

#### Register a User

```http
POST http://localhost:5165/api/auth/register
Content-Type: application/json

{
  "name": "John Doe",
  "email": "john.doe@example.com",
  "password": "SecurePassword123!",
  "roleId": "00000000-0000-0000-0000-000000000001"
}
```

#### Login

```http
POST http://localhost:5165/api/auth/login
Content-Type: application/json

{
  "email": "john.doe@example.com",
  "password": "SecurePassword123!"
}
```

#### Get All Roles

```http
GET http://localhost:5165/api/roles
```

### AMS API Examples

#### Create an Asset

```http
POST http://localhost:5092/api/assets
Content-Type: application/json

{
  "name": "Laptop Dell XPS 15",
  "assetTypeId": "guid-here",
  "categoryId": "guid-here",
  "locationId": "guid-here"
}
```

#### Get Maintenance Records

```http
GET http://localhost:5092/api/maintenancerecords
```

### HRMS API Examples

#### Create an Employee

```http
POST http://localhost:5000/api/employees
Content-Type: application/json

{
  "name": "Jane Smith",
  "email": "jane.smith@example.com",
  "departmentId": "guid-here"
}
```

---

## 🗄️ Database Migrations

### Create a New Migration

```bash
# Navigate to the service directory
cd IDP/IDP.Api  # or AMS/AMS.Api or HRMS/HRMS.API

# Create migration
dotnet ef migrations add MigrationName

# Apply migration
dotnet ef database update
```

### Remove Last Migration

```bash
dotnet ef migrations remove
```

### Generate SQL Script

```bash
dotnet ef migrations script -o migration.sql
```

---

## 💻 Development

### Project Structure

Each service follows a clean architecture pattern:

```
Service.Api/
├── Controller/          # API Controllers
├── Data/               # DbContext and Configurations
│   ├── Configs/       # Entity Framework Configurations
│   └── Migrations/    # Database Migrations
├── Dtos/              # Data Transfer Objects
│   ├── *CreateDto.cs  # Request DTOs
│   └── *ResponseDto.cs # Response DTOs
├── Mapping/           # AutoMapper Profiles
├── Models/            # Domain Models/Entities
├── Services/          # Business Logic Services
├── Program.cs         # Application Entry Point
└── appsettings.json   # Configuration
```

### Code Style

- Follow C# coding conventions
- Use meaningful variable and method names
- Add XML documentation comments for public APIs
- Use DTOs for all API requests/responses
- Implement proper error handling and validation

### Testing

Run tests (if available):

```bash
dotnet test
```

---

## 🔒 Security

### Best Practices Implemented

1. **Connection Strings**: Stored in `appsettings.Development.json` (not committed)
2. **Password Hashing**: Secure password hashing algorithms
3. **JWT Tokens**: Secure token generation and validation
4. **CORS**: Configured CORS policies
5. **Input Validation**: Model validation using Data Annotations
6. **SQL Injection Prevention**: Parameterized queries via EF Core

### Security Recommendations

- **Never commit** `appsettings.Development.json` with real credentials
- Use **User Secrets** for local development:
  ```bash
  dotnet user-secrets set "ConnectionStrings:Default" "your-connection-string"
  ```
- Use **Azure Key Vault** or **AWS Secrets Manager** for production
- Enable **HTTPS** in production
- Implement **rate limiting** for API endpoints
- Use **API keys** or **OAuth 2.0** for service-to-service communication

---

## 🚢 Deployment

### Production Checklist

- [ ] Update connection strings to production database
- [ ] Configure CORS for production domains
- [ ] Enable HTTPS
- [ ] Set up logging (Serilog, Application Insights, etc.)
- [ ] Configure health checks
- [ ] Set up monitoring and alerting
- [ ] Review and update security settings
- [ ] Run database migrations
- [ ] Configure reverse proxy (IIS, Nginx, etc.)

### Docker Deployment (Future)

```dockerfile
# Example Dockerfile structure
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["IDP.Api/IDP.Api.csproj", "IDP.Api/"]
RUN dotnet restore "IDP.Api/IDP.Api.csproj"
COPY . .
WORKDIR "/src/IDP.Api"
RUN dotnet build "IDP.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IDP.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IDP.Api.dll"]
```

---

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Contribution Guidelines

- Follow the existing code style
- Write meaningful commit messages
- Add tests for new features
- Update documentation as needed
- Ensure all tests pass before submitting PR

---

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 👥 Authors

- **Chhorhout** - *Initial work* - [GitHub](https://github.com/Chhorhout)

---

## 🙏 Acknowledgments

- .NET Team for the excellent framework
- Entity Framework Core team
- AutoMapper contributors
- All open-source contributors

---

## 📞 Support

For support, please open an issue in the [GitHub Issues](https://github.com/Chhorhout/BackEnd_KCL_HUB/issues) page.

---

## 🔄 Changelog

### Version 1.0.0
- Initial release
- IDP service with JWT authentication
- AMS service with comprehensive asset management
- HRMS service with employee and department management
- Database migrations and seeding
- CORS configuration
- Network access configuration

---

**Built with ❤️ using .NET 8**

