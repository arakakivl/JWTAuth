# JWTAuth
Its backend is made in .NET and the frontend is available with Angular 13. For learning the basics of authentication and authorization, this app uses Json Web Tokens for authentication and role based authorization.

## Endpoints

### **Signup, Signin, Refresh token and logout:**
 - `POST /signup` `[AllowAnonymous]`
 - `POST /signin` `[AllowAnonymous]`
 - `POST /refresh` `[AllowAnonymous]`
 - `POST /logout` `[Users e Admins]`
 
### **User management**
 - `GET /admin` `[Admins]`
 - `PATCH /admin` `[Admins]`
 - `DELETE /admin` `[Admins]`

## Concepts and tools
 - dotnet CLI
 - Entity Framework Core
 - Clean Architecture
 - Repository Pattern
 - SQL Server
 - JWT Tokens
 - Docker
 - Docker Compose
 - Wait-For-It.sh (https://github.com/vishnubob/wait-for-it)
 - Angular CLI
 - Angular Material
 - RxJs
 - Npm
 - Guards
 - Interceptors
 - Pipes
 
It's really neccessary to explicity the huge use of StackOverflow?! 

## Running
This app can be run within Docker containers or even by `dotnet CLI`:
### dotnet cli
**Requirements:**
 - .NET SDK 6
 - Angular CLI
 
 1. Clone this repository:
 ```
 git clone https://github.com/arakakiv/jwtauth
 ```
 
 2. Restore each dependency using the `dotnet cli` and the `npm` package manager (you'll need nodejs):
 ```
 cd jwtauth/back && dotnet restore
 cd jwtauth/front && npm install
 ```

 3. Update the app to use an "In memory database" provided by EF Core by uncommenting the line 17 and commenting the line 16:
 ```
    // opt.UseSqlServer(connection); Comment!
    opt.UseInMemoryDatabase("JWTAuthApiDb"); // Uncomment!
 ```

4. Run the backend and after doing it, run the frontend:
```
cd jwtauth/back/JWTAuth.Api && dotnet run
cd jwtauth/front && ng serve --open -p 3000
```

5. Go to the specified port in the angular app!

### Docker
**Requirements**
- Docker
- Docker compose

 1. Clone this repository:
 ```
 git clone https://github.com/arakakiv/jwtauth
 ```

2. Build and run the application using `docker-compose` with the command below:
```
docker-compose up --build
```
3. Go to the specified port and thats all!
