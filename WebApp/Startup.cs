using Library.Core.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Models.DataContext;
using Services.InterfaceImplementations;
using Services.Interfaces;
using Services.Version_1.Securites;
using System;
using System.IO;
using System.Reflection;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region CORS policy

            services.AddCors(options =>
            {
                // default policy
                options.AddDefaultPolicy(builder => builder
                                            .AllowAnyOrigin()
                                            .AllowAnyMethod()
                                            .AllowAnyHeader()
                                        );
                // named policy
                options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://localhost").AllowAnyHeader().AllowAnyMethod());
            });

            #endregion

            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ECard",
                    Version = "v1",
                    Description = "An API to perform Ecard Operations",
                    Contact = new OpenApiContact
                    {
                        Name = "prottoy roy",
                        Email = "prottoy@gakkmediabd.com"
                    }

                });
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
             {
               new OpenApiSecurityScheme
               {
                 Reference = new OpenApiReference
                 {
                   Type = ReferenceType.SecurityScheme,
                   Id = "Bearer"
                 }
                },
                new string[] { }
              }
  });

                // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // c.IncludeXmlComments(xmlPath);


            });


            services.AddMvc();

            #region DbContext

            services.AddDbContext<EfDbContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("DefaultDBContextConnection"), options => options.MigrationsAssembly("WebApi")));

            #endregion

            #region data protection service

            services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromMinutes(2));

            #endregion

            // TODO : New Add
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            RegisterJwtBearer(services);

            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });
            //auto-mapper
            //services.AddAutoMapper(typeof(Startup));

            #region Application Service Dependency

            //services.AddScoped<IEfDbContext, EfDbContext>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ITokenFetch, TokenFetch>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IForgotPassword, ForgotPasswordService>();
          //  services.AddScoped<IFavouriteService, FavouriteService>();


            services.AddScoped<ICallbackUrlService, CallBackUrlService>();
           // services.AddScoped<IRockvillService, RockvillService>();
         //   services.AddScoped<IFavouriteService, FavouriteService>();
            services.AddScoped<IRockvillService, RockvillService>();
            services.AddScoped<IFavouriteService, FavouriteService>();
            services.AddScoped<IbkashTransectionService, bkashTransectionService>();
            services.AddScoped<IbKashCallbackServices, bKashCallbackServices>();
            


            #endregion

            services.AddCors(options =>
            {
                // default policy
                options.AddDefaultPolicy(builder => builder

                    .AllowAnyOrigin()
                    .WithOrigins("http://localhost:3000")
                    .WithOrigins("http://localhost:4000")
                    .WithOrigins("http://localhost:4200")
                    .WithOrigins("https://evouchers.store")
                    .WithOrigins("http://evouchers.store")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()



                   );

                // named policy
                options.AddPolicy("CorsPolicy",
                         builder => builder
                        .WithOrigins("http://localhost")

                        .AllowAnyHeader().AllowAnyMethod()
                        );
            });
        }
        private void RegisterJwtBearer(IServiceCollection services)
        {
            DependencyContainer.RegisterJwtBearer(services);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env ,ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            loggerFactory.AddFile("wwwroot/Logs/mylog-{Date}.txt");
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECardApi v1"));

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors();
            app.UseCors("CorsPolicy");

            // Linux with Nginx
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
           
        }
    }
}
