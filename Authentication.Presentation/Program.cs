using Authentication.Application.Dtos;
using Authentication.Application.Interfaces;
using Authentication.Application.Services;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;
using Authentication.Infrastructure.Persistence;
using Authentication.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddScoped<IBaseCrudRepository<User>, BaseCrudRepository<User>>();
builder.Services.AddScoped<ICrudService<User, UserCreateDto, UserUpdateDto>, UserService>();

builder.Services.AddScoped<ICrudRepository<Channel>, CrudRepository<Channel>>();
builder.Services.AddScoped<IBaseCrudRepository<UserChannel>, BaseCrudRepository<UserChannel>>();
builder.Services.AddScoped<IBaseCrudRepository<Verification>, BaseCrudRepository<Verification>>();
builder.Services
    .AddScoped<ICrudService<Verification, CreateVerificationDto, UpdateVerificationDto>, VerificationService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();