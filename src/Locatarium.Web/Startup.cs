using Locatarium.BLL.Extensions;
using Locatarium.DAL.Context;
using Locatarium.DAL.Extensions;
using Locatarium.Web.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Locatarium.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var connectionStringCore = Configuration.GetConnectionString("LocatariumDb");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Dependency Injection for architecture layers.
            services.AddLocatariumDALService(connectionStringCore);
            services.AddLocatariumBLLService();

            services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<LocatariumDbContext>();

            services.AddAuthentication(options =>
             {
                 options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                 options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
             });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Users/Login";
                options.LogoutPath = "/Users/Logout";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", policy =>
                    policy.RequireAssertion(context =>
                        context.User.FindFirst(ClaimTypes.Role).Value == "Administrator"));
            });


            services.AddSingleton<IAuthorizationHandler, ResidenceAuthorizationHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}