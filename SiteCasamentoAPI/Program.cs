using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;
using Microsoft.OpenApi.Models;

// TEM QUE SER DE PRODUCAO!!!!!
MercadoPagoConfig.AccessToken = "APP_USR-8011123736245051-021921-e0df928af2ad2b774c8a3351a604de36-257463821";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

var devClient = "http://localhost:4200";
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithOrigins(devClient));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();