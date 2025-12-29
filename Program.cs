using FluentValidation;
using FluentValidation.AspNetCore;
using ObiletCase.Validators;
using ObiletCase.Middleware;
using ObiletCase.Models.Configuration;
using ObiletCase.Repositories;
using ObiletCase.Services;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddFluentValidation();

// FluentValidation Validators
builder.Services.AddScoped<IValidator<JourneySearchParameters>, JourneySearchValidator>();

// Session yapılandırması
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Memory Cache
builder.Services.AddMemoryCache();

// Polly Retry & Circuit Breaker Policies
var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.RequestTimeout)
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

var circuitBreakerPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

// HttpClient ve Obilet API Client yapılandırması
builder.Services.AddHttpClient<IObiletApiClient, ObiletApiClient>(client =>
{
    var baseUrl = builder.Configuration["ObiletApi:BaseUrl"] ?? "https://v2-api.obilet.com";
    client.BaseAddress = new Uri(baseUrl);
    client.Timeout = TimeSpan.FromSeconds(30);
})
.AddPolicyHandler(retryPolicy)
.AddPolicyHandler(circuitBreakerPolicy);

// API Settings
builder.Services.Configure<ObiletApiSettings>(
    builder.Configuration.GetSection("ObiletApi"));

// Services
builder.Services.AddScoped<IObiletSessionService, ObiletSessionService>();
builder.Services.AddScoped<IBusLocationService, BusLocationService>();
builder.Services.AddScoped<IJourneyService, JourneyService>();

var app = builder.Build();

// Global Exception Middleware
app.UseGlobalExceptionMiddleware();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();