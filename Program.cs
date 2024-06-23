using AutoMapper;
using FluentValidation;
using HealthcareAppointmentApp.Configuration;
using HealthcareAppointmentApp.Data;
using HealthcareAppointmentApp.DTO;
using HealthcareAppointmentApp.Helpers;
using HealthcareAppointmentApp.Repositories;
using HealthcareAppointmentApp.Service;
using HealthcareAppointmentApp.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using System.Text;

namespace HealthcareAppointmentApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var jwtSettings = builder.Configuration.GetSection("JWTSettings");

            // Add services to the container.
            builder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
            });

            var connString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<HealthCareDbContext>(options => options.UseSqlServer(connString));
            builder.Services.AddRepositories();
            builder.Services.AddScoped<IApplicationService, ApplicationService>();
            builder.Services.AddScoped<IValidator<AvailabilityInsertDTO>, AvailabilityInsertValidator>();
            builder.Services.AddExceptionHandler<CustomExceptionHandler>();

            builder.Services.AddScoped(provider => 
                new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new MapperConfig());
                }).CreateMapper()
            );

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["ValidIssuer"],
                    ValidAudience = jwtSettings["ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        jwtSettings["securityKey"]!))
                };
            });

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    
                });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Health care API", Version = "v1" });
                options.SupportNonNullableReferenceTypes();
                options.EnableAnnotations();
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
                    new OpenApiSecurityScheme
                    {
                        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        BearerFormat = "JWT"
                    });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = JwtBearerDefaults.AuthenticationScheme,
                            Name = "Authorization",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                options.SchemaFilter<TimeOnlySchemaFilter>();
            });

            builder.Services.AddSwaggerGenNewtonsoftSupport();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AngularClient",
                    c => c.WithOrigins("http://localhost:4200")  // Angular runs on http://localhost:4200
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler(_ => { });
            app.UseHttpsRedirection();
            app.UseCors("AngularClient");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
