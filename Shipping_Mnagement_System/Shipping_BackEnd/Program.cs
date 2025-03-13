
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Shipping.Core.Models.Identity;
using Shipping.Core.Repositories;
using Shipping.Repository;
using Shipping.Repository.Data;
using Shipping.Repository.Data.Identity;
using Shipping.Service;
using Shipping_APIs.Errors;
using Shipping_APIs.Middlewares;

namespace Shipping_APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Service Add services to the container
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShippingSys.APIs", Version = "v1" });
            });

            builder.Services.AddDbContext<ShippingContext>(Options=>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }
            );

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ShippingContext>();

            builder.Services.AddScoped<UserService, UserService>();
            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {

                    /*
                     ModelState = {
                        {"Name", ModelStateEntry { Errors = ["The Name field is required."] } },
                        { "Age", ModelStateEntry { Errors = ["The field Age must be between 18 and 60."] } }
                     }
                     */
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                    .SelectMany(p => p.Value.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });

            #endregion



            var app = builder.Build();

            using var Scope = app.Services.CreateScope();
            var Services = Scope.ServiceProvider;
            var context = Services.GetRequiredService<ShippingContext>();
            var roleManager = Services.GetRequiredService<RoleManager<IdentityRole>>();
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                await context.Database.MigrateAsync();
                await SeedRoles.Initialize(roleManager);
            }
            catch (Exception e)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(e, "migration error");
            }


            #region Configure-Configure the HTTP request pipeline
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Talabat.APIs v1"));
            }   

            app.UseStatusCodePagesWithRedirects("/errors/{0}"); //kestrel server who will send the code status
            //It only activates when a request reaches the end of the pipeline without a response

            app.UseHttpsRedirection(); //forces https

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
