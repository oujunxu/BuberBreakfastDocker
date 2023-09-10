using System.Configuration;
using BuberBreakfast.Context;
using BuberBreakfast.Services.Breakfasts;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
{
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));
    builder.Services.AddControllers();
    builder.Services.AddScoped<IBreakfastService, BreakfastService>();
    builder.Services.AddDbContext<BreakfastContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("SQLServer"), serverVersion));
}

var app = builder.Build(); // this is the pipeline every requests has to go through
{   
    app.UseExceptionHandler("/error"); // works like a try catch for the following middlewares
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}