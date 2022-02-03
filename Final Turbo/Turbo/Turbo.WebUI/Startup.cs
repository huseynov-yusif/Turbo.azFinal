using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MotiCv.AppCode.Extension;
using MotiCv.Models.DbContexts;
using Newtonsoft.Json;
using Turbo.WebUI.AppCode.Providers;
using Turbo.WebUI.Models.DbContexts;
using Turbo.WebUI.Models.Entities.Membership;
using Turbo.WebUI.Models.ViewModels;

namespace Turbo.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(cfg => {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                cfg.Filters.Add(new AuthorizeFilter(policy));
            }).AddNewtonsoftJson(cfg=> cfg.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddRouting(cfg => cfg.LowercaseUrls = true);
            services.AddDbContext<TurboDbContext>(cfg =>
            {
                cfg.UseSqlServer(Configuration.GetConnectionString("cstring"));
            })
                .AddIdentity<TurboUser,TurboRole>()
                .AddEntityFrameworkStores<TurboDbContext>();
            services.AddScoped<HomeViewModel>();
            services.Configure<IdentityOptions>(cfg=> {
                cfg.Password.RequireDigit = false;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequiredLength = 3;
                cfg.User.RequireUniqueEmail = true;
                cfg.Lockout.MaxFailedAccessAttempts = 2;
                cfg.Lockout.DefaultLockoutTimeSpan =new TimeSpan(0,5,0);
            });
            services.ConfigureApplicationCookie(cfg =>
            {
                cfg.LoginPath = "/signin.html";
                cfg.AccessDeniedPath = "/accessdenied.html";
                cfg.ExpireTimeSpan = new TimeSpan(0, 5, 0);
                cfg.Cookie.Name = "Turbo";
            });
            services.AddAuthentication();
            services.AddAuthorization(cfg =>
            {
                foreach (var policyname in Program.principals)
                {
                    cfg.AddPolicy(policyname, p =>
                    {
                        p.RequireAssertion(h =>
                        {
                            return h.User.IsInRole("SuperAdmin")||
                             h.User.HasClaim(policyname, "1");
                        });
                    });
                }

            
            });
            services.AddScoped<SignInManager<TurboUser>>();
            services.AddScoped<RoleManager<TurboRole>>();
            services.AddScoped<UserManager<TurboUser>>();
            services.AddMediatR(this.GetType().Assembly);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.SeedMembership();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseRequestLocalization(cfg =>
            {
                cfg.AddSupportedUICultures("az", "en");
                cfg.AddSupportedCultures("az", "en");
                cfg.RequestCultureProviders.Clear();
                cfg.RequestCultureProviders.Add(new CultureProvider());
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas-with-lang",
                    pattern: "{lang}/{area:exists}/{controller=Dashboard}/{action=Index}/{id?}",
                    constraints: new
                    {
                        lang = "az|en"
                    }
                    );
                endpoints.MapControllerRoute(
                    name: "default-signin",
                    pattern: "signin.html",
                    defaults: new
                    {
                        area = "admin",
                        controller = "dashboard",
                        action = "signin"
                    });
                endpoints.MapControllerRoute(
                    name: "default-accessdenied",
                    pattern: "accessdenied.html",
                    defaults: new
                    {
                        area = "admin",
                        controller = "dashboard",
                        action = "accessdenied"
                    });
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}" );

                    endpoints.MapControllerRoute("default", "{lang}/{controller=home}/{action=index}/{id?}",
                    constraints: new
                    {
                        lang = "az|en"
                    });
                    endpoints.MapControllerRoute("default", "{controller=home}/{action=index}/{id?}");
            });
        }
    }
}
