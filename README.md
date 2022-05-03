# JWTAuth APi
Projeto que utiliza **Json Web Tokens** como meio de **autenticação** e de **autorização** de usuários. Uma interface gráfica em **Angular 13** também está incluída, para a demonstração de uma aplicação real e completa.

## Endpoints
A api possui tanto endpoints que permitem usuários não autenticados quanto autenticados e o mesmo vale para autorizados e não autorizados.

### **Registro, login, refresh de tokens e logout:**
 - `POST /signup` `[AllowAnonymous]`
 - `POST /signin` `[AllowAnonymous]`
 - `POST /refresh` `[AllowAnonymous]`
 - `POST /logout` `[Users e Admins]`
 
### **Gerenciamento de usuários**
 - `GET /admin` `[Admins]`
 - `PATCH /admin` `[Admins]`
 - `DELETE /admin` `[Admins]`

## Conceitos e recursos utilizados
 - dotnet CLI
 - Entity Framework Core
 - Clean Architecture
 - Repository Pattern
 - SQL Server
 - Swagger
 - xUnit
 - JWT Tokens
 - Docker
 - Angular CLI
 - Angular Material
 - RxJs
 - Npm
 - Guards
 - Interceptors
 - Pipes
 
E é claro, muita pesquisa no **google** e no **StackOverflow**!

## Como executar
O projeto pode ser executado tanto pela `dotnet CLI` quanto pelo `Docker`:
### dotnet cli
**Requisitos:**
 - .NET SDK 6
 - Angular CLI
 
 1. Faça o download ou clone este repositório via git:
 ```
 git clone https://github.com/arakakiv/jwtauth
 ```
 
 2. Restaure as dependências entrando nos diretórios de cada parte do projeto (back e front) com os seguintes comandos:
 ```
 cd jwtauth/back && dotnet restore
 cd jwtauth/front && npm install
 ```
 
 3. Edite o arquivo back/JWTAuth.Api/Program.cs e comente a linha referente ao uso do SQL Server (16) e descomente a linha referente a utilização de um banco de dados na memória (17):
 ```
    // opt.UseSqlServer(connection); Comente esta linha!
    opt.UseInMemoryDatabase("JWTAuthApiDb"); // Descomente essa!
 ```

4. Rode o back-end e depois rode o front-end com os seguintes comandos:
```
cd jwtauth/back/JWTAuth.Api && dotnet run
cd jwtauth/front && ng serve --open -p 3000
```

5. Vá para `http://localhost:3000` e é isso!

### Docker
**Requisitos**
- Docker
- Docker compose

 1. Faça o download ou clone este repositório via git:
 ```
 git clone https://github.com/arakakiv/jwtauth
 ```

2. Use o Docker Compose para 'buildar' e executar a aplicação:
```
docker-compose up --build
```
3. Vá para `http://localhost:3000` e é isso!
