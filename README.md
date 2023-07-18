# SmallProjectManager2

Test project which whole purpose is to allow myself to learn how to use Blazor, Entity Framework Core and ASP.NET Core.

## Prerequisites (runtime)

- .NET 7 Runtime
- Running MSSQL Server instance on localhost (it's on my TODO list to make it configurable) with database named `Projects`
    - If you have database running on localhost, you can create database with `dotnet ef database update` command in the `Server` directory and migration will be applied automatically

## Prerequisites (development)
- .NET 7 SDK
- Running MSSQL Server instance on localhost (it's on my TODO list to make it configurable) with database named `Projects`

## How to run
In the root directory of the project run `dotnet run --project Server`

## To build publishable executable
In the root directory go to the `Server` directory and run `dotnet publish -c Release`
Build will be located in `bin/Release/net7.0/publish` directory

## Todo
- [ ] Make database connection configurable
- [ ] Add ability to edit stuff
- [ ] Searching and complex queries
- [ ] Dashboard on / route
- [ ] Maybe more consistent design
- [ ] Don't delete whole relation tree when address is deleted