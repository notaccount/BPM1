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
using BPM.Models;
using BPM.Repository.PowerManage;

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

            services.AddScoped(typeof(PowerUserRepository));
            services.AddScoped(typeof(PowerMenuRepository));

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
                   name: "area",
                   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
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
                    Power_User user = new Power_User();
                    user.ID = Guid.NewGuid();
                    user.IsSuperUser = true;
                    user.Cn = "超级管理员";
                    user.CreateTime = DateTime.Now;
                    user.PassWord = "123456";
                    user.UID = "Admin";
                    db.Power_User.Add(user);
                    db.SaveChanges();



                    Power_Area area = new Power_Area();
                    area.Code = "A001";
                    area.CreateTime = DateTime.Now;
                    area.ID = Guid.NewGuid();
                    area.IsEnable = true;
                    area.Level = 1;
                    area.ParentID = Guid.Empty;
                    area.Title = "北京总公司";
                    db.Power_Area.Add(area);
                    db.SaveChanges();
                }
                else
                {
                    //db.Database.Migrate();
                }
            }
        }
    }
}
