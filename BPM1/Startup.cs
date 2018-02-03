using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BPM.Repository;
using Microsoft.EntityFrameworkCore;

namespace BPM1
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
            string ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            string ProviderName = Configuration.GetConnectionString("ProviderName");

            if (ProviderName == "SqlServer")
            {
                services.AddDbContext<DataContext>(options => options.UseSqlServer(ConnectionString));
            }
            else if (ProviderName == "SqlLite")
            {
                services.AddDbContext<DataContext>(options => options.UseSqlite(ConnectionString));
            }


            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
            InitializeNetCoreBBSDatabase(app.ApplicationServices);
        }

        private void InitializeNetCoreBBSDatabase(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<DataContext>();

                if (db.Database != null && db.Database.EnsureCreated())
                {
                    //db.PowerUser.AddRange(CreateUserList());
                    //db.SaveChanges();
                }
                else
                {
                    //db.Database.Migrate();
                }
            }
        }
    }
}
