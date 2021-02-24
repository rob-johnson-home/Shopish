using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopish.Shop.Api.EFDataAccess.Data;
using Shopish.Shop.Api.EFDataAccess.Repositories;
using Shopish.Shop.Api.Service.Services;

namespace ShopishShopApi
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
            services.AddControllers();

            // add db context, connection string from appsettings.json
            services.AddDbContext<ShopDbContext>(
        options => options.UseSqlServer(Configuration.GetConnectionString("MMTShop")));

            // DI for all our repos and servieces here
            services.AddTransient(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddTransient(typeof(IProductRepository), typeof(ProductRepository));
            services.AddTransient<IShopService, ShopService>();

            // use swagger to auto document our effort
            services.AddSwaggerGen();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Shopish.Shop.Api.Service.ViewModels.AutoMappings());
            });
            services.AddSingleton<IMapper>(sp => config.CreateMapper());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MMT Shop API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
