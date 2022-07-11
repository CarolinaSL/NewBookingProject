using MessageBus;
using NewBookingApp.Core.Options;
using NewBookingApp.Core.WebExtensions;
using NewBookingApp.Email;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

var appOptions = builder.Services.GetOptions<AppOptions>("AppOptions");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomMassTransit(configuration, typeof(EmailRoot).Assembly);


var app = builder.Build(); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

});

app.MapGet("/", x => x.Response.WriteAsync(appOptions.Name));


app.Run();
