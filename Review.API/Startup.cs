using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Review.API.Aggregators;
using Review.API.Models;
namespace Review.API
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
            services
                .AddMvc(a => { a.EnableEndpointRouting = false; })
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddScoped<IReviewService,ReviewService>();
            services.AddSwaggerForOcelot(Configuration);
            services.AddOcelot().AddCacheManager(settings => settings.WithDictionaryHandle());
            services.Configure<ApiSettings>(Configuration.GetSection("ApiSettings"));

            //TODO - Policies and logs must be here
            services.AddHttpClient<IReviewService, ReviewService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMvc();
            app.UseStaticFiles();
            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.PathToSwaggerGenerator = "/swagger/docs";
                opt.SwaggerEndpoint("/openapi.json", "Review Web API v1");
            });
            await app.UseOcelot();
        }
    }
}