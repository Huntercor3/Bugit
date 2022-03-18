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

    SwaggerGenOptionsExtensions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    SwaggerGenOptionsExtensions.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

/// <Login>
builder.Services.AddSingleton<IUserService, UserService>();
// Setting up authentication for the app using JwtBearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    // Setting token parameters, the Jwt values will need to be updated for deployment.
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
// Telling the api the use Authorization
builder.Services.AddAuthorization();
/// </Login>

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

/// <MoreLogin>
// Telling the api to use Authentication and Authorization services
app.UseAuthentication();
app.UseAuthorization();

// Login feature
app.MapPost("/login",
    (UserLogin user, IUserService service) => Login(user, service));

IResult Login(UserLogin user, IUserService service)
{
    if (!string.IsNullOrEmpty(user.EmailAddress) &&
        !string.IsNullOrEmpty(user.Password))
    {
        // Replace with identity model of some sort
        var loggedInUser = service.CheckUserInDBO(user);
        if (loggedInUser is null) return Results.NotFound("User not found or password incorrect");

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, loggedInUser.EmailAddress),
            new Claim(ClaimTypes.Role, loggedInUser.Role)
        };

        var token = new JwtSecurityToken
            (
            issuer: builder.Configuration["Jwt:Issuer"],
            audience: builder.Configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                SecurityAlgorithms.HmacSha256)
            );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Results.Ok(tokenString);
    }
    return Results.BadRequest("Invalid user credentials");
}

// THIS IS AN EXAMPLE HOW TO UTILIZE ROLES
/*
app.MapGet("/listUsers",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
(IUserService service) => ListUsers(service))
    .Produces<List<UserAuth>>(statusCode: 200, contentType: "application/json");
*/

/*app.MapGet("/get-all-users",
    async () => await Endpoints.GetUsers())
    .WithTags("User Endpoints");
*/



app.MapPost("/get-all-bugs", async () =>
 await BugDBHelper.GetAllBugs()).WithTags("bug Endpoints");

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