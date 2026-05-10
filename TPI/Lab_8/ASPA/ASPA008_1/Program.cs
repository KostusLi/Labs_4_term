using ANC25_WEBAPI_DLL;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.AddCelebritiesConfiguration();
builder.AddCelebritiesServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();

app.UseANCErrorHandler("ANC28");
//app.MapCelebrities();
//app.MapLifeevents();
app.MapPhotoCelebrities();

app.UseAuthorization();

app.MapControllerRoute(
    name: "celebrity_new",
    pattern: "/0",
    defaults: new {Controller="Celebrities", Action="NewHumanForm"});

app.MapControllerRoute(
    name: "celebrity_id",
    pattern: "/{id:int:min(1)}",
    defaults: new {Controller="Celebrities", Action="Human"});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Celebrities}/{action=Index}/{id?}");

app.Run();
