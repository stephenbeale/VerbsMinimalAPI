using VerbsMinimalAPI;
using VerbsMinimalAPI.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSingleton<IDataAccess, DataAccess>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapApiCalls();

app.UseHttpsRedirection();
app.Run();
