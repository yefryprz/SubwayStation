using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using SubwayStation.Domain;
using SubwayStation.Infrastructure.ConfigInjections;
using SubwayStation.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add global authorization with jwt
builder.Services.AddControllers(options =>
{
    options.EnableEndpointRouting = true;

    var policy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();

    options.Filters.Add(new AuthorizeFilter(policy));
});

//Get config from appSetting.json
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("Settings"));

//Enable memory cache
builder.Services.AddMemoryCache();

//Add Custom Configuration
builder.Services.AddAutoMapperConfig();
builder.Services.AddSwaggerInjection();
builder.Services.AddServicesInjections();
builder.Services.AddRepositoryInjections();
builder.Services.AddAuthenticationInjection(builder.Configuration);

// Inject DbContext
string? connectionString = builder.Configuration.GetConnectionString("SubwayStationConnection");
builder.Services.AddDbContext<SubwayStationContext>(options => options.UseSqlServer(connectionString, migration => migration.MigrationsAssembly("SubwayStation.API")));
builder.Services.AddDbContext<SubwayIdentityContext>(options => options.UseSqlServer(connectionString, migration => migration.MigrationsAssembly("SubwayStation.API")));

//Enable AspNetCore identity Authentication
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<SubwayIdentityContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.DisplayRequestDuration();
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

//Add middlewares
app.UseMiddleware(typeof(ErrorHandlingMiddleware));

app.MapControllers();
app.Run();

//Add this line to use WebApplicationFactory to run this code while run test
public partial class Program { }