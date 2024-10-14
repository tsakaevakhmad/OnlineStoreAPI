using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.BLL.AutoMapper;
using OnlineStoreAPI.BLL.Interfaces;
using OnlineStoreAPI.BLL.Interfaces.Utilities;
using OnlineStoreAPI.BLL.Services;
using OnlineStoreAPI.BLL.Utilities;
using OnlineStoreAPI.DAL.Contexts;
using OnlineStoreAPI.DAL.FileStorages;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.DAL.Repositories;
using OnlineStoreAPI.DAL.RepositoryServices;
using OnlineStoreAPI.Domain.Configurations;
using OnlineStoreAPI.Domain.Entities;
using System.Data.Entity;

namespace OnlineStoreAPI.ApplicationExtensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void Configurations(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MinioOptions>(builder.Configuration.GetSection("Minio"));
        }

        public static void Reposytories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IRepository<Company, string>, CompanyRepository>();
            builder.Services.AddScoped<IItemRepositories, ItemRepository>();

            //Cache service
            builder.Services.AddScoped<IRepositoryCacheServices, RepositoryCacheServices>();
        }

        public static void Services(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ICategoryServices, CategoryServices>();
            builder.Services.AddTransient<ICompanyServices, CompanyServices>();
            builder.Services.AddTransient<IItemServices, ItemServices>();
            builder.Services.AddTransient<IFileStorage, MInioServices>();

            //Utilities
            builder.Services.AddScoped<ISortAndFilterManager, SortAndFilterManager>();
        }

        public static void AutoMapperProfiles(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(CategoryProfile), typeof(ItemCategoryProfile), typeof(CompanyProfile));
        }

        public static void MainCofigurations(this WebApplicationBuilder builder)
        {
            var connection = builder.Configuration.GetSection("DB").Value;
            builder.Services.AddDbContext<AppDbContext>(option => option.UseNpgsql(connection));

            builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            builder.Services.AddStackExchangeRedisCache(options => {
                options.Configuration = builder.Configuration.GetSection("Redis:Configuration").Value;
                options.InstanceName = builder.Configuration.GetSection("Redis:InstanceName").Value;
            });

            var frontHost = builder.Configuration.GetSection("FrontHost").Get<string[]>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                        .WithOrigins(frontHost)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });
        }

        public static IApplicationBuilder Migrate(this IApplicationBuilder applicationBuilder)
        {
            using (var scope = applicationBuilder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }

            return applicationBuilder;
        }
    }
}
