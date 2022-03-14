using Api.Authorization;
using Api.Authorization.Permission;
using Api.Models;
using Api.Services.Authenticate;
using Api.Services.Categories;
using Api.Services.Products;
using Api.Services.StoreService;
using Infastructure.Data;
using Infastructure.Repositories;
using Infastructure.Repositories.Catalogs.CategoryRepo;
using Infastructure.Repositories.ProductImageRepo;
using Infastructure.Repositories.ProductRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using static Api.Share.Delegate;

namespace myshop1
{
    public class Startup
    {
        const string SECRET_KEY = "KeyOfMyshop10121994"; // in appsettings.json
        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
        
        public Startup(IConfiguration configuration, IConfiguration config)
        {
            Configuration = configuration;
            Config = config;
        }

        public IConfiguration Configuration { get; }
        public static IConfiguration Config { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });


            services.AddDbContext<AppDbContext>(options =>
                //options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))
               options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "myshop1", Version = "v1" });
            });
            services.AddAutoMapper(typeof(Startup));

            #region Config Sercure API by JWT
            //Register configuration and validate token
            string issuer = Configuration["Token:Issuer"];
            string issuer2 = Configuration.GetSection("Token:Issuer").Get<string>();
            string signingKey = Configuration.GetValue<string>("Token:Issuer");
          

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            IssuerSigningKey = SIGNING_KEY, //The key also defined in jwtController
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = issuer,
                            ValidAudience = issuer

                        };
                 });

            #endregion Config Sercure API by JWT

            #region Custome Authorization Handler in Middleware 
            //Authorize Handler

            services.AddSingleton<IAuthorizationHandler, IsAccountNotDisabledHandler>();
            services.AddSingleton<IAuthorizationHandler, IsEmployeeHandler>();

            //Authorize Policy base on Requirement
            services.AddAuthorization(options =>
            {
                options.AddPolicy("canManageProduct",
                        policyBuilder => 
                            policyBuilder.AddRequirements(
                                    new IsAccountEnabledRequirement(),
                                    new IsAllowedToEditProductRequirement()
                                )

                    );
            });
            #endregion Custome Authorization Handler in Middleware 
            #region Register Service
            /*Register Service, Repos DI here*/

            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IStorageService, FileStorageService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            //Authorize Policy Provider
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermisstionAuthorizeHandler>();

            //services.AddSingleton<IStudentRepository, TestStudentRepository1>();
            //services.AddSingleton<IStudentRepository, TestStudentRepository2>();

            services.AddSingleton<TestStudentRepository1>();
            services.AddSingleton<TestStudentRepository2>();

            services.AddTransient<ServiceResolver>(serviceProvider => serviceTypeName =>
            {
                switch (serviceTypeName)
                {
                    case ServiceType.StudentRepo1:
                        return serviceProvider.GetService<TestStudentRepository1>();
                    case ServiceType.StudentRepo2:
                        return serviceProvider.GetService<TestStudentRepository2>();
                   
                    default:
                        return null;
                }
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion Register Service
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "myshop1 v1"));
            }

            app.UseHttpsRedirection();

            //add it here
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(options => options
                            .SetIsOriginAllowed(x=> true)
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials()
                            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
