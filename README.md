# Database Models and Migrations

## Todo

~~1. Global Exception Handling~~
~~2. Enum Helper for Fluent Validation~~
 ~~- Convert the enum to a list of strings and validate that the type is in the list~~
3. Determine how to return sessions with repeat occurrences
4. Fix the email validation error message
5. Add user checkin functionality to store user info

```csharp
// User info example
{
  "sub": "****",
  "given_name": "Justin",
  "family_name": "Fay",
  "nickname": "justinfay501",
  "name": "Justin Fay",
  "picture": "https://lh3.googleusercontent.com/a/ACg8ocKjL2GczeHuZHJ5866A-boUi7uHRWXLZGYSTY0PCTA0HY7AQQc=s96-c",
  "updated_at": "2024-09-27T02:39:41.143Z"
}
```


We are going to be using EF to create both ORM and migrations. 

## Postgres



## Migrations

To create a new migration run: 

```
dotnet ef migrations add <MigrationName> -s .\src\tennismanager.api -p .\src\tennismanager.data -v
```

To update the database to the latest migration

```
dotnet ef database update -s .\src\tennismanager.api -p .\src\tennismanager.data -v
```

```
dotnet ef migrations list -s .\src\tennismanager.api -p .\src\tennismanager.data -v
```

This will generate a migration with a timestamp preceeding the given name.


To remove a migration

```
dotnet ef migrations remove -s .\src\tennismanager.api -p .\src\tennismanager.data -v
```


## Models

Models don't need to extend anything, but do needed to be added to the `TennisManagerContext.cs` class in the form of a db set. 


# Starting a New Project

1. Create empty solution
2. Create empty webapi project
3. Run web api with https to validate it works

