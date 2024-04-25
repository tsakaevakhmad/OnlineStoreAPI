using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.BLL.AutoMapper;
using OnlineStoreAPI.BLL.Interfaces;
using OnlineStoreAPI.BLL.Interfaces.Utilities;
using OnlineStoreAPI.BLL.Services;
using OnlineStoreAPI.BLL.Utilities;
using OnlineStoreAPI.DAL.Contexts;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.DAL.Repositories;
using OnlineStoreAPI.DAL.RepositoryServices;
using OnlineStoreAPI.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddTransient<ICategoryServices, CategoryServices>();
builder.Services.AddTransient<ICompanyServices, CompanyServices>();
builder.Services.AddTransient<IItemCategoryServices, ItemCategoryServices>();
builder.Services.AddTransient<IItemServices, ItemServices>();

// Repository
builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IItemCategoryRepository, ItemCategoryRepository>();
builder.Services.AddScoped<IRepository<Company>, CompanyRepository>();
builder.Services.AddScoped<IItemRepositories, ItemRepository>();

//Cache service
builder.Services.AddScoped<IRepositoryCacheServices, RepositoryCacheServices>();

//Utilities
builder.Services.AddScoped<ISortAndFilterManager, SortAndFilterManager>();

// AutoMapperProfiles
builder.Services.AddAutoMapper(typeof(CategoryProfile), typeof(ItemCategoryProfile), typeof(CompanyProfile));

var connection = builder.Configuration.GetSection("DB").Value;
builder.Services.AddDbContext<AppDbContext>(option => option.UseNpgsql(connection));

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Redis casher
builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = builder.Configuration.GetSection("Redis:Configuration").Value;
    options.InstanceName = builder.Configuration.GetSection("Redis:InstanceName").Value;
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var frontHost = builder.Configuration.GetSection("FrontHost").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins(frontHost)  //       
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

var app = builder.Build();
app.UseCors("AllowSpecificOrigin");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
