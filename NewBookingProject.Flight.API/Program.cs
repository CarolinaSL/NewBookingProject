using MediatR;
using MessageBus;
using Microsoft.EntityFrameworkCore;
using NewBookingApp.Core.EFCore;
using NewBookingApp.Core.Generators;
using NewBookingApp.Core.Jwt;
using NewBookingApp.Core.Mapping;
using NewBookingApp.Core.Options;
using NewBookingApp.Core.WebExtensions;
using NewBookingApp.Flight.API.Extensions;
using NewBookingApp.Flight.Infra.Context;
using NewBookingApp.Flight.Infra.Seed;
using NewBookingProject.Flight.API;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

var configuration = builder.Configuration;
var env = builder.Environment;

var appOptions = builder.Services.GetOptions<AppOptions>("AppOptions");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<FlightDbContext>(options =>
                    options.UseNpgsql(
                          configuration.GetConnectionString("DefaultConnection"),
                         x => x.MigrationsAssembly(typeof(FlightDbContext).Assembly.GetName().Name)));


builder.Services.AddScoped<IDataSeeder, FlightDataSeeder>();

builder.Services.AddJwt();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(FlightRoot).Assembly);
builder.Services.AddCustomMapster(typeof(FlightRoot).Assembly);
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddCustomMassTransit(configuration, typeof(FlightRoot).Assembly);

SnowFlakIdGenerator.Configure(1);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseMigrations();
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
   
});

app.MapGet("/", x => x.Response.WriteAsync(appOptions.Name));

app.Run();
