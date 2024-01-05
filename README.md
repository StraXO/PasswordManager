# Password manager API
This is a password manager API that allows you to store your passwords in a secure way.

## Setup

### Prerequisites
It is recommended to use the latest version of [Visual Studio](https://visualstudio.microsoft.com/) 
or [Rider](https://www.jetbrains.com/rider/) to run the application.

### Running the API
To run the API, the NuGet packages need to be restored and the database needs to be migrated.
To restore the NuGet packages, run the following command:
```bash
dotnet restore
```

To migrate the database, run the following command:
```bash
dotnet ef database update --project .\src\PasswordManager.Persistence --startup-project .\src\PasswordManager
```

## Development

### Adding migrations
To migrate the database, run the following command in the base directory of the project:
```bash
dotnet ef migrations add <migration-name> --project .\src\PasswordManager.Persistence --startup-project .\src\PasswordManager
```

After that, you can update the database with the following command:
```bash
dotnet ef database update --project .\src\PasswordManager.Persistence --startup-project .\src\PasswordManager
```