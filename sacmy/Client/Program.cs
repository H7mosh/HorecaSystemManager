using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using sacmy.Client;
using sacmy.Client.Configuraion;
using sacmy.Client.Services;
using sacmy.Services;
using sacmy.Shared.Core;
using Blazored.LocalStorage;
using System.Globalization;
using sacmy.Client.Shared.Toast;
using MudBlazor.Services;
using MudBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient
builder.Services.AddHttpClient("sacmy.ServerAPI", client => 
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});


// Configure API URL
var baseApiUrl = builder.Configuration["BaseApiUrl"] ?? builder.HostEnvironment.BaseAddress;
builder.Services.AddSingleton(new AppConfig { BaseApiUrl = baseApiUrl });

// Add Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddBlazoredLocalStorage();

// Add Services
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("sacmy.ServerAPI"));
builder.Services.AddScoped<NotificationClientService>();
builder.Services.AddScoped<PurchaseService>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddSingleton<UserGlobalClass>();
builder.Services.AddSingleton<AuthService>();
builder.Services.AddScoped<ProductsService>();
builder.Services.AddScoped<BrandService>();
builder.Services.AddScoped<ToastService>();
builder.Services.AddScoped<StickyNoteService>();


// Configure MudBlazor
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

var host = builder.Build();


// Set default culture
var localStorage = host.Services.GetRequiredService<ILocalStorageService>();
var culture = await localStorage.GetItemAsync<string>("culture") ?? "en-US";
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(culture);
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(culture);

await host.RunAsync();