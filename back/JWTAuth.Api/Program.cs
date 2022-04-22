using Microsoft.EntityFrameworkCore.InMemory;
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

// Add services to the container.

builder.Services.AddDbContext<UsersDbContext>(opt => 
{
    opt.UseInMemoryDatabase("JWTAuthUsers");
});

builder.Services.AddDbContext<InvalidTokensDbContext>(opt => {
    opt.UseInMemoryDatabase("JWTAuthInvalidTokens");
});

builder.Services.AddTransient<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<IInvalidTokensRepository, InvalidTokensRepository>();
builder.Services.AddTransient<ITokensService, TokensService>();
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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();