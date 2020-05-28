using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Domain;
using Ecommerce.Domain.Models;
using Ecommerce.Infrastructure.Mappers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core = Ecommerce.Core;
using Infra = Ecommerce.Infrastructure;
using DataMembers = Ecommerce.Common.DataMembers;
using Ecommerce.Core.Validations;
using Ecommerce.Services;

namespace Ecommerce
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region ID
            services.AddScoped<Core.IArticuloManager, Core.Managers.Articulo>();
            services.AddScoped<Core.IArticuloTipoManager, Core.Managers.ArticuloTipo>();
            services.AddScoped<Core.ILoteManager, Core.Managers.Lote>();
            services.AddScoped<Core.IUsuarioManager, Core.Managers.Usuario>();
            services.AddScoped<Core.INotificacionesManager, Core.Managers.Notificaciones>();

            services.AddScoped<Infra.IArticuloInfrastructure, Infra.Repository.Articulo>();
            services.AddScoped<Infra.IArticuloTipoInfrastructure, Infra.Repository.ArticuloTipo>();
            services.AddScoped<Infra.ILoteInfrastructure, Infra.Repository.Lote>();
            services.AddScoped<Infra.IUsuarioInfrastructure, Infra.Repository.Usuario>();
            services.AddScoped<Infra.INotificacionesInfrastructure, Infra.Repository.Notificaciones>();

            services.AddSingleton<Infra.Mappers.ITransformMapper, Infra.Mappers.TransformMapper>();

            services.AddTransient<StatisticsService>();
            #endregion

            //app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            services.AddScoped<IConnectionContext>(x => new ConnectionContext(Configuration.GetConnectionString("DeRemate")));


            services.AddDbContext<ProductsManagerContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DeRemate")));

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddXmlSerializerFormatters()
            .AddFluentValidation();

            #region Validaciones
            services.AddTransient<IValidator<DataMembers.Input.Usuario>, UsuarioValidation>();
            services.AddTransient<IValidator<DataMembers.Input.Lote>, LoteValidation>();
            services.AddTransient<IValidator<DataMembers.Input.Articulo>, ArticuloValidation>();
            #endregion

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Index";
                    //options.AccessDeniedPath
                });
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
                app.UseExceptionHandler("/Error/Status");
                app.UseHsts();
            }

            #region Configuracion Automapper
            var config = new MapperConfiguration(cfg =>
            {
                Ecommerce.Infrastructure.Mappers.TransformMapper.Initialize(cfg);
            });

            var mapper = config.CreateMapper();
            Ecommerce.Infrastructure.Mappers.TransformMapper.SetMapper(mapper);
            #endregion

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
