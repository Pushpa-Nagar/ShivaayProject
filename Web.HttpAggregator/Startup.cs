using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;
using Web.HttpAggregator.Config;
using Web.HttpAggregator.Services.ProductManagement.IProductManagementServices;
using Web.HttpAggregator.Services.ProductManagement.ProductManagementServices;

namespace Web.HttpAggregator
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
            services.AddOptions();
            services.Configure<BaseUrlsConfig>(Configuration.GetSection("urls"));
            //services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddControllers(options => options.EnableEndpointRouting = false);
            services.AddMvc().AddNewtonsoftJson();
            services.AddHttpServices();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "WebGateway.API", Version = "v1" });
                c.CustomSchemaIds(x => x.FullName);
                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer",
                //    BearerFormat = "JWT",
                //    In = ParameterLocation.Header,
                //    Description = "JWT Authorization header using the Bearer scheme."
                //});
                //c.OperationFilter<AddRequiredHeaderParameter>();
                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //          new OpenApiSecurityScheme
                //            {
                //                Reference = new OpenApiReference
                //                {
                //                    Type = ReferenceType.SecurityScheme,
                //                    Id = "Bearer"
                //                }
                //            },
                //            new string[] {}
                //    }
                //});
            });

            //services.AddIdentityCore<ApplicationUser>()
            //.AddEntityFrameworkStores<AuthDBContext>()
            // .AddDefaultTokenProviders();
            //services.AddCustomAuthentication(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var pathBase = Configuration["PATH_BASE"];

            if (!string.IsNullOrEmpty(pathBase))
            {
                loggerFactory.CreateLogger("init").LogDebug($"Using PATH BASE '{pathBase}'");
                app.UsePathBase(pathBase);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("../swagger/v1/swagger.json", "WebGateway API v1");
                    c.RoutePrefix = string.Empty;
                });
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<BaseUrlsConfig>(configuration.GetSection("urls"));
            services.AddMvc();
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //        builder => builder.AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .AllowCredentials());
            //});

            return services;
        }
        //public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        //{
        //    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        //    var identityUrl = configuration.GetValue<string>("urls:identity");
        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        //    }).AddJwtBearer(options =>
        //    {
        //        options.Authority = identityUrl;
        //        options.RequireHttpsMetadata = false;
        //        options.Audience = "webshoppingagg";
        //        options.Events = new JwtBearerEvents()
        //        {
        //            OnAuthenticationFailed = async ctx =>
        //            {
        //            },
        //            OnTokenValidated = async ctx =>
        //            {
        //            }
        //        };
        //    });

        //    return services;
        //}
        public static IServiceCollection AddHttpServices(this IServiceCollection services)
        {
            //register delegating handlers
            //services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //register http services
            services.AddHttpClient<IProductManagementService, ProductManagementService>()
               //.AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
               .AddPolicyHandler(GetRetryPolicy())
               .AddPolicyHandler(GetCircuitBreakerPolicy());


            return services;
        }
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
              .HandleTransientHttpError()
              .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
              .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        }
        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }

}
