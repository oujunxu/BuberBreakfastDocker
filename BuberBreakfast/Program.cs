using BuberBreakfast.Services.Breakfasts;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddScoped<IBreakfastService, BreakfastService>();
}

var app = builder.Build(); // this is the pipeline every requests has to go through
{   
    app.UseExceptionHandler("/error"); // works like a try catch for the following middlewares
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}