using AutoMapper;
using Data.Context;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using API.Extensions;
using System;
using API.Middleware;
using Microsoft.Extensions.FileProviders;
using MicroElements.Swashbuckle.FluentValidation;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddApplicationServices();
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers()
                      .AddFluentValidation(c =>
                      {
                          c.RegisterValidatorsFromAssemblyContaining<Startup>();
                          c.ValidatorFactoryType = typeof(HttpContextServiceProviderValidatorFactory);
                      });

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddCors();

            services.AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;
                    options.Filters.Add(typeof(ValidationFilter));
                });
                //.AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>())
                //.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwaggerDocumentation();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                             .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                         ValidateIssuer = false,
                         ValidateAudience = false,
                         ValidateLifetime = true,
                         ClockSkew = TimeSpan.Zero
                     };
                 });

        }

        public void Configure(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())

            using (var context = scope.ServiceProvider.GetService<ApplicationContext>())
                context.Database.Migrate();

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.Use(async (context, next) =>
            {
                await next().ConfigureAwait(true);

                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/index.html";
                    await next().ConfigureAwait(true);
                }
            });


            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Content")
                ),
                RequestPath = "/content"
            });
            app.UseDefaultFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();
            app.UseSwaggerDocumention();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
