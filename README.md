# .NET Core Web Application for User Management

## Overview

The User Management Application is a .NET Core web application designed with help of Entity Framework and ADO .NET combined to facilitate user registration, authentication, and login.

## Table of Contents

- [Features](#features)
- [Frameworks and Libraries](#frameworks-and-libraries)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Database Setup](#database-setup)
- [Running the Application](#running-the-application)
- [Usage](#usage)

## Features

List the main features of the application related to user management. For example:

- User registration and authentication
- Profile management (update user information)
- Password management (reset, change)

## Frameworks and Libraries

List the frameowork and libraries used to develop the application. For example:

- .NET Core: The application is built using .NET Core framework, providing cross-platform capabilities and performance optimizations.
- Entity Framework Core (EF Core): Utilized for object-relational mapping (ORM) to manage application data using models and database contexts.
- ADO.NET: Used for direct database operations where performance or specific requirements dictate its use alongside EF Core.

## Prerequisites

Outline the prerequisites needed to set up and run the application. For example:

- [.NET SDK](https://dotnet.microsoft.com/download) installed
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/) (optional but recommended)

## Installation

Provide step-by-step instructions to install dependencies and set up the development environment. For example:

1. Clone the repository:
   ```
   git clone https://github.com/yourusername/your-project.git
   ```

2. Navigate to the project directory:
   ```
   cd your-project
   ```

3. Restore dependencies:
   ```
   dotnet restore
   ```

## Configuration

Explain how to configure the application, including environment variables, database connections, and any settings that need to be adjusted before running the application.

## Database Setup

Describe how to set up the database for the application. For example:

1. Configure the database connection string in `appsettings.json`:
   ```json
   {
     "UmConnString": {
       "DefaultConnection": "Server=localhost;Database=UserManagementDb;Trusted_Connection=True;"
     }
   }
   ```

2. Apply Scripts which has been store into below location:
   ```
   \UserManagement.Web\SQLScripts\Script.sql
   ```

## Running the Application

Provide instructions on how to build and run the application. For example:

1. Build the application:
   ```
   dotnet build
   ```

2. Run the application:
   ```
   dotnet run
   ```

3. Open a web browser and navigate to `https://localhost:5001` to view the application.

## Usage

Explain how to use the application once it's running. Provide examples of common tasks such as user registration, login, and password change.

---

## Find Me!

Wanna Reach Me Out 📌?<br/>
Reach Me Out At :
[Crest Infosytems]
<a href="https://www.linkedin.com/company/crest-infosystems-pvt-ltd/"><img src="src/public/assets/images/linkedin.svg" width="16" height="16"></img></a>
<br/>
<br/>
Full Stack Developer | Driving Initiatives In Executing Ideas To Reality And Surplus Them |<br/>
Let's Connect To Explore 👇<br />
<a href="https://www.linkedin.com/company/crest-infosystems-pvt-ltd/"><img src="src/public/assets/images/linkedin.svg" width="16" height="16"></img></a>