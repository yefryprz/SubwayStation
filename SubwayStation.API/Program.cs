using Microsoft.EntityFrameworkCore;
using SubwayStation.Domain;
using SubwayStation.Infrastructure.ConfigInjections;
using SubwayStation.Infrastructure.middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Get config from appSetting.json
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("Settings"));

//Enable memory cache
builder.Services.AddMemoryCache();

//Add Custom Configuration
builder.Services.AddAutoMapperConfig();
builder.Services.AddServicesInjections();
builder.Services.AddRepositoryInjections();

// Inject DbContext
string? connectionString = builder.Configuration.GetConnectionString("SubwayStationConnection");
builder.Services.AddDbContext<SubwayStationContext>(options => options.UseSqlServer(connectionString));

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

//Add middlewares
app.UseMiddleware(typeof(ErrorHandlingMiddleware));

app.MapControllers();
app.Run();

//Add this line to use WebApplicationFactory to run this code while run test
public partial class Program { }