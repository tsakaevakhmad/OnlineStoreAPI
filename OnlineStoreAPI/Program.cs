using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.BLL.AutoMapper;
using OnlineStoreAPI.BLL.Interfaces;
using OnlineStoreAPI.BLL.Services;
using OnlineStoreAPI.DAL.Contexts;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.DAL.Repositories;
using OnlineStoreAPI.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddTransient<ICategoryServices, CategoryServices>();
builder.Services.AddTransient<ICompanyServices, CompanyServices>();

// Repository
builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IRepository<ItemCategory>, ItemCategoryRepository>();
builder.Services.AddScoped<IRepository<Company>, CompanyRepository>();
builder.Services.AddScoped<IItemRepositories, ItemRepository>();

//AutoMapperProfiles
builder.Services.AddAutoMapper(typeof(CategoryProfile));

var connection = builder.Configuration.GetSection("DB").Value;
builder.Services.AddDbContext<AppDbContext>(option => option.UseNpgsql(connection));

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
