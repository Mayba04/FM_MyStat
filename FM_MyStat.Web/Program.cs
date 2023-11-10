using FM_MyStat.Core;
using FM_MyStat.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


string connStr = builder.Configuration.GetConnectionString("DefaultConnection");
// Database context
builder.Services.AddDbContext(connStr);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add core services
builder.Services.AddCoreServices();

// Add Infastracture services
builder.Services.AddInfrastructureServices();

// Add repositories
builder.Services.AddRepositories();


var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
