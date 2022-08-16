EF Core scripting 

## Requirements
- Add package __Microsoft.EntityFrameworkCore.Tools__
- Use NuGet Package Manager console 

## Add migration

    Add-Migration InitExchange -StartupProject src\RestCountries.Data -Project src\RestCountries.Data
  see [Microsoft Documentation](https://docs.microsoft.com/de-de/ef/core/managing-schemas/migrations/managing?tabs=vs)

## Remove Migration

    Remove-Migration -StartupProject src\RestCountries.Data -Project src\RestCountries.Data
  see [Microsoft Documentation](https://docs.microsoft.com/de-de/ef/core/managing-schemas/migrations/managing?tabs=vs)

## Create script 

    Script-Migration -StartupProject src\RestCountries.Data -Project src\RestCountries.Data
  see [Microsoft Documentation](https://docs.microsoft.com/de-de/ef/core/managing-schemas/migrations/applying?tabs=vs)

## Update database (set connection string in appsettings.json)
    Update-Database -StartupProject src\RestCountries.Data -Project src\RestCountries.Data
  see [Microsoft Documentation](https://docs.microsoft.com/de-de/ef/core/managing-schemas/migrations/applying?tabs=vs)

