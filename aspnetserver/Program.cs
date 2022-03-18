using aspnetserver;
using aspnetserver.Services;
using aspnetserver.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
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
        .WithOrigins("http://localhost:3000", "https://purple-ground-019dc9c0f.1.azurestaticapps.net");
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
/*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    // Setting token parameters, the Jwt values will need to be updated for deployment.
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
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
*/
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
        options.Cookie.HttpOnly = true;
    });
// Telling the api the use Authorization
builder.Services.AddAuthorization();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
/// </Login>

var app = builder.Build();

var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None,
};
app.UseCookiePolicy(cookiePolicyOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
//app.UseCookiePolicy(cookiePolicyOptions);

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
LoginController loginCon = new LoginController();
app.MapPost("/login",
    (LoginModel user) => loginCon.LoginOnPostAsync(user));

app.MapPost("/add-new-project-by-project-name", async (string projectName) => { await ProjectDBHelper.AddNewProject(projectName); }).WithTags("Project Endpoints");

app.MapPost("/register",
    (RegisterModel user, IUserService service) => RegisterAsync(user, service));

/*
async Task<IActionResult> LoginOnPostAsync(LoginModel user, IUserService service)
{
    if (!string.IsNullOrEmpty(user.EmailAddress) &&
        !string.IsNullOrEmpty(user.Password))
    {
        var loggedInUser = service.CheckUserInDBO(user);
        if (loggedInUser is null) return (IActionResult)Results.NotFound("User not found or password incorrect");

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, loggedInUser.EmailAddress),
            new Claim(ClaimTypes.Role, loggedInUser.Role)
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
            IsPersistent = true,
            IssuedUtc = DateTimeOffset.UtcNow,
            RedirectUri = "/nomatch"
        };

        HttpContext.SignInAsync
            (
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties
            );

        return (IActionResult)Results.Ok(tokenString);
    }

    // Something failed. Redisplay the form.
    return (IActionResult)Results.BadRequest("Invalid user credentials");
}
*/
async Task<IResult> RegisterAsync(RegisterModel userEntry, IUserService service)
{
    if (!string.IsNullOrEmpty(userEntry.emailAddress) &&
        !string.IsNullOrEmpty(userEntry.password))
    {
        User userToRegister = new User(userEntry.userId, userEntry.firstName, userEntry.lastName, userEntry.emailAddress, userEntry.phoneNumber, userEntry.hardware, userEntry.role, userEntry.password);
        await UserDBHelper.AddUser(userToRegister);

        LoginModel login = new LoginModel();
        login.EmailAddress = userToRegister.email;
        login.Password = userToRegister.password;

        return Results.Ok();
    }
    return Results.BadRequest("Invalid registration credentials");
}

// THIS IS AN EXAMPLE HOW TO UTILIZE ROLES
/*
app.MapGet("/listUsers",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
(IUserService service) => ListUsers(service))
    .Produces<List<UserAuth>>(statusCode: 200, contentType: "application/json");
*/

app.MapGet("/get-all-users",
    async () => await Endpoints.GetUsers())
    .WithTags("User Endpoints");

app.Run();