# Authentication Service

## Description

This service handles authentication functionalities contains Sign-up, Sing-in, Forgot Password

## Features

- [ ] Sign-up
- [ ] Sign-in
- [ ] Forgot Password

## Database

### Diagram

<details>
  <summary>Show</summary>

![db-diagram](./Assets/database.jpg)
</details>

### Database init

```shell
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
```