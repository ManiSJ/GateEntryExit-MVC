using GateEntryExit_MVC.Services;
using GateEntryExit_MVC.Services.Gate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IHttpClientService, HttpClientService>();
builder.Services.AddTransient<IGateService, GateService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Gate}/{action=GetAll}/{id?}");

app.Run();
