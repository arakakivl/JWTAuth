using JWTAuth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using JWTAuth.Core.Interfaces;
using JWTAuth.Application.Services.Interfaces;
using JWTAuth.Application.Services;
using JWTAuth.Infrastructure.Persistence.Repositories;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var connection = @"Server=db;Database=master;User=sa;Password=Your_password123;";

builder.Services.AddDbContext<AppDbContext>(opt => 
{
    opt.UseSqlServer(connection);
    // opt.UseInMemoryDatabase("JWTAuthApiDb");
});

builder.Services.AddTransient<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<IInvalidTokensRepository, InvalidTokensRepository>();
builder.Services.AddTransient<IAcessTokensService, AcessTokensService>();
builder.Services.AddTransient<IRefreshTokensService, RefreshTokensService>();
builder.Services.AddTransient<IRefreshTokensRepository, RefreshTokensRepository>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IAdmService, AdmService>();

var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("SecureToken").Value);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true; // Only in Development mode;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddCors();
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(x => {
    x.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
});

app.MapControllers();


using (var sp = builder.Services.BuildServiceProvider())
{
    sp?.GetService<AppDbContext>()?.Database.Migrate();
}


app.Run();