# TodoList — Full Stack ASP.NET Core Web App

--- 
A secure full-stack task manager built with ASP.NET Core and React. Features JWT authentication, category-based task organization, soft delete, and is deployed to Azure App Service via GitHub Actions CI/CD.

---
Live Deployment: [todolist app](https://todo-list-e2crgubpevb7eqgz.westcentralus-01.azurewebsites.net)
---

## Overview

Built using ASP.NET Core with a layered backend structure. Users can register, log in, create categories, manage tasks within those categories, and view deleted tasks in a trash view.

JWT authentication secures all endpoints and ensures users can only access their own data. Entity Framework Core handles database access and migrations.

The frontend is built with React and Vite and served by the ASP.NET Core backend.

---

## Core Features

- JWT-based authentication (register / login)
- Protected API routes using token validation
- Category creation and management
- Task creation scoped to specific categories
- Soft delete with trash view
- User-specific data isolation
- RESTful API design using standard HTTP methods
- Entity Framework Core integration with SQL database
- Separation of Controllers, Services, and Data layers

---

## Architecture

- **Controllers** – Handle HTTP requests and routing  
- **Services Layer** – Business logic, validation and data manipulation 
- **Data (EF Core)** – Handles database communication  
- **SQL Database** – Stores users, categories, and tasks  
- **Frontend** – React + Vite for user interface and API consumption

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

C# · ASP.NET Core · Entity Framework Core · SQL Server / SQLite · JWT Authentication · Swagger · REST API · React + Vite

---

## Notes

- Built with a focus on clean API structure and relational database design  
- Swagger used for testing and API visualization  
- Deployed to Azure App Service on Linux via GitHub Actions CI/CD pipeline