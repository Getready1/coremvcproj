using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.DataLayer.Repositories.Impl;
using SportsStore.DataLayer.Repositories.Interfaces;
using SportsStore.DomainModels;
using SportsStore.Web.ViewModels;
using Microsoft.Extensions.Configuration;
using SportsStore.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Web
{
    public class Startup
    {
		public Startup(IConfiguration configuration)
		{
			Configuratuion = configuration;
		}

		public IContainer ApplicationContainer { get; set; }

		public IConfiguration Configuratuion { get; set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public IServiceProvider ConfigureServices(IServiceCollection services)
        {
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuratuion["ConnectionStrings:DefaultConnection"]));

			services.AddMvc();
			services.AddAutoMapper();
			services.AddAutofac();

			var builder = new ContainerBuilder();
			builder.Populate(services);
			builder.RegisterType<ProductRepository>().As<IProductRepository>();
			builder.RegisterInstance(Mapper.Instance);
			ApplicationContainer = builder.Build();

			return new AutofacServiceProvider(ApplicationContainer);
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
			app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
				app.UseStatusCodePages();
            }

			app.UseMvcWithDefaultRoute();
			SeedData(app);
        }

		void SeedData(IApplicationBuilder app)
		{
			ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
			context.Database.Migrate();
			if (!context.Products.Any())
			{
				context.Products.AddRange(
						new Product
						{
							Name = "Kayak", Description = "A boat for one person",
							Category = "Watersports", Price = 275
						},
						new Product
						{
							Name = "Lifejacket",
							Description = "Protective and fashionable",
							Category = "Watersports", Price = 48.95m
						},
						new Product
						{
							Name = "Soccer Ball",
							Description = "FIFA-approved size and weight",
							Category = "Soccer", Price = 19.50m
						},
						new Product
						{
							Name = "Corner Flags",
							Description = "Give your playing field a professional touch",
							Category = "Soccer", Price = 34.95m
						},
						new Product
						{
							Name = "Stadium",
							Description = "Flat-packed 35,000-seat stadium",
							Category = "Soccer", Price = 79500
						},
						new Product
						{
							Name = "Thinking Cap",
							Description = "Improve brain efficiency by 75%",
							Category = "Chess", Price = 16
						},
						new Product
						{
							Name = "Unsteady Chair",
							Description = "Secretly give your opponent a disadvantage",
							Category = "Chess", Price = 29.95m
						},
						new Product
						{
							Name = "Human Chess Board",
							Description = "Secretly give your opponent a disadvantage",
							Category = "Chess",
							Price = 75
						},
						new Product
						{
							Name = "Bling-Bling King",
							Description = "Gold-plated, diamond-studded King",
							Category = "Chess",
							Price = 1200
						}
					);
				context.SaveChanges();
			}
		}
    }
}
