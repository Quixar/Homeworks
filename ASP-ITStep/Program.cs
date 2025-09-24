using ASP_ITStep.Data;
using ASP_ITStep.Middleware.Auth;
using ASP_ITStep.Services.Email;
using ASP_ITStep.Services.Identity;
using ASP_ITStep.Services.Jwt;
using ASP_ITStep.Services.Kdf;
using ASP_ITStep.Services.Random;
using ASP_ITStep.Services.Storage;
using ASP_ITStep.Services.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("emailsettings.json");

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddSingleton<ITimeService, SecTimeService>();
builder.Services.AddSingleton<IRandomService, DefaultRandomService>();
builder.Services.AddSingleton<ITimeService, MilisecTimeService>();
builder.Services.AddSingleton<IIdentityService, IdentityService>();
builder.Services.AddSingleton<IKdfService, PbKdfService>();
builder.Services.AddSingleton<IEmailService, GmailService>();
builder.Services.AddSingleton<IJwtService, JwtServiceV1>();
builder.Services.AddSingleton<IStorageService, DiskStorageStorage>();


MySqlConnection connection = new(builder.Configuration.GetConnectionString("LocalDb"));

builder.Services.AddDbContext<DataContext>(
    options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)), ServiceLifetime.Singleton
);

builder.Services.AddScoped<DataAccessor>();



builder.Services.AddDistributedMemoryCache(); 

builder.Services.AddSession(options => 
{ 
  options.IdleTimeout = TimeSpan.FromSeconds(1000);
  options.Cookie.HttpOnly = true; 
  options.Cookie.IsEssential = true; 
});


var app = builder.Build();

app.UseRequestLocalization(opt =>
{
    opt.DefaultRequestCulture = new("en-US");
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.UseSession();

app.UseAuthSession();

//app.UseAuthToken();
app.UseAuthJwt();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

using (var scope = app.Services.CreateScope()) 
{ 
    var db = scope.ServiceProvider.GetRequiredService<DataContext>(); 
    await db.Database.MigrateAsync();
}

app.Run();
