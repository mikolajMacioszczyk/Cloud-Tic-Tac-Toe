using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Profiles;
using CloudTicTacToe.Application.Services;
using CloudTicTacToe.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

const string CorsAllPolicy = "AllowAll";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<TicTacToeContext>(options =>
//           options.UseInMemoryDatabase(databaseName: "InMemoryDb"));

builder.Services.AddDbContext<TicTacToeContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(IUnitOfWork)));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IComputerPlayerService, SequentialComputerPlayerService>();
builder.Services.AddScoped<IGameBoardStateService, GameBoardStateService>();
builder.Services.AddScoped<IPointsService, PointsService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddHostedService<TicTacToeMigrator>();

builder.Services.AddCors(o => o.AddPolicy(CorsAllPolicy, corsBulder =>
{
    corsBulder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

var app = builder.Build();

app.UseCors(CorsAllPolicy);

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
