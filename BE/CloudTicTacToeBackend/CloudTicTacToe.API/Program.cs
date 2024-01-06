using CloudTicTacToe.API.Hubs;
using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Profiles;
using CloudTicTacToe.Application.Services;
using CloudTicTacToe.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
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

var useInMemoryDb = builder.Configuration.GetSection("UseInMemoryDb").Get<bool>();

if (useInMemoryDb)
{
    builder.Services.AddDbContext<TicTacToeContext>(options =>
               options.UseInMemoryDatabase(databaseName: "InMemoryDb"));
}
else
{
    builder.Services.AddDbContext<TicTacToeContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));
}

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(IUnitOfWork)));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IComputerPlayerService, SequentialComputerPlayerService>();
builder.Services.AddScoped<IGameBoardStateService, GameBoardStateService>();
builder.Services.AddScoped<IPointsService, PointsService>();
builder.Services.AddSingleton<IGameConnectionService, GameConnectionService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddHostedService<TicTacToeMigrator>();

builder.Services.AddSignalR()
    .AddJsonProtocol(options =>
    {
        options.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.PayloadSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.PayloadSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string>();

builder.Services.AddCors(o => o.AddPolicy(CorsAllPolicy, corsBulder =>
{
    corsBulder
        .AllowAnyHeader()
        .AllowAnyMethod()
        // TODO: Restore
        //.AllowCredentials()
        //.WithOrigins(allowedOrigins.Split(","));
        .AllowAnyOrigin();
}));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
           .AddCookie()
           .AddOpenIdConnect(options =>
           {
               options.ResponseType = builder.Configuration["Authentication:Cognito:ResponseType"];
               options.MetadataAddress = builder.Configuration["Authentication:Cognito:MetadataAddress"];
               options.ClientId = builder.Configuration["Authentication:Cognito:ClientId"];
               options.SaveTokens = true;
               options.Events = new OpenIdConnectEvents()
               {
                   OnRedirectToIdentityProviderForSignOut = OnRedirectToIdentityProviderForSignOut
               };
           });

Task OnRedirectToIdentityProviderForSignOut(RedirectContext context)
{
    context.ProtocolMessage.Scope = "openid";
    context.ProtocolMessage.ResponseType = "code";

    var cognitoDomain = builder.Configuration["Authentication:Cognito:CognitoDomain"];

    var clientId = builder.Configuration["Authentication:Cognito:ClientId"];

    var logoutUrl = $"{context.Request.Scheme}://{context.Request.Host}{builder.Configuration["Authentication:Cognito:AppSignOutUrl"]}";

    context.ProtocolMessage.IssuerAddress = $"{cognitoDomain}/logout?client_id={clientId}&logout_uri={logoutUrl}&redirect_uri={logoutUrl}";

    // delete cookies
    context.Properties.Items.Remove(CookieAuthenticationDefaults.AuthenticationScheme);
    // close openid session
    context.Properties.Items.Remove(OpenIdConnectDefaults.AuthenticationScheme);

    return Task.CompletedTask;
}

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(CorsAllPolicy);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<GameHub>("/hubs/game");

app.Run();
