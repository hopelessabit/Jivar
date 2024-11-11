using Jivar.BO;
using Jivar.DAO.DAOs;
using Jivar.DAO.Interface;
using Jivar.Repository;
using Jivar.Repository.Interface;
using Jivar.Service.Interfaces;
using Jivar.Service.PayLoads.Requests.Firebase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Jivar.Service.Implements;
using Jivar.DAO;
using Microsoft.EntityFrameworkCore;
using Google;

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


        builder.Services.AddScoped<JivarDbContext, JivarDbContext>();

        //Add DAOs
        builder.Services.AddScoped<IAccountDAO, AccountDAO>();

        //Add repostories
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();

        //Add services
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IAccountService, AccountService>();


        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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