using FluentValidation;
using MassTransit;
using MediatR;
using MessageBus;
using Microsoft.EntityFrameworkCore;
using NewBookingApp.Booking.API;
using NewBookingApp.Booking.Domain.Interfaces;
using NewBookingApp.Booking.Infra.Context;
using NewBookingApp.Booking.Infra.Respository;
using NewBookingApp.Core.Generators;
using NewBookingApp.Core.Mapping;
using NewBookingApp.Core.Options;
using NewBookingApp.Core.WebExtensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
var env = builder.Environment;

var appOptions = builder.Services.GetOptions<AppOptions>("AppOptions");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<BookingDbContext>(options =>
                    options.UseNpgsql(
                          configuration.GetConnectionString("DefaultConnection"),
                         x => x.MigrationsAssembly(typeof(BookingDbContext).Assembly.GetName().Name)));

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddMediatR(typeof(BookingRoot).Assembly);
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomMapster(typeof(BookingRoot).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(BookingRoot).Assembly);
builder.Services.AddCustomMassTransit(configuration, typeof(BookingRoot).Assembly);


SnowFlakIdGenerator.Configure(3);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMigrations();

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
