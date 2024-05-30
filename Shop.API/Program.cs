using Shop.Infrastructure.Extensions;
using Shop.Infrastructure.Seeders;
using Shop.Application.Extensions;
using Shop.API.Middlewares;
using Shop.Domain.Entities;
using Shop.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

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

app.MapGroup("api/identity").WithTags("Identity").MapIdentityApi<User>();
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
