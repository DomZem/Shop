using Shop.Infrastructure.Extensions;
using Shop.Infrastructure.Seeders;
using Shop.Application.Extensions;
using Shop.API.Middlewares;
using Shop.API.Extensions;
using Shop.API.Domain.Security;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtConfig>(jwtSection);

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IShopSeeder>();
await seeder.Seed();

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }