using ASP_P26.Data;
using ASP_P26.Services.Kdf;
using ASP_P26.Services.Random;
using ASP_P26.Services.Time;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddSingleton<ITimeService, SecTimeService>();
builder.Services.AddSingleton<IRandomService, DefaultRandomService>();
builder.Services.AddSingleton<ITimeService, MillisecTimeService>();
builder.Services.AddSingleton<IKdfService, PbKdfService>();

MySqlConnection connection = new(builder.Configuration.GetConnectionString("LocalDb"));

builder.Services.AddDbContext<DataContext>(
    options => options.UseMySql(connection, ServerVersion.AutoDetect(connection))
);

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
/* �.�. �������� ����� ������� ������
 * ���������� ������� ������� �� ������
 * �������� �������� ����� � ���������� ���� �����������
 * �������� ����� ��������� ������ �����������
 */
