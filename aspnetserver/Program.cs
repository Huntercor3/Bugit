<<<<<<< HEAD

using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);



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

=======
using aspnetserver;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(SwaggerGenOptionsExtensions =>
{
    SwaggerGenOptionsExtensions.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Bugit Web Api", Version = "v1" });
});

var  app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI(swaggerUIOptionsrExtensions =>
{

    swaggerUIOptionsrExtensions.DocumentTitle = "BugIt server API";
    swaggerUIOptionsrExtensions.SwaggerEndpoint("/swagger/v1/swagger.json", "web API");

});


app.UseHttpsRedirection();

//app.MapGet("/get-all-users", async () => await Endpoints.GetUsers());

app.Run();
>>>>>>> 89e3a5010eae4bbae78d7eafe97623b3f3e26c2e
