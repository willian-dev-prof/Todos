using CQRS.Api.Utils;
using CQRS.Business.Handlers.Concrete;
using CQRS.Infra.Context;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CQRS.Business.Handlers;
using CQRS.Business.Handlers.QueryHandler;
using CQRS.Domain.Repository;
using CQRS.Infra.Repositories;
using CQRS.Business.Handlers.Views;
using CQRS.Business.Commands.Responses;
using CQRS.Business.Handlers.Queries;

namespace CQRS.Api {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddControllers();
            services.AddDbContext<ControletodosContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConectionSql")));
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CQRS.Api", Version = "v1" });
            });

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            services.AddSingleton(appSettings);

            services.AddMediatR(typeof(TodoHandler).Assembly);
            services.AddMediatR(typeof(TodoQueryHandlers).Assembly);
            services.AddScoped<ITodoRepository, TodoRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRS.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            var supportedCultures = appSettings.SupportedLanguages;
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures.ToArray())
                .AddSupportedUICultures(supportedCultures.ToArray());
            app.UseRequestLocalization(localizationOptions);

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
