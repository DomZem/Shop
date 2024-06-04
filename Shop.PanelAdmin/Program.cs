using Shop.GrpcService;
using Shop.PanelAdmin.Config;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddGrpcClient<Shop.PanelAdmin.PdfGenerator.PdfGeneratorClient>(options =>
{
    options.Address = new Uri("https://localhost:5000");
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var shopAPI = builder.Configuration.GetSection("ShopAPI");
builder.Services.Configure<ShopAPIConfig>(shopAPI);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapRazorPages();
 
app.Run();
