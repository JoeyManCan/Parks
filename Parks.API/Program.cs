using Microsoft.EntityFrameworkCore;
using Parks.API.Data;
using Parks.API.ParksMapper;
using Parks.API.Repositories;
using Parks.API.Repositories.Abstractions;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//============ADDING DBCONTEXT=====================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ParksDbContext>(
    options => options.UseSqlServer(connectionString)//ParksDbContext constructor asks for an options parameter
);

//=================Adding DbContext DI========================
builder.Services.AddScoped<IUnitOfWork<ParksDbContext>, UnitOfWork<ParksDbContext>>();

//=================Adding AutoMapper================
builder.Services.AddAutoMapper(typeof(ParksMapping));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


//=================Adding Serilog========================
builder.Host.UseSerilog((fileContext, loggingConfig) =>
    loggingConfig.WriteTo.Console().ReadFrom.Configuration(fileContext.Configuration)
);

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

//app.MapGet("/", () => "Hello World!");

app.Run();
