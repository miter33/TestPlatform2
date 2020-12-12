using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestPlatform.DAL.Context;
using TestPlatform.WEB.Dependency;

namespace TestPlatform.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddMvcOptions(opts => 
            opts.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Поле не может быть пустым"));
            string conString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<TestDbContext>(options => options.UseSqlServer(conString, p => p.MigrationsAssembly("TestPlatform.DAL")));
            services.AddDependencies();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStatusCodePages();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "User/{userName}",
                    defaults: new { controller = "Statistics", action = "ShowUserStatsByCategories", userName = "" }
                    );

                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "User/{userName}/{id}",
                    defaults: new { controller = "Statistics", action = "ShowUserStatsByTests", userName = "", id = 0 }
                    );

                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
