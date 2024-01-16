using ApiSongs.Models;
using ApiSongs.Services;
using Microsoft.EntityFrameworkCore;

namespace ApiSongs
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IArtistData, SqlArtistData>();
            services.AddScoped<IPlatfromData, SqlPlatformData>();
            services.AddScoped<ISongData, SqlSongData>();
            services.AddControllers();
            var connection = "server=localhost; database=platform-db; user=root; password=Mourad123";
            services.AddDbContext<SongsApiDbContext>(
                x => x.UseMySql(connection, ServerVersion.AutoDetect(connection)));
            services.AddSwaggerGen();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("./swagger/v1/swagger.json", "SongsApi");
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = context => context.Response.WriteAsync("OOPS")
                });
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
