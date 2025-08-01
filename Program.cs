var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello new World!");
app.MapControllers();
app.Run();
