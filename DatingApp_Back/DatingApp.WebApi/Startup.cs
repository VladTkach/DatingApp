using DatingApp.DAL.Context;
using DatingApp.WebApi.Extensions;
using DatingApp.WebApi.Middleware;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.WebApi;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<DatingAppContext>(options =>
            options.UseSqlite(_configuration["ConnectionStrings:DatingAppConnection"]));

        services.RegisterAutoMapper();

        services.RegisterCustomServices();

        services.ConfigureJwt(_configuration);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseCors(builder => builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("https://localhost:4200"));

        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(cfg =>
        {
            cfg.MapControllers();
        });
        
        InitializeDatabase(app);
    }

    private static async void InitializeDatabase(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
        try
        {
            await using var context = scope.ServiceProvider.GetRequiredService<DatingAppContext>();
            await context.Database.MigrateAsync();
            await Seed.SeedUsers(context);
        }
        catch (Exception e)
        {
            var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
            logger.LogError(e, "An Error occured during migration");
        }
    }
}