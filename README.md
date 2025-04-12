# Shipping Management System

The **Shipping Management System** is a robust and scalable solution designed to streamline shipping operations. It provides features for managing orders, users, and reporting, with a focus on security, performance, and modularity. Built on .NET 9.0, it leverages modern development practices and tools.

---

## Features
- **Order Management**: 
  - Create, update, and track orders with detailed information.
  - Support for filtering, sorting, and pagination.
- **User Management**: 
  - Role-based access control (Admin, Employee, Merchant, Delivery Agent).
  - User registration and authentication with JWT.
- **Authentication & Authorization**: 
  - Secure JWT-based authentication.
  - Role and permission-based authorization.
- **Error Handling**: 
  - Centralized exception handling with custom error responses.
- **API Documentation**: 
  - Integrated Swagger for API testing and documentation.
- **Database Management**: 
  - Entity Framework Core with SQL Server for database operations.
- **Dependency Injection**: 
  - Modular and testable architecture with built-in .NET DI.

---

## Project Structure
The solution is organized into multiple projects for better separation of concerns:

1. **Shipping.Core**:
   - Contains domain models, enums, and shared logic.
   - Implements specifications for querying data.

2. **Shipping.Repository**:
   - Handles database operations using Entity Framework Core.
   - Implements generic repository and unit of work patterns.

3. **Shipping.Service**:
   - Contains business logic and service layer.
   - Implements services for orders, users, and reporting.

4. **Shipping.DTOs**:
   - Defines data transfer objects (DTOs) for API communication.

5. **Shipping_BackEnd**:
   - API layer for exposing endpoints and handling requests.
   - Includes middleware for error handling and authentication.

---

## Prerequisites
- .NET 9.0 SDK
- SQL Server
- Visual Studio 2022 or later
- Postman (optional, for API testing)

---

## Setup Instructions

### 1. Clone the Repository
`git clone <repository-url> cd Shipping_Management_System`

### 2. Configure the Database
Update the `appsettings.json` file in the `Shipping_BackEnd` project with your database connection string:
`"ConnectionStrings": { "DefaultConnection": "Server=<server>;Database=ShippingDB;Trusted_Connection=True;" }`

### 3. Apply Database Migrations
Run the following command to apply migrations and create the database:
`dotnet ef database update --project Shipping.Repository`

### 4. Seed Initial Data
The application seeds initial roles and an admin user during startup. Ensure the database is properly configured before running the application.

### 5. Run the Application
Start the application using the following command:
`dotnet run --project Shipping_BackEnd`

### 6. Access the API
- Swagger UI: [https://localhost:<port>/swagger](https://localhost:<port>/swagger)
- Example Endpoints:
  - **Orders**: `/api/orders`
  - **Users**: `/api/users`
  - **Authentication**: `/api/auth`

---

## Key Endpoints

### Orders
- **GET** `/api/orders`: Retrieve all orders with optional filters.
- **POST** `/api/orders`: Create a new order.
- **PUT** `/api/orders/{id}`: Update an existing order.
- **DELETE** `/api/orders/{id}`: Delete an order.

### Users
- **POST** `/api/auth/register`: Register a new user.
- **POST** `/api/auth/login`: Authenticate and retrieve a JWT token.

### Buggy (Error Testing)
- **GET** `/api/buggy/notfound`: Simulate a 404 error.
- **GET** `/api/buggy/servererror`: Simulate a 500 error.

---

## Technologies Used
- **Framework**: .NET 9.0
- **Database**: SQL Server
- **Authentication**: JWT
- **API Documentation**: Swagger
- **ORM**: Entity Framework Core
- **Dependency Injection**: Built-in .NET DI
- **Logging**: Integrated logging with `ILogger`.

---

## Development Notes

### Error Handling
- Custom middleware (`ExceptionHandlerMiddleware`) is used for centralized error handling.
- Provides detailed error responses in development and generic responses in production.

### Seeding
- Initial roles and an admin user are seeded during application startup.
- Modify the `IdentitySeedData` class to customize seeding logic.

### AutoMapper
- Used for mapping between domain models and DTOs.
- Configured in the `MappingProfiles` class.

### Specifications
- Implements the Specification pattern for flexible and reusable query logic.
- Example: `OrderSpecifications` for filtering and sorting orders.

---

## Contributing
Contributions are welcome! Please follow these steps:
1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Submit a pull request with a detailed description of your changes.

---

## License

---

## Contact
For any questions or support, please contact the project maintainer.


