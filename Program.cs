using DigilizeTest.Common;
using DigilizeTest.Common.Database;
using DigilizeTest.Companies;
using DigilizeTest.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddTransient(typeof(CompanyService));
builder.Services.AddTransient(typeof(UserService));


builder.Services.AddDbContext<AppDbContext> (opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"), options => {
        options.CommandTimeout(180);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.UseExceptionHandler(exceptionApp =>
{
    exceptionApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var errorResponse = new
        {
            Message = "An unexpected error occurred. Please try again later."
        };

        await context.Response.WriteAsJsonAsync(errorResponse);
    });
});

app.MapCompanyEndpoints();
app.MapUserEndpoints();


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
