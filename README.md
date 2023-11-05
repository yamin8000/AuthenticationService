# Authentication Service

## Description

This service handles authentication functionalities.

## Features

- [x] Sign-up
- [ ] Sign-in
- [x] Verify
- [x] Set Credentials
- [ ] Reset Password

## Database

### Diagram

<details>
  <summary>Show</summary>

![db-diagram](./Assets/database.jpg)
</details>

### Create Migrations and Database Update

```shell
dotnet tool install --global dotnet-ef
dotnet ef migrations add MigrationName
dotnet ef database update
```
