using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserProductAPI.Core.Entities;
using UserProductAPI.Infrastructure.Data;
using UserProductAPI.Infrastructure.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContexts
builder.Services.AddDbContext<UserProductAuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserProductAuthConnectionString")));

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register Identity Password Hasher
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


// Register UserRepository
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
