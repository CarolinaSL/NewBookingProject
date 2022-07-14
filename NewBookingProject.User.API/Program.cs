using FluentValidation;
using MediatR;
using MessageBus;
using Microsoft.EntityFrameworkCore;
using NewBookingApp.Core.Generators;
using NewBookingApp.Core.Mapping;
using NewBookingApp.Core.Options;
using NewBookingApp.Core.WebExtensions;
using NewBookingProject.Passenger.API;
using NewBookingProject.Passenger.API.Data;

var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;
var env = builder.Environment;

var appOptions = builder.Services.GetOptions<AppOptions>("AppOptions");
// Add services to the container.
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<PassengerDbContext>(options =>
                    options.UseNpgsql(
                          configuration.GetConnectionString("DefaultConnection"),
                         x => x.MigrationsAssembly(typeof(PassengerDbContext).Assembly.GetName().Name)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(PassengerRoot).Assembly);
builder.Services.AddCustomMapster(typeof(PassengerRoot).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(PassengerRoot).Assembly);
builder.Services.AddCustomMassTransit(configuration, typeof(PassengerRoot).Assembly);

SnowFlakIdGenerator.Configure(2);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapGet("/", x => x.Response.WriteAsync(appOptions.Name));

app.Run();
