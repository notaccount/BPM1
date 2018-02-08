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
using BPM.Repository.SystemManage;
using Microsoft.AspNetCore.Http;

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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


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
            services.AddScoped(typeof(AreaReponsitory));
            services.AddSession();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider svp)
        {
            HttpContext.ServiceProvider = svp;
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
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                               name: "area",
                               template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
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
                    user.PassWord = MD5Helper.MD5Encrypt32("123456");
                    user.UID = "Admin";
                    user.U_AreaCode = "A001";
                    user.U_IsValid = true;
                    db.Power_User.Add(user);
                    db.SaveChanges();

                    Power_Area area = new Power_Area();
                    area.Code = "A001";
                    area.ID = Guid.NewGuid();
                    area.IsEnable = true;
                    area.Level = 1;
                    area.ParentID = Guid.Empty;
                    area.Title = "北京总公司";
                    db.Power_Area.Add(area);
                    db.SaveChanges();


                    Power_Menu menu1 = new Power_Menu();
                    menu1.ID = Guid.NewGuid();
                    menu1.Code = "M001";
                    menu1.IsEnable = true;
                    menu1.IsShow = true;
                    menu1.Name = "系统管理";
                    menu1.U_CreateDate = DateTime.Now;
                    menu1.U_IsSystem = true;
                    menu1.U_IsValid = true;
                    db.Power_Menu.Add(menu1);
                    db.SaveChanges();

                    Power_Menu menu2 = new Power_Menu();
                    menu2.ID = Guid.NewGuid();
                    menu2.Code = "M002";
                    menu2.IsEnable = true;
                    menu2.IsShow = true;
                    menu2.Name = "区域管理";
                    menu2.U_CreateDate = DateTime.Now;
                    menu2.U_IsSystem = true;
                    menu2.U_IsValid = true;
                    menu2.Path = "/SystemManage/Area/Index";
                    menu2.ParentID = menu1.ID;
                    db.Power_Menu.Add(menu2);
                    db.SaveChanges();

                    Power_Menu menu3 = new Power_Menu();
                    menu3.ID = Guid.NewGuid();
                    menu3.Code = "M003";
                    menu3.IsEnable = true;
                    menu3.IsShow = true;
                    menu3.Name = "菜单管理";
                    menu3.U_CreateDate = DateTime.Now;
                    menu3.U_IsSystem = true;
                    menu3.U_IsValid = true;
                    menu3.Path = "/SystemManage/Menu/Index";
                    menu3.ParentID = menu1.ID;
                    db.Power_Menu.Add(menu3);
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
