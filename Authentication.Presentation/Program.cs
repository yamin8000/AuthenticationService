using Authentication.Domain.Entities;
using Authentication.Domain.Interfaces;
using Authentication.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IRepository<Login>, LoginRepository>();
builder.Services.AddControllers();
var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();