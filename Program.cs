using Microsoft.EntityFrameworkCore;
using FROASTERY.Components;
using FROASTERY.Data;
using FROASTERY.Services;
using Microsoft.AspNetCore.Components.Authorization;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.CaptureStartupErrors(true);
builder.Logging.SetMinimumLevel(LogLevel.Trace);
// Chuỗi kết nối SQL Server
var connectionString = "Server=localhost;Database=F_ROASTERY;Trusted_Connection=True;TrustServerCertificate=True;";

// Đăng ký AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<VerificationService>();      
// builder.Services.AddScoped<EmailSender>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<TinTucService>();
builder.Services.AddScoped<DanhGiaService>();
builder.Services.AddScoped<DashboardService>();
//===
builder.Services.AddSingleton<UserSessionService>(); 
builder.Services.AddSingleton<CartService>();



builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
// ===
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



builder.Services.AddScoped<ProtectedSessionStorage>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/dangnhap";       // Đường dẫn khi chưa đăng nhập
        options.AccessDeniedPath = "/home_admin";  // Đường dẫn khi bị từ chối truy cập
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();





var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage(); // Quan trọng để debug
}

app.UseHttpsRedirection();
app.UseAntiforgery();

// **Thêm dòng này để phục vụ file tĩnh như ảnh, CSS, JS**
app.UseStaticFiles();
//--------------
app.UseAuthentication();
app.UseAuthorization();



app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
