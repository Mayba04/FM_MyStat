using FM_MyStat.Core;
using FM_MyStat.Core.Entities.Users;
using FM_MyStat.Infrastructure;
using FM_MyStat.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args);


string connStr = builder.Configuration.GetConnectionString("DefaultConnection");

// Database context

// SQLServer
builder.Services.AddDbContext(connStr);

// PostgreSQL
/*builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});*/

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add core services
builder.Services.AddCoreServices();

// Add Infastracture services
builder.Services.AddInfrastructureServices();

// Add mapping services
builder.Services.AddMapping();

// Add repositories
builder.Services.AddRepositories();


var app = builder.Build();
//AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseStatusCodePagesWithRedirects("/Error/{0}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
