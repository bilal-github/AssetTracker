using AssetTracker.Models;

//pass in the server name (data source) to SetupDatabase
//also update the datasource in the connection string in the appsettings.json
DbSetup.SetUpDatabase(".");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ICategory, CategoryRepository>();
builder.Services.AddSingleton<IItem, ItemRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
