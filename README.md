# C# .NET Todo List API (Full Stack CRUD Application)

--- 
A secure full-stack Todo List application built with ASP.NET Core. It provides authentication and task management through a RESTful API backed by a SQL database.

---

## Overview

Built using ASP.NET Core MVC with a layered backend structure. Users can register, log in, and perform CRUD operations on their own todo items.

JWT authentication is used to secure endpoints and ensure users can only access their own data. Entity Framework Core handles database access and migrations.

---

## Core Features

- JWT-based authentication (register / login)
- Protected API routes using token validation
- Full CRUD operations for todo management
- User-specific data isolation
- RESTful API design using standard HTTP methods
- Entity Framework Core integration with SQL database
- Separation of Controllers, Services, and Data layers

---

## Architecture

- **Controllers** – Handle HTTP requests and routing  
- **Services Layer** – Focuses on the logic, validation and data manipulation 
- **Data (EF Core)** – Handles database communication  
- **SQL Database** – Stores users, Tags, Task and todo items  

Designed with separation of concerns to improve scalability and maintainability.

---

## Authentication Flow

1. User registers or logs in  
2. Credentials are validated on the server  
3. JWT token is generated and returned  

---

## API Endpoints

### Auth
- `POST /api/AuthApi/register`
- `POST /api/AuthApi/login`

### Example CRUD (Categories)
- `GET /Categories`
- `POST /Categories`
- `PUT /Categories/{id}`
- `DELETE /Categories/{id}`


## Tech Stack

C# · ASP.NET Core MVC · Entity Framework Core · SQL Server / SQLite · JWT Authentication · Swagger · REST API

---

## Notes

- Built with a focus on clean API structure and relational database design  
- Swagger used for testing and API visualization  
- Designed to reflect typical backend patterns used in .NET web applications  