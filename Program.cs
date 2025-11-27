using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;     
using TimeCapsule.Infrastructure.Data; 

var bld = WebApplication.CreateBuilder();

var connectionString = bld.Configuration.GetConnectionString("DefaultConnection");

bld.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

bld.Services
    .AddFastEndpoints()
    .SwaggerDocument();

var app = bld.Build();
app.UseFastEndpoints()
    .UseSwaggerGen();
app.Run();