using EmployeeTest.Data;
using EmployeeTest.GenericRepo.GenericRepositoryService;
using EmployeeTest.GenericRepo.GenericRepositoryService.Interface;
using EmployeeTest.Service.EmployeeDataService;
using EmployeeTest.Service.EmployeeDataService.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTest
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
            services.AddCors(option => option.AddPolicy("MyBlogPolicy", builder => {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

            }));
            services.AddControllers();
            services.AddHttpClient();
            services.AddMvc();

            services.AddHttpContextAccessor();
            #region DatabaseSettings
            services.AddDbContext<EmployeeDbContext>(options =>options.UseSqlServer(Configuration.GetConnectionString("EmployeeDbConnectionString")));
            #endregion

            #region genericRepository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion

            #region EmployeeDataService
            services.AddScoped<IEmployeeRepoService, EmployeeRepoService>();
            #endregion

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v3", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Project Api Services",
                    Version = "v3",
                    Description = "All Api",
                });
            });

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
            app.UseCors("MyBlogPolicy");

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v3/swagger.json", "Project Api Service"));

        }


    }
}
