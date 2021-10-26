using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            services
                .AddScoped<FighterRepository>()
                .AddScoped<LoginRepository>()
                .AddSingleton<ArenaRepository>()
                .AddSingleton<JWTMiddleware>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = Configuration["Auth0:Authority"];
                options.Audience = Configuration["Auth0:Audience"];
                options.RequireHttpsMetadata = false;
            });
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware<JWTMiddleware>();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
