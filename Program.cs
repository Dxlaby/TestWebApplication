using System.ComponentModel;
using TestWebApplication.Background;
using TestWebApplication.Selenium;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHostedService<BackgroundWork>();
builder.Services.AddTransient<WeatherFinder>();

var app = builder.Build();
//app.Urls.Add("http://45.14.224.110:5024");
app.Urls.Add("http://localhost:5024");


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

Console.WriteLine("Started running app");
app.Run();
