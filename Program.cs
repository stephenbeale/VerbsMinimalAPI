using Microsoft.AspNetCore.Authentication.Cookies;
using VerbsMinimalAPI;
using VerbsMinimalAPI.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSingleton<IDataAccess, DataAccess>()
    .AddAuthorization()
    .AddAuthentication()    
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/forbidden";
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorOrigin", builder =>
    {
        builder.WithOrigins("https://host.docker.internal:8080")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

var app = builder.Build();

//From AI
app.UseCors("AllowBlazorOrigin");

//From error on Swagger
app.UseAuthorization();
app.UseAuthentication();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapApiCalls();

app.UseHttpsRedirection();
app.Run();
