using MediatR;
using MessageBus;
using Microsoft.EntityFrameworkCore;
using NewBookingApp.Core.EFCore;
using NewBookingApp.Core.Mapping;
using NewBookingApp.Core.Options;
using NewBookingApp.Core.WebExtensions;
using NewBookingApp.Identity;
using NewBookingApp.Identity.Data.Configuration;
using NewBookingApp.Identity.Data.Context;
using NewBookingApp.Identity.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var env = builder.Environment;

// Add services to the container.
var appOptions = builder.Services.GetOptions<AppOptions>("AppOptions");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<IdentityContext>(options =>
                    options.UseNpgsql(
                          configuration.GetConnectionString("DefaultConnection"),
                         x => x.MigrationsAssembly(typeof(IdentityContext).Assembly.GetName().Name)));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomMapster(typeof(IdentityRoot).Assembly);
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(IdentityRoot).Assembly);
builder.Services.AddCustomMapster(typeof(IdentityRoot).Assembly);
builder.Services.AddScoped<IDataSeeder, IdentityDataSeeder>();
builder.Services.AddCustomMassTransit(configuration, typeof(IdentityRoot).Assembly);
builder.Services.AddIdentityServer(env); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMigrations();

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseIdentityServer();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    
});

app.MapGet("/", x => x.Response.WriteAsync(appOptions.Name));

app.Run();
