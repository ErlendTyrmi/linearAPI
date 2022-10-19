using linearAPI.Entities;
using linearAPI.Repo;
using linearAPI.Repo.Database.Generator;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);
var corsSettings = "_allowSpecificOriginsDev";

DataGenerator.Generate();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: corsSettings,
        policy => {
            policy
            .WithOrigins("http://localhost:3000")
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

        options.ExpireTimeSpan = new TimeSpan(0, 60, 0);

        options.Events.OnRedirectToLogin = (context) =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };

        options.Cookie.HttpOnly = true;
        // Only use this when the sites are on different domains
        options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
    });



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
