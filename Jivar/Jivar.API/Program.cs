using Jivar.BO;
using Jivar.DAO;
using Jivar.DAO.DAOs;
using Jivar.DAO.Interface;
using Jivar.Repository;
using Jivar.Repository.Interface;
using Jivar.Service.Implements;
using Jivar.Service.Interfaces;
using Jivar.Service.PayLoads.Requests.Firebase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<JivarDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", policy =>
            {
                policy.WithOrigins("*")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });


        // Add services to the container.

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Jivar API Test", Version = "v1" });

            // Cấu hình Bearer token 
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Vui lòng nhập 'Bearer' [space] và token vào đây.",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"

            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
            });
        });



        // Load Firebase settings from configuration
        var firebaseSettings = builder.Configuration.GetSection("Firebase").Get<FirebaseSetting>();

        if (firebaseSettings == null)
        {
            throw new Exception("Firebase settings not found in configuration.");
        }

        // Load JWT settings

        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

        // Add JWT Authentication
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience
            };
        });

        builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        builder.Services.AddScoped<JivarDbContext, JivarDbContext>();

        //Add DAOs
        builder.Services.AddScoped<IAccountDAO, AccountDAO>();

        //Add repostories
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<ISprintRepository, SprintRepository>();
        builder.Services.AddScoped<ISprintTaskRepository, SprintTaskRepository>();
        builder.Services.AddScoped<ISprintTaskRepository, SprintTaskRepository>();
        builder.Services.AddScoped<ITaskRepository, TaskRepository>();

        //Add services
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<ISprintService, SprintService>();
        builder.Services.AddScoped<ISprintTaskService, SprintTaskService>();
        builder.Services.AddScoped<ITaskService, TaskService>();


        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHttpContextAccessor();

        var app = builder.Build();


        app.UseCors("AllowSpecificOrigin");
        // Configure the HTTP request pipeline.

        app.UseSwagger();
        app.UseSwaggerUI();


        app.UseRouting();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}