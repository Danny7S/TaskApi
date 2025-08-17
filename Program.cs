using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskApi;
using TaskApi.Repositories;
using TaskApi.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<TaskDbContext>(options=>
    {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });


var secret = builder.Configuration.GetValue<string>("AppSettings:Token");
var issuer = builder.Configuration.GetValue<string>("AppSettings:Issuer");
var audiences = builder.Configuration.GetSection("AppSettings:Audience").Get<List<string>>();
var key = Convert.FromBase64String(secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),

        ValidateIssuer = false,
        ValidIssuer = issuer,

        ValidateAudience = false,
        ValidAudiences = audiences,

        ValidateLifetime = false,

        ClockSkew = TimeSpan.Zero,
    };

});


builder.Services.AddScoped<IToDoTaskRepository, ToDoTaskRepository>();
builder.Services.AddScoped<IToDoTaskService, ToDoTaskService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

var testToken = JwtTokenGenerator.GenerateTestToken(builder.Configuration);
Console.WriteLine("==== TEST JWT TOKEN ====");
Console.WriteLine(testToken);
Console.WriteLine("========================");

app.Run();
