using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Services;
using CloudTicTacToe.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TicTacToeContext>(options =>
            options.UseInMemoryDatabase(databaseName: "InMemoryDb"));

builder.Services.AddDbContext<TicTacToeContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(IUnitOfWork)));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IComputerPlayerService, SequentialComputerPlayerService>();
builder.Services.AddScoped<IGameBoardStateService, GameBoardStateService>();

// TODO: Migrator
// TODO: Migrations

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
