using Common.Interfaces;
using LinearAPI.Services;
using LinearMockDatabase;
using LinearMockDatabase.Database;
using LinearMockDatabase.Repo;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);
var corsSettings = "_allowSpecificOriginsDev";
var cookieLifeTime = new TimeSpan(2, 0, 0); // Cookielife 2 h

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: corsSettings,
        policy =>
        {
            policy
            .WithOrigins("http://localhost:3000", "https://localhost:3000")
            .WithMethods("GET", "PUT", "POST", "DELETE")
            .AllowCredentials()
            .WithHeaders(HeaderNames.Accept, HeaderNames.ContentType, HeaderNames.Authorization);
        }
    );
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.Name = "session_cookie";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = cookieLifeTime;
        options.Events.OnRedirectToLogin = (context) =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
        options.Cookie.HttpOnly = true;
        // Only use this when the sites are on different domains
        options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
    });

ILinearRepo linearRepo = new LinearRepo("generated/");
builder.Services.AddSingleton(typeof(ILinearRepo), linearRepo);
builder.Services.AddSingleton(
    typeof(ISessionService),
    new SessionService(linearRepo.User, linearRepo.Session, new TimeSpan(20, 0, 0)) // session 20h
    );
builder.Services.AddSingleton(
    typeof(ISpotBookingService),
    new SpotBookingService(linearRepo.Order, linearRepo.Advertiser, linearRepo.SpotBooking, linearRepo.Spot)
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(corsSettings);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
