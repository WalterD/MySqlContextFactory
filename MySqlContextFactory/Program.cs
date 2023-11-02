using Microsoft.EntityFrameworkCore;
using MySqlContextFactory.Data.MyDatabase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Setup database(s) - Add DbContextFactory.
string? dbConnectionString = builder.Configuration.GetConnectionString("WebApplication");
//builder.Services.AddDbContextFactory<MyDatabaseDbContext>(options =>
//    options.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)));

builder.Services.AddPooledDbContextFactory<MyDatabaseDbContext>(options =>
    options.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)));

builder.Logging.ClearProviders();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
