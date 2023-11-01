using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;
using Authentication.Infrastructure.Persistence;
using Authentication.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddScoped<IRepository<User>, EntityRepository<User>>();
builder.Services.AddScoped<IRepository<UserChannel>, EntityRepository<UserChannel>>();
builder.Services.AddScoped<IRepository<PasswordReset>, EntityRepository<PasswordReset>>();
builder.Services.AddScoped<IRepository<Login>, EntityRepository<Login>>();
builder.Services.AddScoped<IRepository<Verification>, EntityRepository<Verification>>();

builder.Services.AddControllers();

var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();