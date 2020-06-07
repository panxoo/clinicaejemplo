using FRANLES_DENT_3.Data;
using FRANLES_DENT_3.Libreria;
using FRANLES_DENT_3.Servicios.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace FRANLES_DENT_3
{
    public class Startup
    {

        private readonly ILoggerFactory _factory;
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

            

        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("DefaultConnection"),
                                        mySqlOptions =>
                                        {
                                            mySqlOptions.ServerVersion(new Version(8, 0, 16), ServerType.MySql);
                                        })
                        .UseLoggerFactory(_factory)
                        .EnableSensitiveDataLogging());

            var dataProtectionProviderType = typeof(DataProtectorTokenProvider<IdentityUser>);

            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddTokenProvider(TokenOptions.DefaultProvider, dataProtectionProviderType);

            services.ConfigureApplicationCookie(options =>
            {
                //Obtiene o establece un valor que especifica si un script del
                //lado del cliente puede acceder a una cookie.
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(12);
                options.LoginPath = "/Home/Index";
                options.AccessDeniedPath = "/Usuario/Account/AccessDenied";
            });

            services.AddSession(options =>
            {
                options.Cookie.Name = ".FRANLES.Session";
                options.IdleTimeout = TimeSpan.FromHours(6);
            });

            //services.AddAuthorization(options => {
            //    options.AddPolicy("Authorization", policy => policy.RequireRole("Admin", "User"));
            //});

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                options.Lockout.MaxFailedAccessAttempts = 2;
                options.Lockout.AllowedForNewUsers = true;
            });

            services.AddScoped<IListGeneral, ListGeneral>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation(); 
            services.AddRazorPages();
             
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute("Principal", "Principal", "{controller=Principal}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute("AtributoEmpresa", "AtributoEmpresa", "{controller=ConfigAtributo}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute("Usuarios", "Configuracion", "{controller=Usuario_Conf}/{action=Usuarios}/{id?}");
                endpoints.MapAreaControllerRoute("AdminPersonal", "AdminPersonal", "{controller=AdminUsuario}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute("SudoAdministrador", "SudoAdmin", "{controller=SudoAdministrador}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute("EmpresaConf", "Configuracion", "{controller=EmpresaConfg}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}