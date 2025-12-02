# E-Commerce API

A .NET 8 Web API backend service for an e-commerce frontend application, created for learning purposes. This API provides authentication and product management functionality.

## 🌐 Live Demo
API is deployed at: https://ecommerce-api.redmeadow-db5f70a9.eastus.azurecontainerapps.io/weatherforecast

## 🛠 Technologies Used
- .NET 8
- Entity Framework Core
- JWT Authentication
- SQL Server
- Docker
- Azure Container App

## 🔑 API Endpoints

### Authentication
- **POST** `/api/Login/register`
  - Register a new user
  - Body: 
    ```json
    {
        "username": "string",
        "email": "string",
        "password": "string",
        "role": "string" (optional, defaults to "customer")
    }
    ```

- **POST** `/api/Login/login`
  - Login user
  - Body:
    ```json
    {
        "email": "string",
        "password": "string"
    }
    ```
  - Returns: JWT token and user details

### Products
All product endpoints require authentication (JWT token)

- **GET** `/api/Products`
  - Get all products
  - Requires: Authorization header with JWT token

- **GET** `/api/Products/filter?category={categories}`
  - Get products by category
  - Query params: category (comma-separated categories)
  - Requires: Authorization header with JWT token

## 🔒 Authentication
The API uses Auth0 for authentication. To access protected endpoints:
  1. Register a user account via Auth0 or your app’s signup page

  2. Login to get an Auth0 access token

  3. Include the token in the Authorization header:
     Authorization: Bearer {your-auth0-token}

## 💾 Database
The application uses SQL Server for data storage. The database schema includes:
- Users table (authentication)
- Products table (e-commerce items)

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- Docker (optional)
- SQL Server

### Local Development
1. Clone the repository
2. Update connection string in `appsettings.json`

3. Run the application

### Docker Deployment
1. Build the Docker image
2. Run the container

## 📝 Notes
- This is a learning project demonstrating .NET 8 Web API development
- Implements secure authentication using Auth0/JWT
- Uses Entity Framework Core for database operations
- Containerized using Docker for easy deployment
- Deployed on Azure Container Instances

## 🤝 Contributing
This is a learning project, but contributions are welcome! Feel free to submit issues and pull requests.
