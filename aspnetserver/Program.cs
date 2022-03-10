using aspnetserver;
using aspnetserver.Services;
using aspnetserver.Models;
using aspnetserver.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* LINK TO TUT
https://www.c-sharpcorner.com/article/login-and-role-based-custom-authentication-in-asp-net-core-3-1/
*/
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    {
        builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("http://localhost:3000", " https://purple-ground-019dc9c0f.1.azurestaticapps.net");
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
UserAuth UserAuthenticated = new UserAuth();
builder.Services.AddSingleton<IUserService, UserService>();
// Setting up authentication for the app using JwtBearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
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
    (LoginModel user, IUserService service) => Login(user, service));

IResult Login(LoginModel user, IUserService service)
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

        UserAuthenticated.JWTString = tokenString;
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

app.MapGet("/TESTGET",
    () => UserAuthenticated.JWTString)
    .WithTags("Test JWT Token Print");

app.MapGet("/get-all-users",
    async () => await Endpoints.GetUsers())
    .WithTags("User Endpoints");

app.Run();