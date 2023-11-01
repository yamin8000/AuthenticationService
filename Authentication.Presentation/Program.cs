using Authentication.Application.Interfaces;
using Authentication.Application.Services;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;
using Authentication.Infrastructure.Persistence;
using Authentication.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();

builder.Services
    .AddScoped<IRepository<Login>, EntityRepository<Login>>();
builder.Services
    .AddScoped<ICrudService<Login, object, object>,
        CrudService<Login, object, object>>();

builder.Services.AddControllers();

var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();