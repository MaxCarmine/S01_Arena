using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using P01_ArenaMvc.DataAccess;
using P01_ArenaMvc.JWT;
using P01_ArenaMvc.Middleware;
using P01_ArenaMvc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P01_ArenaMvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {

            services.AddDbContext<AppDbCtx>(options => {
                object p = options.UseNpgsql(Configuration.GetConnectionString("Default Connection"));
            });
            services.AddScoped<FighterRepository>();
            services.AddScoped<LoginRepository>();
            services.AddSingleton<ArenaRepository>();
            services.AddAuthentication(IISDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = "/Account/Unauthorized/";
                    options.AccessDeniedPath = "/Account/Forbidden/";
                })
                .AddJwtBearer(options => {
                    options.Audience = "http://localhost:5001/";
                    options.Authority = "http://localhost:5000/";
                });
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseRouting();
            try {
                app.UseAuthorization();
                app.UseMiddleware<JWTMiddleware>();

            } catch (InvalidOperationException) {
                Console.WriteLine("Proooooblem");
            }
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
