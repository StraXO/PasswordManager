# Password Manager API

This application allows you to store your passwords in a secure way and access them from anywhere with http requests.

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
dotnet ef database update --project .\src\PasswordManager.Persistence.PostgreSql --startup-project .\src\PasswordManager --project .\src\PasswordManager.Persistence.PostgreSql
```

## Development

### Adding migrations

To migrate the database, run the following command in the base directory of the project:

```bash
dotnet ef migrations add <migration-name> --startup-project .\src\PasswordManager --project .\src\PasswordManager.Persistence.PostgreSql
```

After that, you can update the database with the following command:

```bash
dotnet ef database update --startup-project .\src\PasswordManager --project .\src\PasswordManager.Persistence.PostgreSql
```

## Production
To run the application in production, you need to set the following environment variables:

- Jwt:Audience
- Jwt:Issuer
- Jwt:SigningKey (Can be generated using `openssl rand -base64 64`)