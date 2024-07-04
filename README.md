# User Product Management API

## Description
This is a robust ASP.NET Core web API that allows users to register, login, and perform CRUD operations on products. The API supports pagination and filtering for efficient data management.

## Features
- User registration and authentication
- CRUD operations for products
- Pagination and filtering for product lists
- Secure endpoints with JWT authentication

## Requirements
- .NET Core SDK 6.0 or later
- SQL Server
- Entity Framework Core

## Setup

### Clone the Repository
```bash
git clone <repository_url>

//Navigate to the Project Directory

cd UserProductAPI

//Restore Dependencies
dotnet restore

//Configure User Secrets
dotnet user-secrets set "ConnectionStrings:UserProductAuthConnectionString" "your_auth_connection_string" --project UserProductAPI
dotnet user-secrets set "ConnectionStrings:UserProductDBConnectionString" "your_product_connection_string" --project UserProductAPI
dotnet user-secrets set "Jwt:Key" "your_jwt_key" --project UserProductAPI
dotnet user-secrets set "Jwt:Issuer" "your_jwt_issuer" --project UserProductAPI
dotnet user-secrets set "Jwt:Audience" "your_jwt_audience" --project UserProductAPI

dotnet run


