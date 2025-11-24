# HRMS (Human Resource Management System) API

A modern, scalable Human Resource Management System built with ASP.NET Core, featuring employee management, pagination, and search capabilities.

## 🚀 Features

- **Employee Management**
  - Create, Read, Update, Delete (CRUD) operations for employees
  - Advanced search functionality across multiple fields
  - Pagination support with customizable page size
  - Response headers for pagination metadata

- **Security**
  - Secure connection string management using user secrets
  - Input validation and model state checking
  - Unique constraints on email and phone numbers

- **Performance**
  - Asynchronous operations throughout
  - Entity Framework Core with optimized queries
  - No-tracking queries for read operations

## 🛠️ Technology Stack

- **Backend Framework**: ASP.NET Core 9.0
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **API Documentation**: Swagger/OpenAPI
- **Object Mapping**: AutoMapper
- **Validation**: Data Annotations

## 📋 Prerequisites

- .NET 9.0 SDK
- SQL Server (Express or higher)
- Visual Studio 2022 or VS Code
- Git

## 🔧 Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/HRMS.git
   cd HRMS
   ```

2. **Configure the database connection**
   ```bash
   # Set up user secrets for development
   cd HRMS.API
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:Default" "Your_Connection_String"
   ```

3. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

## 📚 API Documentation

### Employee Endpoints

#### Get All Employees
```http
GET /api/Employee
```

Query Parameters:
- `page` (optional): Page number (default: 1)
- `searchTerm` (optional): Search term to filter employees
- `searchBy` (optional): Field to search in (firstname, lastname, email, phone)

Response Headers:
- `X-Total-Count`: Total number of records
- `X-Total-Pages`: Total number of pages
- `X-Current-Page`: Current page number
- `X-Page-Size`: Items per page

#### Get Employee by ID
```http
GET /api/Employee/{id}
```

#### Create Employee
```http
POST /api/Employee
```

Request Body:
```json
{
    "firstName": "string",
    "lastName": "string",
    "email": "string",
    "phoneNumber": "string",
    "dateOfBirth": "date",
    "hireDate": "date"
}
```

#### Update Employee
```http
PUT /api/Employee/{id}
```

#### Delete Employee
```http
DELETE /api/Employee/{id}
```

## 🔐 Security

### Connection String Security
The application uses .NET User Secrets to store sensitive information like connection strings. This ensures that sensitive data is not committed to source control.

### Data Validation
- Email addresses must be unique
- Phone numbers must be unique
- Required fields are enforced
- String length constraints are applied

## 🏗️ Project Structure

```
HRMS/
├── HRMS.API/                 # Main API project
│   ├── Controllers/         # API Controllers
│   ├── Models/             # Domain Models
│   ├── Dtos/              # Data Transfer Objects
│   ├── Data/              # Database Context and Configurations
│   ├── Mapping/           # AutoMapper Profiles
│   └── Helpers/           # Utility Classes
└── README.md
```

## 🧪 Testing

The API can be tested using tools like Postman or curl. Example curl commands:

```bash
# Get all employees with pagination
curl -X GET "http://localhost:5045/api/Employee?page=1&searchTerm=john&searchBy=firstname"

# Create new employee
curl -X POST "http://localhost:5045/api/Employee" \
     -H "Content-Type: application/json" \
     -d '{"firstName":"John","lastName":"Doe","email":"john@example.com","phoneNumber":"1234567890","dateOfBirth":"1990-01-01","hireDate":"2024-01-01"}'
```

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👥 Authors

- Your Name - Initial work

## 🙏 Acknowledgments

- ASP.NET Core team
- Entity Framework Core team
- All contributors and supporters

## 📞 Support

For support, email your-email@example.com or create an issue in the repository.