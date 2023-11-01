using Authentication.Application.Dtos;
using Authentication.Application.Interfaces;
using Authentication.Application.Services;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();

builder.Services
    .AddScoped<ICrudService<User, UserCreateDto, UserUpdateDto>, CrudService<User, UserCreateDto, UserUpdateDto>>();

builder.Services.AddControllers();

var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();