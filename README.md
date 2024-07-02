# Backend

This project was created using .NET 8 and EF Core as base, with a Database of SQLServer

## Set up

Necessary NuGETpackages for the project (In case they are not installed )

`Microsoft.AspNetCore.Identity.EntityFramework`

`Microsoft.EntityFrameworkCore.SqlServer`

`Microsoft.EntityFrameworkCore.Tools`

`Swashbuckle.AspNetCore`

## Running the project

Make sure to change the `DatabaseConnection string` in the appsettings.json for one that matches your local SQLServer database

In the NuGET package manager console make sure to add the migrations and update the database so that the project can connect to it

Make sure to run the project in `IIS Express` for swagger and testing purposes
