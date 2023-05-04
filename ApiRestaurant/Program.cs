using ApiRestaurant;
using ApiRestaurant.Entities;
using ApiRestaurant.Services;
using System.Reflection;
using NLog.Web;
using ApiRestaurant.Middleware;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RestaurantDbContext>();
builder.Services.AddScoped<IRestaurantService,RestaurantService>();
builder.Services.AddScoped<RestaurantSeeder>();
builder.Services.AddScoped<IDishService,IDishService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Host.UseNLog();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddSwaggerGen();
var app = builder.Build();
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<RestaurantSeeder>();
seeder.Seed();
app.UseMiddleware<ErrorHandlingMiddleware>();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API");
});

app.UseAuthorization();
app.MapControllers();



app.Run();
