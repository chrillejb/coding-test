using System;
using System.IO;
using CodingTestApi.Adapters;
using CodingTestApi.Auth;
using CodingTestApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CodingTestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CodingTestApi", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(Directory.GetParent(typeof(Program).Assembly.Location).FullName, "CodingTestApi.xml"));
            });

            services.AddHttpClient<SpotifyTokenFetcher>(client =>
            {
                client.BaseAddress = new Uri(Configuration["Urls:SpotifyTokenUrl"]);
            });
            services.AddHttpClient<ISpotifySearchAdapter, SpotifySearchAdapter>(client =>
            {
                client.BaseAddress = new Uri(Configuration["Urls:SpotifySearchUrl"]);
            });
            services.AddTransient<ArtistMatchingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodingTestApi v1"));
            }

            app.UseExceptionHandler("/Error");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
