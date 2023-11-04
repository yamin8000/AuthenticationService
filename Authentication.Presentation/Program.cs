using System.Reflection;
using Authentication.Application.Dtos;
using Authentication.Application.Interfaces;
using Authentication.Application.Services;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;
using Authentication.Infrastructure.Persistence;
using Authentication.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddScoped<ICrudRepository<User>, CrudRepository<User>>();
builder.Services.AddScoped<ICrudService<User, UserCreateDto, UserUpdateDto>, UserService>();

builder.Services.AddScoped<ICrudRepository<UserChannel>, CrudRepository<UserChannel>>();
builder.Services.AddScoped<ICrudRepository<Verification>, CrudRepository<Verification>>();
builder.Services
    .AddHttpClient<ICrudService<UserChannel, UserChannelCreateDto, UserChannelUpdateDto>,
        CrudService<UserChannel, UserChannelCreateDto, UserChannelUpdateDto>>();
builder.Services.AddScoped<IService, AuthService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    options.IncludeXmlComments(xmlPath.Replace("Representation", "Application"));
    options.IncludeXmlComments(xmlPath.Replace("Representation", "Domain"));
    options.IncludeXmlComments(xmlPath.Replace("Representation", "Infrastructure"));
});

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