using Data.Models;
using Data.Repositories;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;

using System.Linq;

namespace odata
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

#if MOCK
            services.AddSingleton<IReportsRepository, MockReportsRepository>();
#else
            services.AddSingleton<IReportsRepository, ReportsRepository>();
#endif
            services.AddControllers(mvcOptions =>
                mvcOptions.EnableEndpointRouting = false);

            services.AddOData();
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
            app.UseAuthorization();

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Expand().Count().Filter().OrderBy().MaxTop(100).SkipToken().Build();
                routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel());
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }

        public IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();
            //odataBuilder.EntitySet<Student>("Students");
            odataBuilder.EntitySet<Association>("Associations");

            return odataBuilder.GetEdmModel();
        }
    }
}
