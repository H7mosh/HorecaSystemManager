using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using MudBlazor;
using MudBlazor.Services;
using sacmy.Client;
using sacmy.Client.Configuraion;
using sacmy.Client.Services;
using sacmy.Services;
using sacmy.Shared.Core;
using Blazored.LocalStorage;
using Microsoft.Extensions.Localization;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("sacmy.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

var baseApiUrl = builder.Configuration["BaseApiUrl"] ?? "http://46.165.247.249:80";
builder.Services.AddSingleton(new AppConfig { BaseApiUrl = baseApiUrl });

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddBlazoredLocalStorage();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("sacmy.ServerAPI"));
builder.Services.AddScoped<NotificationClientService>();
builder.Services.AddScoped<PurchaseService>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddMudServices();
builder.Services.AddSingleton<UserGlobalClass>();
builder.Services.AddSingleton<AuthService>();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomEnd;
    config.SnackbarConfiguration.VisibleStateDuration = 5000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});
builder.Services.AddBlazorBootstrap();

var host = builder.Build();

// Set the default language
var localStorage = host.Services.GetRequiredService<ILocalStorageService>();
var language = await localStorage.GetItemAsync<string>("language") ?? "en";
var culture = new CultureInfo(language);
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await host.RunAsync();