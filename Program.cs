using VerbsMinimalAPI;
using VerbsMinimalAPI.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSingleton<IDataAccess, DataAccess>();

//From AI - an attempt to allow connection to a non-docker project
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowBlazorOrigin",
		builder => builder.WithOrigins("https://localhost:YOUR_BLAZOR_PORT")
						  .AllowAnyHeader()
						  .AllowAnyMethod());
});


var app = builder.Build();

//From AI
app.UseCors("AllowBlazorOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapApiCalls();

app.UseHttpsRedirection();
app.Run();
