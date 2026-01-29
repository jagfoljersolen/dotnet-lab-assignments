using System.Security.Claims;
using AspNetCore.Authentication.Basic;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyApp.Namespace.Data;
using MyApp.Namespace.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddScoped<IFoxService, FoxService>();
builder.Services
.AddAuthentication(BasicDefaults.AuthenticationScheme)
.AddBasic(options =>
    {
        options.Realm = "Fox API";
        options.Events = new BasicEvents
        {
                OnValidateCredentials = async (context) =>
            {
                var user = context.Username;
                var isValid = user == "user" &&
                        context.Password == "password";
                if (isValid)
                {
                    context.Response.Headers.Add(
                            "ValidationCustomHeader",
                            "From OnValidateCredentials"
                    );
                    var claims = new[]
                    {
                        new Claim(
                            ClaimTypes.NameIdentifier,
                            context.Username,
                            ClaimValueTypes.String,
                            context.Options.ClaimsIssuer
                        ),
                        new Claim(
                            ClaimTypes.Name,
                            context.Username,
                            ClaimValueTypes.String,
                            context.Options.ClaimsIssuer
                        )
                    };
                    context.Principal = new ClaimsPrincipal(
                            new ClaimsIdentity(
                                claims,
                                context.Scheme.Name
                                )
                    );
                    context.Success();
                }
                else
                {
                    context.NoResult();
                }
            }
        };
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IFoxesRepository, FoxesRepository>();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var logger = context.RequestServices
            .GetRequiredService<ILogger<Program>>();

        var feature = context.Features
            .Get<IExceptionHandlerPathFeature>();

        if (feature?.Error is not null)
        {
            logger.LogError(
                feature.Error,
                "Nieobsłużony wyjątek podczas obsługi żądania {Path}",
                feature.Path
            );
        }

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var response = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
            Detail = "Wystąpił nieoczekiwany błąd po stronie serwera."
        };

        await context.Response.WriteAsJsonAsync(response);
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseFileServer();



app.Run();


