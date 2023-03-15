using grocery_mate_backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var conn = builder.Configuration.GetConnectionString("DefaultConnection");
var conn = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
Console.WriteLine(conn);

builder.Services.AddDbContext<ClickDbContext>(options => options.UseNpgsql(conn));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corspolicy", buid =>
{
   // buid.WithOrigins("http://localhost:3000", "http://localhost:80", "http://localhost:5000", "*").AllowAnyMethod().AllowAnyHeader();
   buid.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ClickDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
