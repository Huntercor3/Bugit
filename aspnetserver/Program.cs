using aspnetserver;
using aspnetserver.Services;
using aspnetserver.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Diagnostics;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using aspnetserver.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    {
        builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("http://localhost:3000", "https://purple-ground-019dc9c0f.1.azurestaticapps.net", "https://localhost:7075/swagger/index.html");
    });
});
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(SwaggerGenOptionsExtensions =>
{
    SwaggerGenOptionsExtensions.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Bugit Web Api", Version = "v1" });
});

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(swaggerUIOptionsrExtensions =>
{
    swaggerUIOptionsrExtensions.DocumentTitle = "BugIt server API";
    swaggerUIOptionsrExtensions.SwaggerEndpoint("/swagger/v1/swagger.json", "web API");
});

// HTTP request pipeline
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("CORSPolicy");

app.UseCookiePolicy();
LoginController loginCon = new LoginController();
RegisterController registerCon = new RegisterController();

#region User Endpoints

app.MapPost("/loginController",
    (LoginModel user) => loginCon.LoginUser(user)).WithTags("User Endpoints");
app.MapPost("/registerController",
    (RegisterModel user) => registerCon.RegisterUser(user)).WithTags("User Endpoints");

#endregion User Endpoints

#region Bug Endpoints

app.MapPost("/add-bug-to-project-by-project-id/{bugId},{projectId}", async (int projectId, int bugId) =>
{
    ProjectDBHelper.AddBugToProject(projectId, bugId);
}).WithTags("Bug Endpoints");

app.MapPost("/create-bug", async (Bug bugToCreate) =>
{
    BugDBHelper.AddBug(bugToCreate);
}).WithTags("Bug Endpoints");

app.MapPost("/update-bug", async (Bug bugToUpdate) =>
{
    BugDBHelper.UpdateBug(bugToUpdate);
}).WithTags("Bug Endpoints");

app.MapPost("/get-all-bugs", async () =>
 await BugDBHelper.GetAllBugs()).WithTags("Bug Endpoints");

app.MapGet("/get-bug-by-bug-id/{bugId}", async (int bugId) =>
    await BugDBHelper.GetBugByID(bugId)).WithTags("Bug Endpoints");

app.MapGet("/get-bug-comment-by-id/{bugId}", async (int bugId) =>
{
    await BugDBHelper.GetCommentsForBug(bugId);
}).WithTags("Bug Endpoints");

app.MapGet("/get-bugs-by-project-id/{projectId}", async (int projectId) =>
{
    await ProjectDBHelper.GetBugsInProject(projectId);
}).WithTags("Bug Endpoints");

app.MapDelete("/delete-bug-by-id/{bugId}", async (int bugId) =>
{
    BugDBHelper.DeleteBug(bugId);
}).WithTags("Bug Endpoints");

#endregion Bug Endpoints

#region Project Endpoints

app.MapPost("/add-new-project-by-project-name", async (string projectName) =>
{
    ProjectDBHelper.AddNewProject(projectName);
}).WithTags("Project Endpoints");

app.MapGet("/get-bugs-in-project-by-id/{projectId}", async (int projectId) =>
{
    await ProjectDBHelper.GetBugsInProject(projectId);
}).WithTags("Project Endpoints");

#endregion Project Endpoints

/*app.MapPost("update-bug",
    (Bug bug) =>
    {
        BugDBHelper.UpdateBug(bug);
    }
    ).WithTags("Bug Endpoints");*/

app.Run();