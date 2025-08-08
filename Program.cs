using Microsoft.EntityFrameworkCore;
using minimalAPI.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ShirtStoreManagement");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello new World!");
app.MapControllers();
app.Run();

