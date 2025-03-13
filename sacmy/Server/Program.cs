using Hangfire;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using sacmy.Client.Services;
using sacmy.Server.DatabaseContext;
using sacmy.Server.Service;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Add database context
builder.Services.AddDbContext<SafeenCompanyDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("onlineConnectionString")
    )
);

// Add Hangfire
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("onlineConnectionString")));
builder.Services.AddHangfireServer();

// Add custom services
builder.Services.AddScoped<FileService>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<StoreService>();
builder.Services.AddScoped<RedisCacheService>();
builder.Services.AddHostedService<LowStockCheckerService>();
builder.Services.AddHostedService<LowStockNotificationService>();


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "46.165.247.249:6379,password=Hamosh1995";
    options.InstanceName = "RedisCacheInstance";
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

// Logging
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

// Notification Services
builder.Services.AddScoped(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var logger = serviceProvider.GetRequiredService<ILogger<NotificationService>>();
    return new NotificationService(configuration, "SafinAhmedManagerNotificationKeys", logger);
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddScoped(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var logger = serviceProvider.GetRequiredService<ILogger<NotificationService>>();
    return new NotificationService(configuration, "SafinAhmedNotificationKeys", logger);
});


builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
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

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
