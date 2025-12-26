using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using TimeCapsule.Infrastructure.Data;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var bld = WebApplication.CreateBuilder();

var connectionString = bld.Configuration.GetConnectionString("DefaultConnection");

bld.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

bld.Services.AddCors(options =>
    options.AddPolicy("AllowAll", builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    ));

bld.Services
    .AddFastEndpoints()
    .SwaggerDocument();

var app = bld.Build();

app.UseCors("AllowAll");

app.UseFastEndpoints()
    .UseSwaggerGen();
app.Run();