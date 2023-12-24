using SpotifyClone.Services;
using SpotifyClone.types;

namespace SpotifyClone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services
                .AddHttpClient<SpotifyService>();
            builder.Services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .RegisterService<SpotifyService>();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                    .WithOrigins("https://studio.apollographql.com")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            var app = builder.Build();
            app.UseCors();

            app.MapGraphQL();

            app.Run();
        }
    }
}