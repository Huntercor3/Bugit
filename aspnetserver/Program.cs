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
//var person = Endpoints.

//app.MapGet("/get-all-users", async () => await Endpoints.GetAllUsers())
 //   .WithTags("User Endpoints");

app.MapGet("/get-all-bugs", async () => await Endpoints.GetAllBugs())
    .WithTags("User Endpoints");

app.MapGet("/get-all-projects", async () => await Endpoints.GetAllProjects())
    .WithTags("User Endpoints");

app.MapGet("/get-all-organizations", async () => await Endpoints.GetAllOrganizations())
    .WithTags("User Endpoints");

/*app.MapGet("/get-users-in-project-by-id/{projectId}", async (int projectId) =>
{
   await Endpoints.GetUsersInProject(projectId);

}).WithTags("User Endpoints");
*/
app.MapGet("/get-bugs-in-project-by-id/{projectId}", async (int projectId) =>
{
    await ProjectDBHelper.GetBugsInProject(projectId);

}).WithTags("Project Endpoints");

app.MapGet("/get-project-in-organization-by-id/{organizationId}", async (int organizationId) =>
{
    await Endpoints.GetProjectInOrganization(organizationId);

}).WithTags("Project Endpoints");

app.MapGet("/get-bug-comment-by-id/{bugId}", async (int bugId) =>
{
    await Endpoints.GetCommentsForBug(bugId);

}).WithTags("Bug Endpoints");

app.MapPost("/add-bug-to-project-by-project-id/{bugId},{projectId}", async (int projectId, int bugId) =>
{
    await ProjectDBHelper.AddBugToProject(projectId, bugId);

}).WithTags("Bug Endpoints");

app.MapPost("/add-new-project-by-project-name", async (string projectName) =>
{
    await ProjectDBHelper.AddNewProject(projectName);

}).WithTags("Project Endpoints");

app.MapGet("/get-bugs-by-project-id/{projectId}", async (int projectId) =>
{
    await ProjectDBHelper.GetBugsInProject(projectId);

}).WithTags("Bug Endpoints");

app.Run();