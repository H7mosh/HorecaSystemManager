using Hangfire;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.DatabaseContext;
using sacmy.Server.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Add database context
builder.Services.AddDbContext<SafeenCompanyDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("productionConnectionString")
    )
);

// Add Hangfire
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("productionConnectionString")));
builder.Services.AddHangfireServer();

// Add custom services
builder.Services.AddScoped<FileService>();
builder.Services.AddScoped<NotificationService>(provider => {
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new NotificationService(configuration, "SafinAhmedManagerNotificationKeys");
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseCors();

// Configure Hangfire dashboard
app.UseHangfireDashboard();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
