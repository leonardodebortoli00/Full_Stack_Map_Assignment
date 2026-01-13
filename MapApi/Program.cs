
using MapApi.Middleware;
using MapApi.Services;

namespace MapApi
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IMapService, MapService>();
            
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ApiKeyMiddleware>();
            app.MapControllers();

            // Handle Render PORT
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            app.Urls.Add($"http://0.0.0.0:{port}");

            app.Run();
        }
    }
}
