using E_Invitation.Data;
using E_Invitation.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));




            //string mySqlConnectionStr = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContextPool<ApplicationDbContext>(options => options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr)));

           
            services.AddMvc();

             //services.AddDbContextPool<ApplicationDbContext>(options => options.UseMySQL(mySqlConnectionStr));
            //services.AddDbContextPool<ApplicationDbContext>(options => options.UseNpgsql(mySqlConnectionStr));

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(10);//You can set Time   
            });
            services.AddScoped<RepositoryRank, RepositoryRank>();
            services.AddScoped<RepositoryCategory, RepositoryCategory>();
            services.AddScoped<RepositoryEnclosure, RepositoryEnclosure>();
            services.AddScoped<RepositoryUser, RepositoryUser>();
            services.AddScoped<RepositoryOcassion, RepositoryOcassion>();
            services.AddScoped<RepositoryVacancy, RepositoryVacancy>();
          

            services.AddScoped<RepositoryVacancyPlan, RepositoryVacancyPlan>();
            services.AddScoped<RepositoryGestList, RepositoryGestList>();
            services.AddScoped<RepositoryVacancyReport, RepositoryVacancyReport>();
            services.AddScoped<RepositoryOcassionStatus, RepositoryOcassionStatus>();
            services.AddScoped<RepositoryOcassionMapping, RepositoryOcassionMapping>();
            services.AddScoped<RepositoryECard, RepositoryECard>();
            services.AddAuthentication("CookieAuth")
               .AddCookie("CookieAuth", config =>
               {
                   config.Cookie.Name = "Invitation";
                   config.LoginPath = "/Login/Index";
               });
            //services.AddAuthentication();

            //        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => Configuration.Bind("JwtSettings", options))
            //.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => Configuration.Bind("CookieSettings", options));
            //       // services.AddAuthentication().AddCookie(options =>
            //  {
            //      options.LoginPath = "/Login/Index/";
            //      options.AccessDeniedPath = "/Login/Index/";
            //  })
            //.AddJwtBearer(options =>
            //{
            //    options.Audience = "https://localhost:44386/";
            //    options.Authority = "https://localhost:44386/";
            //});
            services.AddHttpContextAccessor();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Login/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();




            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });


           
        }
    }
}
