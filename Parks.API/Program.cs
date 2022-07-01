using Microsoft.EntityFrameworkCore;
using Parks.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//============ADDING DBCONTEXT=====================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ParksDbContext>(
    options => options.UseSqlServer(connectionString)//ParksDbContext constructor asks for an options parameter
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
