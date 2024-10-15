using OnlineStoreAPI.ApplicationExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.MainCofigurations();
builder.Reposytories();
builder.Services();
builder.AutoMapperProfiles();
builder.Configurations();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Migrate();

app.MapControllers();

app.Run();
