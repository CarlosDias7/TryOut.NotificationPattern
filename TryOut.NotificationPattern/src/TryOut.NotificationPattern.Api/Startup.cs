using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using TryOut.NotificationPattern.Repository.Register;

namespace TryOut.NotificationPattern.Api
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseMvc().UseApiVersioning();
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    x.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
                }
            });

            app.UseRouting();

            app.UseRequestLocalization(new RequestLocalizationOptions { DefaultRequestCulture = new RequestCulture("pt-BR") });
            CultureInfo.CurrentCulture = new CultureInfo("pt-BR");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(m => { m.EnableEndpointRouting = false; })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            ConfigureSwagger(services);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddRepository();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void ConfigureSwagger(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "TryOut.NotificationPattern", Version = "v1" });
            });
        }
    }
}