# JWT Authentication with .Net 6

## Overview

This project demonstrates how to implement JWT (JSON Web Token) authentication in a .NET 6.0 API using C#.

## Requirements

- .NET SDK 6.0
- Visual Studio or Any IDE that support to run .Net

## Setup

1. Update the `appsettings.json` file with your database connection string and JWT settings.
3. To start the API, run `dotnet run` in the project root folder.

## Usage

- Test API endpoints using Postman or any other tool.
- Use the `/Authorization/SignIn`endpoints to obtain a JWT token, username and password are stored in Model/User.cs
- Access GetWeatherForecast endpoints. Make sure to provide a valid JWT token in the HTTP Authorization header.
