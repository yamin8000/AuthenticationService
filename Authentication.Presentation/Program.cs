using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;
using Authentication.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IRepository<User>, EntityRepository<User>>();
builder.Services.AddControllers();
var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();