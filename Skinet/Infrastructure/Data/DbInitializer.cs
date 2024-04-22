using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public static class DbInitializer
{
    public static async Task InitializeDbAsync<TExecutedIn>(this IApplicationBuilder app)
    {
        await using var scope = app.ApplicationServices.CreateAsyncScope();
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<StoreContext>();
        var logger = services.GetRequiredService<ILogger<TExecutedIn>>();
        try
        {
            await dbContext.Database.MigrateAsync();
            await dbContext.SeedDataAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e,"Error occured during database initialization");
        }
    }
    
}