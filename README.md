# Authentication Service

## Description

This service handles authentication functionalities.

## Features

- [x] Sign-up
- [ ] Sign-in
- [x] Verify
- [x] Set Credentials
- [ ] Reset Password
> Scenario\
1- The user enters his number.\
2- If the user's number is already registered, a message will be sent to him.\
3- In the text of the message, there is a password reset link, which is a combination of the password reset path and the token parameter. (Token is a random 32-character phrase.)\
4- If the link is valid, the user will be redirected to the password change page.\
5- The user enters his new password on the page and his password is changed.\

- [ ] Reset Username

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
