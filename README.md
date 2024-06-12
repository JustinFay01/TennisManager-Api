# Database Models and Migrations

We are going to be using EF to create both ORM and migrations. 

## Postgres



## Migrations

To create a new migration run: 

```
dotnet ef migrations add <MigrationName>
```

To update the database to the latest migration

```
dotnet ef database update
```

This will generate a migration with a timestamp preceeding the given name.


## Models

Models don't need to extend anything, but do needed to be added to the `TennisManagerContext.cs` class in the form of a db set. 


# Starting a New Project

1. Create empty solution
2. Create empty webapi project
3. Run web api with https to validate it works

