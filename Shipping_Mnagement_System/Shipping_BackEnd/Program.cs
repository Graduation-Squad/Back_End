using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shipping.Core.Models.Identity;
using Shipping.Core.Repositories;
using Shipping.Core.Services.Contracts;
using Shipping.Repository;
using Shipping.Repository.Data;
using Shipping.Repository.Data.Identity;
using Shipping.Service;
using Shipping_APIs.Errors;
using Shipping_APIs.MappingProfiles;
using Shipping_APIs.Middlewares;
using System.Text;

namespace Shipping_APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            builder.Services.AddControllers();

            // ? Swagger Configuration
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShippingSys.APIs", Version = "v1" });

                #region Enable JWT Authentication
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token."
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
                #endregion
            });


            // ? Database Configuration
            builder.Services.AddDbContext<ShippingContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // ? Identity Configuration
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ShippingContext>();

            // ? Dependency Injection
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
            builder.Services.AddScoped<UserService, UserService>();
            builder.Services.AddScoped<IPermissionService, PermissionService>();
            builder.Services.AddScoped<IUserGroupService, UserGroupService>();
            builder.Services.AddScoped<IBranchService, BranchService>();

            // ? AutoMapper Configuration
            builder.Services.AddAutoMapper(config => config.AddProfile(new MappingProfiles.MappingProfiles()));

            // ? Custom API Error Handling
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(p => p.Value.Errors.Count > 0)
                        .SelectMany(p => p.Value.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToArray();

                    var validationErrorResponse = new ApiValidationErrorResponse { Errors = errors };
                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });
            #endregion

            #region JWT Authentication
            var jwtKey = builder.Configuration["JwtSettings:Key"];
            if (string.IsNullOrEmpty(jwtKey) || jwtKey.Length < 16)
            {
                throw new ArgumentNullException(nameof(jwtKey), "JWT Key is missing or too short in configuration.");
            }

            Console.WriteLine($"Loaded JWT Key: {jwtKey}"); // Debugging
            var key = Encoding.UTF8.GetBytes(jwtKey);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    ValidateLifetime = true
                };
            });
            #endregion

            builder.Services.AddScoped<IGovernorateService, GovernorateService>();
            builder.Services.AddScoped<ICityService, CityService>();
            builder.Services.AddScoped<IAreaService, AreaService>();



            var app = builder.Build();

            #region Database Migration & Seeding
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ShippingContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await context.Database.MigrateAsync();
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(e, "Migration error occurred.");
            }
            #endregion

            #region Configure Middleware Pipeline
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShippingSys.APIs v1"));
            }

            app.UseStatusCodePagesWithRedirects("/errors/{0}"); 
            //app.UseStatusCodePages();

            app.UseHttpsRedirection(); 
            app.UseAuthentication();  
            app.UseAuthorization();   
            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}