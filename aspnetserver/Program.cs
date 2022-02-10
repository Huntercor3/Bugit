
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder =>
        {
            builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("http://localhost:3000", "https://bugitretry.azurewebsites.net");
        });
}); 

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(SwaggerGenOptionsExtensions =>
{
    SwaggerGenOptionsExtensions.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Bugit Web Api", Version = "v1" });
});

var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI(swaggerUIOptionsrExtensions =>
{

    swaggerUIOptionsrExtensions.DocumentTitle = "BugIt server API";
    swaggerUIOptionsrExtensions.SwaggerEndpoint("/swagger/v1/swagger.json", "web API");
    
});


app.UseHttpsRedirection();



app.Run();

