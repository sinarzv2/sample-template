using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Mapper;
using Application.Services.AcountServices;
using Application.Services.DataInitializer;
using Application.Services.JwtServices;
using Domain.Common;
using Domain.Common.DependencyLifeTime;
using Infrastructure.IRepository;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SinaRazavi_Test.Extentions;

namespace SinaRazavi_Test
{
    public class Startup
    {
        private readonly SiteSettings _siteSettings;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _siteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));

            services.AddDbContext(Configuration);
            services.AddCustomIdentity(_siteSettings.IdentitySettings);
            services.AddControllers();
            services.AddJwtAuthentication(_siteSettings.JwtSettings);
            services.AddSwagger();
            services.AddAutoMapper(typeof(UserMapper));
            services.AddApiVersioning();


            services.Scan(scan => scan
                .FromAssemblyOf<AcountService>()
                .AddClasses(classes => classes.AssignableTo<ITransientService>())
                .AsImplementedInterfaces()
                .WithTransientLifetime()

                .AddClasses(classes => classes.AssignableTo<IScopedService>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()

                .FromAssemblyOf<UserRepository>()
                .AddClasses(classes => classes.AssignableTo<ITransientService>())
                .AsImplementedInterfaces()
                .WithTransientLifetime()

                .AddClasses(classes => classes.AssignableTo<IScopedService>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwaggerAndUi();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.IntializeDatabase();
        }
    }
}