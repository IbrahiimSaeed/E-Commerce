using Domain.Contracts;
using E_Commerce.API.Middlewares;

namespace E_Commerce.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedDatabaseAsyns(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var ObjectOfDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeeding.SeedDataAsync();

            return app;
        }
        public static WebApplication UseExceptionHandlingMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionHandlingMiddlewares>();
            return app;
        }
        public static WebApplication UseSwaggerMiddlewares(this WebApplication app)
        {
            app.UseSwagger();   //Middlewares ==> swagger
            app.UseSwaggerUI(); //Middlewares ==> swagger
            return app;
        }
    }
}
