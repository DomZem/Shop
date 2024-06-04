using DinkToPdf.Contracts;
using DinkToPdf;
using Shop.GrpcService.Configurations;
using Shop.GrpcService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
});

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddSingleton(new HtmlToPdfConfiguration());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<PdfGenerationService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
